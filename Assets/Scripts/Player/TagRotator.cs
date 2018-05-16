using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TagRotator : MonoBehaviour {

	void Start () {
		
	}
	
	void Update () {
        this.gameObject.transform.LookAt(this.transform.position + (this.transform.position - Camera.main.transform.position));
	}
}
