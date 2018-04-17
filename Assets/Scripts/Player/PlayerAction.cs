using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerAction : MonoBehaviour {

    Camera head;
    public GameObject pointer;
    // Use this for initialization
	void Start () {
        head = GetComponentInChildren<Camera>();
	}
	
	// Update is called once per frame
	void Update () {
        //Left click

        Ray ray = new Ray(head.transform.position, head.transform.forward);

        RaycastHit hit;
        if (Physics.Raycast(ray, out hit, 10))
        {
            //changes color of pointer based on whether in range or not

            pointer.GetComponent<Image>().color = new Color32(0,255,0, 255);

            //if LMB clicked, destroy (if in range)
            if (Input.GetMouseButtonDown(0))
            {
                hit.collider.gameObject.SetActive(false);
            }
        }

        else
        {
            pointer.GetComponent<Image>().color = new Color32(255, 0 , 0, 255);
        }

	}
}
