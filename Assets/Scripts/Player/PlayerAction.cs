using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerAction : MonoBehaviour {

    Camera head;
    public float meleeDamage = 0.2f;
    public GameObject pointer;

    // Use this for initialization
    void Start () {
        head = GetComponentInChildren<Camera>();
	}
	
	// Update is called once per frame
	void Update () {
        Ray ray = new Ray(head.transform.position, head.transform.forward);
        RaycastHit hit;
        if(Physics.Raycast(ray, out hit, 10))
        {
		    //changes color of pointer based on whether in range or not
		    pointer.GetComponent<Image>().color = new Color32(0,255,0, 255);
            //Left click
            if (Input.GetMouseButtonDown(0))
            {
                GameObject gameObject = hit.collider.gameObject;
                if (gameObject.CompareTag("Floor"))
                {
                    gameObject.GetComponent<TileController>().DoDamage(meleeDamage);
                }
            }
        } else
        {
            pointer.GetComponent<Image>().color = new Color32(255, 0, 0, 255);
        }
	}
}
