using System.Collections;
using System.Linq;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

[RequireComponent(typeof(PlayerMovement))]
public class EffectManager : NetworkBehaviour {
    public enum EffectType { SPEED, JUMP };
    public class Effect
    {
        public EffectType type;
        public float multiplier;
        public float timer;
        public float totalTime;
        public Effect(EffectType type, float multiplier, float time)
        {
            this.type = type;
            this.multiplier = multiplier;
            this.timer = time;
            this.totalTime = time;
        }
    }
    PlayerMovement movement;
    Dictionary<EffectType, Effect> effects = new Dictionary<EffectType, Effect>();

    private void Start()
    {
        movement = this.gameObject.GetComponent<PlayerMovement>();
    }


    [Server]
    public void AddEffect(EffectType type, float multiplier, float time)
    {
        if (effects.ContainsKey(type))
        {
            effects[type].timer = time;
            effects[type].totalTime = time;
            effects[type].multiplier = multiplier;
            RpcUpdateEffectTime(type, time);
        }
        else
        {
            Effect e = new Effect(type, multiplier, time);
            effects[type] = e;
            RpcAddEffect(type, multiplier, time);
            switch (type)
            {
                case EffectType.SPEED:
                    movement.speed *= multiplier;
                    break;
                case EffectType.JUMP:
                    movement.jumpHeight *= multiplier;
                    break;
            }
        }
    } 
    [ClientRpc]
    public void RpcAddEffect(EffectType type, float multiplier, float time)
    {
        Effect e = new Effect(type, multiplier, time);
        effects[type] = e;
    }

    [ClientRpc]
    public void RpcUpdateEffectTime(EffectType type, float time)
    {
        if (effects.ContainsKey(type))
        {
            effects[type].totalTime = time;
            effects[type].timer = time;
        }
    }

    [Server]
    public void RemoveEffect(Effect effect)
    {
        effects.Remove(effect.type);
        RpcRemoveEffect(effect.type);
        switch (effect.type)
        {
            case EffectType.SPEED:
                movement.speed /= effect.multiplier;
                break;
            case EffectType.JUMP:
                movement.jumpHeight /= effect.multiplier;
                break;
        }
    }

    [ClientRpc]
    public void RpcRemoveEffect(EffectType type)
    {
        effects.Remove(type);
    }

	void FixedUpdate () {
        foreach (EffectType key in effects.Keys.ToList())
        {
            effects[key].timer -= Time.fixedDeltaTime;
            if(effects[key].timer <= 0)
            {
                if(isServer)
                    RemoveEffect(effects[key]);
            }
        }
    }

    public Dictionary<EffectType, Effect> GetEffects()
    {
        return effects;
    }
}
