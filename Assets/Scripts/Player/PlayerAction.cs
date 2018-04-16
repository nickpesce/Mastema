using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAction : MonoBehaviour {

    Camera camera;
	// Use this for initialization
	void Start () {
        camera = GetComponentInChildren<Camera>();
	}
	
	// Update is called once per frame
	void Update () {
        //Left click
		if(Input.GetMouseButtonDown(0))
        {
            Ray ray = new Ray(camera.transform.position, camera.transform.forward);
            RaycastHit hit;
            if(Physics.Raycast(ray, out hit, 10))
            {
                hit.collider.gameObject.SetActive(false);
            }
        }
	}
}
