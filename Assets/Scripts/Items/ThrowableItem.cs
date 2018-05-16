using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class ThrowableItem : Item {

    protected bool thrown = false;

    [Server]
    public override void UseItem(Vector3 position, Vector3 direction)
    {
        base.UseItem(position, direction);
        Throw(position, direction);
    }

    [Server]
    public void Throw(Vector3 position, Vector3 direction)
    {
        this.transform.position = position;
        Rigidbody rb = this.gameObject.GetComponent<Rigidbody>();
        rb.isKinematic = false;
        rb.velocity = direction * 50;
        SpawnItem();
        RpcMakeNotKinematic();
        thrown = true;
    }


    [ClientRpc]
    private void RpcMakeNotKinematic()
    {
        Rigidbody rb = this.gameObject.GetComponent<Rigidbody>();
        rb.isKinematic = false;
    }

}
