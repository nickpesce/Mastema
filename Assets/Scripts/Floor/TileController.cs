using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class TileController : NetworkBehaviour {

    float damage;

	void Start () {
        damage = 0;
	}

    //Gets called even if destroyed (damage >= 1)
    [ClientRpc]
    void RpcOnDamage(float newDamage)
    {
        Debug.Log("Damage updated to " + newDamage);
        damage = newDamage;
        this.gameObject.GetComponent<Renderer>().material.color = new Color(1 - newDamage, 1 - newDamage, 1 - newDamage);
    }

    [ClientRpc]
    void RpcOnDestroy()
    {
        this.gameObject.SetActive(false);
    }

    [ClientCallback]
    private void Update()
    {
    }

    [Server]
    public void DoDamage(float amount)
    {
        damage += amount;
        RpcOnDamage(damage);
        if (damage >= 1)
        {
            RpcOnDestroy();
        }
    }

    [Server]
    public void CheckFloorDestroyer(GameObject gameObject)
    {
        FloorDestroyer floorDestroyer;
        if ((floorDestroyer = gameObject.GetComponent<FloorDestroyer>()) != null)
        {
            DoDamage(floorDestroyer.calculateDamage(this.gameObject));
        }
    }

    [ServerCallback]
    void OnCollisionEnter(Collision collision)
    {
        CheckFloorDestroyer(collision.gameObject);
    }

    [ServerCallback]
    private void OnTriggerEnter(Collider collider)
    {
        CheckFloorDestroyer(collider.gameObject);
    }


}
