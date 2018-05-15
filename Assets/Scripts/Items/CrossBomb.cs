using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

[RequireComponent(typeof(Rigidbody))]
public class CrossBomb : ThrowableItem
{

    public GameObject cross;
    public GameObject floor;
    public GameObject particles;

    void Start()
    {
    }

    void Update()
    {

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
            Instantiate(cross, new Vector3(this.transform.position.x, 0f, this.transform.position.z), Quaternion.identity);
            Instantiate(particles, this.transform.position, Quaternion.identity);
            NetworkServer.Destroy(this.gameObject);
        }
    }
}
