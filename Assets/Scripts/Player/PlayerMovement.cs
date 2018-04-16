using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerMovement : MonoBehaviour {
    private Rigidbody rb;


    public float moveSpeed = 5F;
    public float rotSpeed = 10f;
    public float jumpSpeed = 3f;
    float angle;
    float rotX;
    float rotY;

    // Use this for initialization
    void Start () {
        rb = GetComponent<Rigidbody>();

	}
	
	// Update is called once per frame
	void FixedUpdate () {

        float moveHorizonal = Input.GetAxis("Horizontal");
        float moveVertical = Input.GetAxis("Vertical");

        //rotX += Input.GetAxis("Mouse X");
        //rotY -= Input.GetAxis("Mouse Y");

        //transform.eulerAngles = new Vector3(0, rotX, 0);


        Vector3 movementV = new Vector3(0.0f, 0.0f, moveVertical);
        Vector3 movementH = new Vector3(-moveHorizonal, 0.0f, 0.0f);
        Vector3 up = new Vector3(0.0f, 1f, 0.0f);

        transform.Translate(movementV * moveSpeed * Time.deltaTime);
        transform.Translate(movementH * moveSpeed * Time.deltaTime);

        if (Input.GetKeyDown("space"))
        {

            if (transform.position.y < 5 && transform.position.y >= 1)
            {
                rb.AddForce(up * jumpSpeed, ForceMode.Impulse);
            }
        }




    }
}
