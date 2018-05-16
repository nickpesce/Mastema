using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Networking;

public class HostButtons : MonoBehaviour {

    public GameObject nameInput;
    public GameObject portInput;

    // Use this for initialization
    void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    public void HostGame()
    {
        Game.PLAYER_NAME = nameInput.GetComponent<Text>().text;
        int port = 7777;
        Int32.TryParse(portInput.GetComponent<Text>().text, out port);
        NetworkManager.singleton.networkPort = port;
        NetworkManager.singleton.StartHost();
    }
}
