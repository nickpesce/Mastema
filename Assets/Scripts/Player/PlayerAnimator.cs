using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAnimator : MonoBehaviour {

    const float locomotionAnimationSmoothTime = .1f;

    Animator animator;

    float baseSpeed;

    private PlayerAction action;
    private Rigidbody rb;

    // Use this for initialization
    void Start()
    { 
        animator = GetComponentInChildren<Animator>();
        baseSpeed = this.gameObject.GetComponent<PlayerMovement>().speed;
        action = this.gameObject.GetComponent<PlayerAction>();
        rb = this.gameObject.GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 vel = rb.velocity;
        float speedPercent = vel.magnitude / baseSpeed;
        animator.SetFloat("speedPercent", speedPercent, locomotionAnimationSmoothTime, Time.deltaTime);

    }
}
