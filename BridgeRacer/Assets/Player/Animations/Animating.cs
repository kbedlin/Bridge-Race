using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Animating : MonoBehaviour
{
    Animator animator;
    Rigidbody body;

    private void Start()
    {
        animator = GetComponent<Animator>();
        body = GetComponent<Rigidbody>();
    }


    private void FixedUpdate()
    {
        float velocity = body.velocity.magnitude;
        bool isRunning = animator.GetBool("isRunning");
        if (!isRunning && velocity > 0.05f)
        {
            animator.SetBool("isRunning", true);
            return;
        }
        if (isRunning && velocity < 0.05f)
        {
            animator.SetBool("isRunning", false);
            return;
        }

    }
}
