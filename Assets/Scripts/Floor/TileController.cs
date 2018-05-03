using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class TileController : NetworkBehaviour {

    [SyncVar(hook = "OnDamage")]
    float damage;

	void Start () {
        damage = 0;
	}

    /// <summary>
    /// When the damage is changed on the tile
    /// Gets called even if destroyed(damage >= 1)
    /// </summary>
    [Client]
    void OnDamage(float newDamage)
    {
        damage = newDamage;
        this.gameObject.GetComponent<Renderer>().material.color = new Color(1 - newDamage, 1 - newDamage, 1 - newDamage);
    }

    /// <summary>
    /// When this tile is destroyed on client. Trigger animation
    /// </summary>
    [ClientRpc]
    void RpcOnDestroy()
    {
    }

    [ClientCallback]
    private void Update()
    {
    }

    [Server]
    public void DoDamage(float amount)
    {
        damage += amount;
        if (damage >= 1)
        {
            Network.Destroy(this.gameObject);
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
