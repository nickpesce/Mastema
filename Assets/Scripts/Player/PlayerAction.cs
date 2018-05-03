using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Networking;

public class PlayerAction : NetworkBehaviour {

    GameObject head;
    public float meleeDamage = 0.2f;
    public GameObject pointer;

    public GameObject aoe;
    public float aoeRadius = 50f;

    public float aoeCD = 5f;
    private float nextAOE;

    //On server, script is not started, but it is initialized for commands
    //Start() will not work
    void Awake() {
        //The gameobject with a camera component (even if its deactivated)
        head = GetComponentInChildren<Camera>(true).gameObject;
	}
	
    [Command]
    void CmdHitFloor()
    {
        Ray ray = new Ray(head.transform.position, head.transform.forward);
        RaycastHit hit;
        if (Physics.Raycast(ray, out hit, 10))
        { 
            GameObject gameObject = hit.collider.gameObject;
            if (gameObject.CompareTag("Floor"))
            {
                gameObject.GetComponent<TileController>().DoDamage(meleeDamage);
            }
        }
    }

    [Command]
    void CmdAoeAttack()
    {
        Debug.Log("Command aoe");
        if (Time.time < nextAOE) return;
        Ray ray = new Ray(head.transform.position, head.transform.forward);
        RaycastHit hit;
        if (Physics.Raycast(ray, out hit, 10))
        {
            GameObject gameObject = hit.collider.gameObject;
            if (gameObject.CompareTag("Floor"))
            {
                Vector3 pos = gameObject.transform.position;
                GameObject bomb = Instantiate(aoe, new Vector3(pos.x, pos.y + gameObject.transform.localScale.y, pos.z), Quaternion.identity);
                bomb.transform.localScale = bomb.transform.localScale * aoeRadius;
                nextAOE = Time.time + aoeCD;
            }
        }
    }

    [ClientCallback]
    private void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            CmdHitFloor();
        }
        if (Input.GetMouseButtonDown(1))
        {
            CmdAoeAttack();
        }
        Ray ray = new Ray(head.transform.position, head.transform.forward);
        RaycastHit hit;
        if (Physics.Raycast(ray, out hit, 10))
        {
            //changes color of pointer based on whether in range or not
            pointer.GetComponent<Image>().color = new Color32(0, 255, 0, 255);
        }
        else
        {
            pointer.GetComponent<Image>().color = new Color32(255, 0, 0, 255);
        }
    }
}
