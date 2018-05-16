using System.Collections;
using System.Collections.Generic;
using UnityEngine.Networking;
using UnityEngine;

[RequireComponent(typeof(MouseLook))]
[RequireComponent(typeof(PlayerAction))]
[RequireComponent(typeof(PlayerMovement))]
public class LocalPlayerInit : NetworkBehaviour {
    public override void OnStartLocalPlayer()
    {
        AudioListener audio = GetComponentInChildren<AudioListener>();
        FlareLayer flare = GetComponentInChildren<FlareLayer>();
        Camera camera = GetComponentInChildren<Camera>();
        if(camera == null || flare==null || audio == null)
        {
            throw new System.Exception("Local player must have a Camera as a child with a camera, audio listener, and flare layer");
        }
        camera.enabled = true;
        

        flare.enabled = true;
        audio.enabled = true;
        Canvas canvas = GetComponentInChildren<Canvas>(true);
        if (canvas == null)
        {
            throw new System.Exception("Local player must have a Canvas as a child");
        }
        canvas.gameObject.SetActive(true);
        GetComponent<MouseLook>().enabled = true;
        GetComponent<PlayerAction>().enabled = true;
        transform.LookAt(new Vector3(0,0,0));
    }
}
