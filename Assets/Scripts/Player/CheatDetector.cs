using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

[RequireComponent(typeof(Rigidbody))]
public class CheatDetector : NetworkBehaviour {

    //Proportion that player is allowed to "cheat" by
    public float leniency = .1f;

    private Rigidbody rb;
    //Maximum expected distance player can move in one update
    //Assuming no acceleration
    private float expectedDistance;
    private Vector3 prevPosition;

    private void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    [ServerCallback]
	void Update () {
		if(CheckSpeed() || CheckFloating())
        {
            Debug.Log("Player is cheating!");
        }
        prevPosition = transform.position;
        expectedDistance = (rb.velocity).magnitude * Time.deltaTime;
    }

    /// <summary>
    /// If moving too fast, likely teleporting or speed hack
    /// </summary>
    /// <returns>true if cheating</returns>
    bool CheckSpeed()
    {
        float actualDistance = (prevPosition - transform.position).magnitude;
        if((actualDistance - expectedDistance) > expectedDistance*leniency) 
        {
            //TODO server recieves movement input as well and does own physics simulation
            //return true;
        }
        return false;
    }

    bool CheckFloating()
    {
        return false;
    }
}
