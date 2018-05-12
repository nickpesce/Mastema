using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

[RequireComponent(typeof(Rigidbody))]
public class CrossBomb : Item
{

    public GameObject cross;
    public GameObject floor;
    public GameObject particles;
    bool thrown = false;

    void Start()
    {
    }

    [Server]
    public override void UseItem(Vector3 position, Vector3 direction)
    {
        base.UseItem(position, direction);
        ThrowBomb(position, direction);
    }

    void Update()
    {

    }

    [Server]
    private void ThrowBomb(Vector3 position, Vector3 direction)
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
            thrown = false;
            GameObject bomb = Instantiate(cross, new Vector3(this.transform.position.x, 0f, this.transform.position.z), Quaternion.identity);
            GameObject ps = Instantiate(particles, this.transform.position, Quaternion.identity);
            NetworkServer.Destroy(this.gameObject);
        }
    }
}
