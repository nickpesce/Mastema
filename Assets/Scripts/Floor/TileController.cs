using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TileController : MonoBehaviour {

    private float damage;
	void Start () {
        damage = 0;
	}
	
	void Update () {
		
	}

    private void OnCollisionEnter(Collision collision)
    {
        FloorDestroyer floorDestroyer;
        if((floorDestroyer = collision.gameObject.GetComponent<FloorDestroyer>())!= null) {
            damage += floorDestroyer.damagePercent;
            if(damage >= 1)
            {
                this.gameObject.SetActive(false);
            }
        }
    }
}
