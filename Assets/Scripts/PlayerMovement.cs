using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerMovement : MonoBehaviour {
    private Rigidbody rb;

    public float moveSpeed = 5F;
    public float rotSpeed = 10f;
    public float jumpSpeed = 3f;

    // Use this for initialization
    void Start () {
        rb = GetComponent<Rigidbody>();
	}
	
	// Update is called once per frame
	void Update () {
        float moveHorizonal = Input.GetAxis("Horizontal");
        float moveVertical = Input.GetAxis("Vertical");


        Vector3 movementV = new Vector3(0.0f, 0.0f, moveVertical);
        Vector3 movementH = new Vector3(moveHorizonal, 0.0f, 0.0f);
        Vector3 up = new Vector3(0.0f, 1f, 0.0f);

        //rb.AddForce(movement * speed);
        transform.Translate(movementH * moveSpeed * Time.deltaTime);
        transform.Translate(movementV * moveSpeed * Time.deltaTime);

        if (Input.GetKeyDown("space"))
        {
            //Debug.Log("space");

            if (transform.position.y < 5)
            {
                rb.AddForce(up * jumpSpeed, ForceMode.Impulse);
            }
        }
    }
}
