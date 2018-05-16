using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Networking;

public class PlayerComponents : NetworkBehaviour {

    GameObject head;

    [SyncVar(hook = "OnNameChange")]
    string username;

    public TextMesh nameTag;

    // Use this for initialization
    void Start () {
        head = GetComponentInChildren<Camera>(true).gameObject;
        OnNameChange(username);
        if (isLocalPlayer)
        {
            CmdSetName(Game.PLAYER_NAME);            
        }
    }

    // Update is called once per frame
    void Update () {
		
	}

    public GameObject GetHead()
    {
        return head;
    }

    [Command]
    public void CmdSetName(string name)
    {
        this.username = name;
    }

    [Client]
    public void OnNameChange(string newName)
    {
        nameTag.text = newName;
    }
}
