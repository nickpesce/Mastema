using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class EffectsHUD : MonoBehaviour {

    public GameObject player;
    EffectManager effects;
    //TODO make better/more extendable
    public Slider speedSlider, jumpSlider;

	// Use this for initialization
	void Start () {
        effects = player.GetComponent<EffectManager>();
	}
	
	// Update is called once per frame
	void Update () {
        speedSlider.gameObject.SetActive(false);
        jumpSlider.gameObject.SetActive(false);
        foreach (KeyValuePair<EffectManager.EffectType, EffectManager.Effect> entry in effects.GetEffects())
        {
            float percent = entry.Value.timer/entry.Value.totalTime;
            if (entry.Key == EffectManager.EffectType.JUMP)
            {
                jumpSlider.value = percent * jumpSlider.maxValue;
                jumpSlider.gameObject.SetActive(true);
            }
            else if (entry.Key == EffectManager.EffectType.SPEED)
            {
                speedSlider.value = percent * speedSlider.maxValue;
                speedSlider.gameObject.SetActive(true);
            }
        }

    }
}
