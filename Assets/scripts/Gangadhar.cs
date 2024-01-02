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

    private void Start()
    {
        characterController = GetComponent<CharacterController>();

    }

    private void Update()
    {
        CharacterLocomotion();
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


    
