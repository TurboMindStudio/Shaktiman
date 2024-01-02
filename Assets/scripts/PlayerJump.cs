using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;

public class PlayerJump : MonoBehaviour
{
    public float jumpSpeed;
    public float gravity;
    private Vector3 movingDirection = Vector3.zero;
    public CharacterController controller;
    public Animator animator;

    void Update()
    {
        if (controller.isGrounded && Input.GetButtonDown("Jump"))
        {
            animator.SetBool("isJumping", true);
            animator.SetBool("isFalling", true);
            movingDirection.y = jumpSpeed;

        }
        else
        {
            animator.SetBool("isLanding", true);
            animator.SetBool("isFalling", true);
            
        }

        movingDirection.y += gravity * Time.deltaTime;
        controller.Move(movingDirection * Time.deltaTime);
    }

}
