using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using Unity.VisualScripting;
using UnityEngine;
using Cinemachine;
using UnityEngine.UI;
using TMPro;
using Unity.Mathematics;

public class Gangadhar : MonoBehaviour
{
    [Header("Player Controller")]
    //walk
    private CharacterController characterController;
    [Range(0f, 100f)]
    [SerializeField] float MoveSpeed;
    [SerializeField] Camera cam;
    [SerializeField] float smoothTime;
    Vector3 moveInput;
    private float smoothVelocity;


    //Animation
    [Header("Animation---")]
    [SerializeField] public Animator PlayerAnim;


    //Jump
    [Header("Jump Logic---")]
    [SerializeField] float JumpSpeed;
    [SerializeField] float gravity;
    [SerializeField] Transform groundCheck;
    [SerializeField] GameObject LandParticle;
    public bool isGrounded;
    public bool isJumping;
    public bool hasPlayed;
    RaycastHit hit;
    Vector3 moveVelocity;
    float maxRange = 2f;

    private void Start()
    {
        characterController = GetComponent<CharacterController>();

    }

    private void Update()
    {

       /* if (isJumping == true)
        {
          //  CharacterJump();
        }
       */
        CharacterLocomotion();
    }

    void CharacterJump()
    {


        if (Physics.Raycast(groundCheck.position, -groundCheck.up, maxRange))
        {
            isGrounded = true;
            
            PlayerAnim.SetBool("isLanding", false);
            PlayerAnim.SetBool("isJumping",false);
            // landEfx-------------
            if (hasPlayed == true)
            {
                AudioManager.instance.audioSource.PlayOneShot(AudioManager.instance.LandSfx);
                GameObject LandSmokeEfx = Instantiate(LandParticle, groundCheck.position, Quaternion.identity) as GameObject;
                Destroy(LandSmokeEfx, 2f);
                hasPlayed = false;
            }

        }
        else
        {
           
            hasPlayed = true;
            isGrounded = false;
           

        }

        if (isGrounded == true && Input.GetButtonDown("Jump"))
        {

            PlayerAnim.SetBool("isJumping", true);
            moveVelocity.y = JumpSpeed * Time.deltaTime;

        }

        //gravity-----------------------
        moveVelocity.y += gravity * Time.deltaTime;
        characterController.Move(moveVelocity);

        if (moveVelocity.y > 0.3f)
        {
            PlayerAnim.SetBool("isFalling", true);
        }
        else if (moveVelocity.y < 0.1f)
        {
            PlayerAnim.SetBool("isFalling", false);
            PlayerAnim.SetBool("isLanding", true);

        }


    }

    void CharacterLocomotion()
    {
        float horizontal = Input.GetAxis("Horizontal");
        float vertical = Input.GetAxis("Vertical");

        Vector3 direction = Quaternion.Euler(0, cam.transform.eulerAngles.y, 0) * new Vector3(horizontal, 0, vertical);
        float InputMag = Mathf.Clamp01(direction.magnitude);
        moveInput = direction.normalized;


        if (InputMag > 0.0f)
        {
            PlayerAnim.SetFloat("LocoMotion", direction.magnitude);
        }
        else
        {
            PlayerAnim.SetFloat("LocoMotion", 0);
        }

        if (moveInput != Vector3.zero)
        {

            PlayerAnim.SetBool("isMoving", true);
        }
        else
        {
            PlayerAnim.SetBool("isMoving", false);
        }

        if (moveInput != Vector3.zero)
        {

            Quaternion desiredRotation = Quaternion.LookRotation(moveInput, Vector3.up);
            transform.rotation = Quaternion.Slerp(transform.rotation, desiredRotation, smoothTime * Time.deltaTime);
        }

        characterController.Move(moveInput * MoveSpeed * Time.deltaTime);
    }
}


    
