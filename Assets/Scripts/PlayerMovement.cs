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

    // Use this for initialization
    void Start () {
        rb = GetComponent<Rigidbody>();
	}
	
	// Update is called once per frame
	void FixedUpdate () {

        float moveHorizonal = Input.GetAxis("Horizontal");
        float moveVertical = Input.GetAxis("Vertical");

        Vector3 mousePos = Input.mousePosition;


        //To make mousePos relative to center of screen
        mousePos.x -= Screen.width / 2;
        mousePos.y -= Screen.height / 2;

        //To make mousePos relative to transform
        mousePos += transform.position;
        angle = Vector3.Angle(mousePos, Vector3.right);


        transform.rotation = Quaternion.Euler(angle, 0, 0);
        

        Vector3 movement = new Vector3(0.0f, 0.0f, -moveVertical);

        Vector3 up = new Vector3(0.0f, 1f, 0.0f);
        
        transform.Translate(movement * moveSpeed * Time.deltaTime);
 

        if (Input.GetKeyDown("space"))
        {

            if (transform.position.y < 5)
            {
                rb.AddForce(up * jumpSpeed, ForceMode.Impulse);
            }
        }




    }
}
