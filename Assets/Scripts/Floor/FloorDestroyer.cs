using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FloorDestroyer : MonoBehaviour {

    public float damagePercent = 100;
    //closer tiles take full damage, but decreased as distance increases.
    //TODO different function options.
    public bool distanceBased = false;
    private float damage;
	void Start () {
        damage = damagePercent / 100;
	}
	
    public float calculateDamage(GameObject tile)
    {
        float d = damage;
        if(distanceBased)
        {
            //TODO guassian function.
            d /= Math.Max(1, Vector3.Distance(tile.transform.position, this.transform.position));
        }
        return d;
    }

	void Update () {
		
	}
}
