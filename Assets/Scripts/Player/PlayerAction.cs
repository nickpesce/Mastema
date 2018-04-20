using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerAction : MonoBehaviour {

    Camera head;
    public float meleeDamage = 0.2f;
    public GameObject pointer;

    public GameObject aoe;
    public float aoeRadius = 20f;

    public float aoeCD = 1f;
    private float nextAOE;

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
            //Right click
            if (Input.GetMouseButtonDown(1) && Time.time > nextAOE)
            {
                GameObject gameObject = hit.collider.gameObject;
                if (gameObject.CompareTag("Floor"))
                {
                    Vector3 pos = gameObject.transform.position;
                    GameObject bomb = Instantiate(aoe, new Vector3(pos.x, pos.y + gameObject.transform.localScale.y, pos.z), Quaternion.identity);
                    bomb.transform.localScale = bomb.transform.localScale * aoeRadius;
                    
                    Destroy(bomb, .01f);
                    nextAOE = Time.time + aoeCD;
                }
            }
        } else
        {
            pointer.GetComponent<Image>().color = new Color32(255, 0, 0, 255);
        }
	}
}
