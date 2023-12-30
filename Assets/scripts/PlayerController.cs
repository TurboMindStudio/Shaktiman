using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using Unity.VisualScripting;
using UnityEngine;
using Cinemachine;
using UnityEngine.UI;
using TMPro;

public class PlayerController : MonoBehaviour
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
    

    //Jump
    [Header("Jump Logic---")]
    [SerializeField] float JumpSpeed;
    [SerializeField] GameObject LandParticle;


    //Dash
    [Header("Dash Logic---")]
    [SerializeField] float dashSpeed;
    private float dashRate;
    [SerializeField] int dashTime;
    [SerializeField] GameObject dashEfx;
    [SerializeField] Image dashIcon;
    public bool isDashing;
    Vector2 currentVelocity;
    [SerializeField] TextMeshProUGUI dashRateText;


    //Animation
    [Header("Animation---")]
    [SerializeField] public Animator PlayerAnim;

    [Header("Flash Logic")]
    public GameObject MyVCam;
    public bool isFlashing;
    [SerializeField] float FlashTimeLimit;
    [Range(0f, 1000f)]
    [SerializeField] float FlashSpeed;
    [SerializeField] GameObject FlashEfx;
    [SerializeField] TextMeshProUGUI FlashTimeText;
    [SerializeField] Image flashIcon;

    [Header("Spin Logic")]
    [SerializeField] float RotateSpeed;
    [SerializeField] float FlySpeed;
    [Range(0f,1000f)]
    [SerializeField] float FlyAltitude;
    [SerializeField] GameObject FlyEfx;
    public bool isSpin;
    public bool haveSpinPower;
    [SerializeField] characterGravity gravityCs;

    //projectile reference
    private ProjectileShoot projectileShoot;

    private void Start()
    {
        projectileShoot = GetComponent<ProjectileShoot>();
        characterController =GetComponent<CharacterController>();
        AudioManager.instance.FlashRunaudioSource.mute = true;
        AudioManager.instance.FlyRotateFlashRunaudioSource.mute = true;
        haveSpinPower = true;
        dashRateText.text = "";

    }

    private void Update()
    {
        


       // spin true
        if (haveSpinPower)
        {
            Spin();
        }


        CharacterLocomotion();
        Flash();
        Dash();

        
    }




    void CharacterJump()
    {

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

        if (moveInput != Vector3.zero && isFlashing==false)
        {
           
            PlayerAnim.SetBool("isMoving", true);
        }
        else
        {
            PlayerAnim.SetBool("isMoving", false);
        }

        if (moveInput != Vector3.zero)
        {
            Dash();
            Quaternion desiredRotation = Quaternion.LookRotation(moveInput, Vector3.up);
            transform.rotation = Quaternion.Slerp(transform.rotation, desiredRotation, smoothTime * Time.deltaTime);
        }
        
        characterController.Move(moveInput * MoveSpeed * Time.deltaTime);
    }


    void Dash()
    {
        // dash Time Rate
        if (dashRate >= 1)
        {
            dashRate -= Time.deltaTime;
            dashTime = Mathf.FloorToInt(dashRate);
        }

        if (dashTime == 0)
        {
            dashRateText.text = "";
            dashIcon.color = Color.white;

        }
        else
        {
            dashIcon.color = Color.grey;
            dashRateText.text = dashTime.ToString();
        }


        isDashing = true;
        if(isDashing && isFlashing==false)
        {
            
            if (dashRate <= 1 && Input.GetKeyDown(KeyCode.Q))
            {
                
                projectileShoot.isShoot = false;
                AudioManager.instance.audioSource.PlayOneShot(AudioManager.instance.DashSfx);
                characterController.Move(moveInput * dashSpeed * Time.deltaTime);
                GameObject dashEffect = Instantiate(dashEfx, transform.position, Quaternion.identity) as GameObject;
                Destroy(dashEffect, 2);
                dashRate = 9;
               
                
            }
            
        }

        
    }

    void Flash()
    {
        //flash Activation
        
        if (Input.GetKeyDown(KeyCode.F))
        {
            isFlashing = true;
            haveSpinPower = false;
            
        }

        if (isFlashing)
        {
            // reduse Time For Flashing 
            if (FlashTimeLimit > 0)
            {
                FlashTimeLimit -= Time.deltaTime;
                int flashLimit = Mathf.FloorToInt(FlashTimeLimit);
                FlashTimeText.text = flashLimit.ToString();
                flashIcon.color = Color.grey;

                if (FlashTimeLimit < 1)
                {
                    FlashTimeLimit = 11;
                    FlashTimeText.text = "";
                    flashIcon.color = Color.white;
                    isFlashing = false;
                    haveSpinPower = true;

                }

            }
            isDashing = false;
            
            if (moveInput != Vector3.zero)
            {
                projectileShoot.isShoot = false;
                AudioManager.instance.FlashRunaudioSource.mute = false;
                ChangeCinemachineOrbit(50);
                PlayerAnim.SetBool("isFlashing", true);
                FlashEfx.SetActive(true);
                Physics.gravity = new Vector3(0, -9.81f, 0);
                characterController.Move(moveInput * FlashSpeed * Time.deltaTime);
            }
            else
            {
                AudioManager.instance.FlashRunaudioSource.mute = true;
                FlashEfx.SetActive(false);
                PlayerAnim.SetBool("isFlashing", false);
            }

        }
        else
        {
            projectileShoot.isShoot = true;
            AudioManager.instance.FlashRunaudioSource.mute = true;
            FlashEfx.SetActive(false);
            PlayerAnim.SetBool("isFlashing", false);
            ChangeCinemachineOrbit(25);
        }
    }

    void Spin()
    {     

        if (Input.GetKey(KeyCode.R))
        {
            
            gravityCs.enabled = false;
            transform.Rotate(0, RotateSpeed * Time.deltaTime, 0);
            isSpin = true;
            isDashing = false;
            isFlashing = false;
            FlyEfx.SetActive(true);

            ChangeCinemachineOrbit(50);
            CinemachineFreeLook VCamControl = MyVCam.GetComponent<CinemachineFreeLook>();
            ChangeCinemachineOrbit(50);


            if (transform.position.y <= FlyAltitude)
            {
                projectileShoot.isShoot = false;
                AudioManager.instance.FlyRotateFlashRunaudioSource.mute = false;
                Vector3 velocity = transform.up * 50;
                characterController.Move(velocity * Time.deltaTime);
                Physics.gravity = Vector3.zero;
      
            }

        }
        else
        {
            FlyEfx.SetActive(false);
            gravityCs.enabled = true;
            AudioManager.instance.FlyRotateFlashRunaudioSource.mute = true;
            CinemachineFreeLook VCamControl = MyVCam.GetComponent<CinemachineFreeLook>();
            ChangeCinemachineOrbit(22);
        }
    }

    void ChangeCinemachineOrbit(float radius)
    {
        CinemachineFreeLook VCamControl = MyVCam.GetComponent<CinemachineFreeLook>();
        VCamControl.m_Orbits[0].m_Radius = radius;
        VCamControl.m_Orbits[1].m_Radius = radius;
        VCamControl.m_Orbits[2].m_Radius = radius;
    }



}
