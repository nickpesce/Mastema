using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class Potion : Item {

    public EffectManager.EffectType stat;
    public float multiplier;
    public int time;

    [Server]
    public override void UseItem(Vector3 position, Vector3 direction)
    {
        base.UseItem(position, direction);
        DrinkPotion();
    }

    [Server]
    private void DrinkPotion()
    {
        //play animation
        //increase by <multiplayer> for <stat> of <user> for <timer> seconds
        user.GetComponent<EffectManager>().AddEffect(stat, multiplier, time);
    }
}
