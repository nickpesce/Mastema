using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Networking;

[RequireComponent(typeof(PlayerComponents))]
[RequireComponent(typeof(Inventory))]
public class PlayerAction : NetworkBehaviour {

    private PlayerComponents playerComponents;
    public float meleeDamage = 0.2f;
    public GameObject pointer;

    public GameObject aoe;
    public float aoeRadius = 50f;

    public float aoeCD = 5f;
    private float nextAOE;

    private Inventory inventory;

    private NetworkStartPosition spawnPoint;

    //On server, script is not started, but it is initialized for commands
    //Start() will not work
    void Awake() {
        //The gameobject with a camera component (even if its deactivated)
        playerComponents = GetComponent<PlayerComponents>();
        inventory = GetComponent<Inventory>();
	}
	
    void HitFloor()
    {
        Ray ray = new Ray(playerComponents.GetHead().transform.position, playerComponents.GetHead().transform.forward);
        RaycastHit hit;
        if (Physics.Raycast(ray, out hit, 10))
        {
            GameObject hitObject = hit.collider.gameObject;
            if (hitObject.CompareTag("Floor"))
            {
                CmdHitFloor(hitObject);
            }
        }
    }

    [Command]
    void CmdHitFloor(GameObject tile)
    { 
        //TODO make sure distance is < ~10
        tile.GetComponent<TileController>().DoDamage(meleeDamage);
    }

    /*
    void AoeAttack()
    {
        if (Time.time < nextAOE) return;
        Ray ray = new Ray(head.transform.position, head.transform.forward);
        RaycastHit hit;
        if (Physics.Raycast(ray, out hit, 10))
        {
            GameObject hitObject = hit.collider.gameObject;
            if (hitObject.CompareTag("Floor"))
            {
                CmdAoeAttack(hitObject);
            }
        }
    }

    
    [Command]
    void CmdAoeAttack(GameObject tile)
    {
        if (Time.time < nextAOE) return;
        Vector3 pos = gameObject.transform.position;
        //GameObject bomb = Instantiate(aoe, new Vector3(pos.x, pos.y + gameObject.transform.localScale.y, pos.z), Quaternion.identity);
        GameObject bomb = Instantiate(aoe, tile.transform.position, Quaternion.identity);
        bomb.transform.localScale = bomb.transform.localScale * aoeRadius;
        nextAOE = Time.time + aoeCD;
    }
    */

    [Command]
    void CmdUseItem(Vector3 position, Vector3 direction)
    {
        int held = inventory.GetCurrentItem();
        if(held != -1)
        {
            Item.UseItemFromInventory(held, this.gameObject, position, direction);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Death")
        {
            //collided with death barrier
            Debug.Log("Died");

            spawnPoint = FindObjectOfType<NetworkStartPosition>();

            transform.position = spawnPoint.transform.position;
            /* Options:
             *  Spectate until game is over
             *  Lives
             *  Point system (would need to keep track of who kills whom
             */

            
        }
    }

    [ClientCallback]
    private void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            HitFloor();
        }
        if (Input.GetMouseButtonDown(1))
        {
            //AoeAttack();
            CmdUseItem(playerComponents.GetHead().transform.position, playerComponents.GetHead().transform.forward);
        }
        for(int i = 0; i < Inventory.SIZE; i++)
        {
            if (Input.GetKeyDown((i+1).ToString()))
            {
                inventory.CmdSetCurrentItemIndex(i);
            }
        }
        Ray ray = new Ray(playerComponents.GetHead().transform.position, playerComponents.GetHead().transform.forward);
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

    public void setMeleeDamage(float value)
    {
        meleeDamage = Mathf.Clamp(value, 0, 1);
    }
}
