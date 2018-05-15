using System.Collections;
using System.Collections.Generic;
using UnityEngine.Networking;
using UnityEngine.UI;
using UnityEngine;
using System;

public class JoinButtons : MonoBehaviour {

    public GameObject IPInput;
    public GameObject portInput;
    public GameObject nameInput;
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    public void Join()
    {
        string ip = IPInput.GetComponent<Text>().text;
        NetworkManager.singleton.networkAddress = ip;
        int port = 7777;
        NetworkManager.singleton.networkAddress = ip;
        Int32.TryParse(portInput.GetComponent<Text>().text, out port);
        NetworkManager.singleton.networkPort = port;
        NetworkManager.singleton.StartClient();
    }
}
