using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

[RequireComponent(typeof(Rigidbody))]
public class Grenade : Item {

    public float aoeRadius;
    public GameObject aoe;
    bool thrown = false;

    void Start()
    {
    }

    [Server]
    public override void UseItem()
    {
        base.UseItem();
        ThrowGrenade();
    }

    void Update()
    {

    }

    [Server]
    private void ThrowGrenade()
    {
        PlayerComponents player = user.GetComponent<PlayerComponents>();
        this.transform.position = player.GetHead().transform.position;
        Rigidbody rb = this.gameObject.GetComponent<Rigidbody>();
        rb.isKinematic = false;
        rb.AddForce(player.GetHead().transform.forward*5000);
        SpawnItem();
        thrown = true;
    }

    [ServerCallback]
    private new void OnColliderEnter(Collision other)
    {
        base.OnColliderEnter(other);
        if (other.gameObject != user && thrown)
        {
            Explode();
        }
    }

    [ServerCallback]
    private new void OnTriggerEnter(Collider other)
    {
        base.OnTriggerEnter(other);
        if (other.gameObject != user && thrown)
        {
            Explode();
        }
    }

    [Server]
    private void Explode()
    {
        if (thrown)
        {
            GameObject bomb = Instantiate(aoe, this.transform.position, Quaternion.identity);
            bomb.transform.localScale = bomb.transform.localScale * aoeRadius;
            NetworkServer.Destroy(this.gameObject);
        }
    }
}
