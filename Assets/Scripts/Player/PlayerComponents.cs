using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerComponents : MonoBehaviour {

    GameObject head;

    // Use this for initialization
    void Start () {
        head = GetComponentInChildren<Camera>(true).gameObject;
    }

    // Update is called once per frame
    void Update () {
		
	}

    public GameObject GetHead()
    {
        return head;
    }
}
