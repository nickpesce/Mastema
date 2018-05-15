using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Buttons : MonoBehaviour {

    public GameObject mainButtons, hostButtons, joinButtons;
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    public void HostMenu()
    {
        mainButtons.SetActive(false);
        joinButtons.SetActive(false);
        hostButtons.SetActive(true);
    }

    public void JoinMenu()
    {
        mainButtons.SetActive(false);
        joinButtons.SetActive(true);
        hostButtons.SetActive(false);
    }

    public void MainMenu()
    {
        mainButtons.SetActive(true);
        joinButtons.SetActive(false);
        hostButtons.SetActive(false);
    }

    public void Exit()
    {
        Application.Quit();
    }
}
