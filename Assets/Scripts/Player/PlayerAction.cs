using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAction : MonoBehaviour {

    Camera head;
    public float meleeDamage = 0.2f;
	// Use this for initialization
	void Start () {
        head = GetComponentInChildren<Camera>();
	}
	
	// Update is called once per frame
	void Update () {
        //Left click
		if(Input.GetMouseButtonDown(0))
        {
            Ray ray = new Ray(head.transform.position, head.transform.forward);
            RaycastHit hit;
            if(Physics.Raycast(ray, out hit, 10))
            {
                GameObject gameObject = hit.collider.gameObject;
                if(gameObject.CompareTag("Floor"))
                {
                    Debug.Log("hit floor");
                    gameObject.GetComponent<TileController>().DoDamage(meleeDamage);
                }
            }
        }
	}
}
