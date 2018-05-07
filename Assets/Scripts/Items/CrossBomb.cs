using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

[RequireComponent(typeof(Rigidbody))]
public class CrossBomb : Item
{

    public GameObject cross;
    bool thrown = false;

    void Start()
    {
    }

    [Server]
    public override void UseItem()
    {
        base.UseItem();
        ThrowBomb();
    }

    void Update()
    {

    }

    [Server]
    private void ThrowBomb()
    {
        PlayerComponents player = user.GetComponent<PlayerComponents>();
        this.transform.position = player.GetHead().transform.position;
        Rigidbody rb = this.gameObject.GetComponent<Rigidbody>();
        rb.isKinematic = false;
        rb.AddForce(player.GetHead().transform.forward * 5000);
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
            GameObject bomb = Instantiate(cross, new Vector3(this.transform.position.x, .5f, this.transform.position.z), Quaternion.identity);

            NetworkServer.Destroy(this.gameObject);
        }
    }
}
