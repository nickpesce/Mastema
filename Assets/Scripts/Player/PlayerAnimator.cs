using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAnimator : MonoBehaviour {

    const float locomotionAnimationSmoothTime = .1f;

    Animator animator;

    float baseSpeed;

    public GameObject player;
    private PlayerAction action;

    // Use this for initialization
    void Start()
    { 
        animator = GetComponentInChildren<Animator>();
        baseSpeed = player.GetComponent<PlayerMovement>().speed;
        action = player.GetComponent<PlayerAction>();
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 vel = new Vector3(Input.GetAxis("Horizontal"), 0, Input.GetAxis("Vertical"));
        float speedPercent = vel.magnitude / baseSpeed;
        animator.SetFloat("speedPercent", speedPercent, locomotionAnimationSmoothTime, Time.deltaTime);

    }
}
