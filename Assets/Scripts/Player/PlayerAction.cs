using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Networking;
using UnityEngine.SceneManagement;

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

            if (!isServer && isClient) NetworkManager.singleton.StopClient();
            if (isServer)
            {
                spawnPoint = FindObjectOfType<NetworkStartPosition>();

                transform.position = spawnPoint.transform.position;
                this.gameObject.transform.LookAt(new Vector3(0, 0, 0));
            }
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
