using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TileController : MonoBehaviour {

    private float damage;
	void Start () {
        damage = 0;
	}

    public void DoDamage(float amount)
    {
        damage += amount;
        this.gameObject.GetComponent<Renderer>().material.color = new Color(1-damage, 1-damage, 1-damage);
        if (damage >= 1)
        {
            this.gameObject.SetActive(false);
        }
    }

    private void CheckFloorDestroyer(GameObject gameObject)
    {
        FloorDestroyer floorDestroyer;
        if ((floorDestroyer = gameObject.GetComponent<FloorDestroyer>()) != null)
        {
            DoDamage(floorDestroyer.calculateDamage(this.gameObject));
        }
    }

    void OnCollisionEnter(Collision collision)
    {
        CheckFloorDestroyer(collision.gameObject);
    }

    private void OnTriggerEnter(Collider collider)
    {
        CheckFloorDestroyer(collider.gameObject);
    }


}
