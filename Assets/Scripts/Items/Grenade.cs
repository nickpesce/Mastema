using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

[RequireComponent(typeof(Rigidbody))]
public class Grenade : ThrowableItem {

    public float aoeRadius;
    public GameObject aoe;
    public GameObject particles;
    


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
            GameObject bomb = Instantiate(aoe, this.transform.position, Quaternion.identity);
            GameObject ps = Instantiate(particles, this.transform.position, Quaternion.identity);
            bomb.transform.localScale = bomb.transform.localScale * aoeRadius;
            NetworkServer.Spawn(ps);
            NetworkServer.Destroy(this.gameObject);
            
        }
    }
}
