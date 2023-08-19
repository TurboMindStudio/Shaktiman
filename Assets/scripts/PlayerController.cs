using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using Unity.VisualScripting;
using UnityEngine;
using Cinemachine;
using UnityEngine.UI;


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
    [SerializeField] float gravity;
    [SerializeField] Transform groundCheck;
    [SerializeField] GameObject LandParticle;
    public bool isGrounded;
    public bool isJumping;
    public bool hasPlayed;
    RaycastHit hit;
    Vector3 moveVelocity;
    float maxRange = 2f;

    //Dash
    [Header("Dash Logic---")]
    [SerializeField] float dashSpeed;
    [SerializeField] float dashRate;
    [SerializeField] GameObject dashEfx;
    //[SerializeField] Image dashIcon;
    public bool isDashing;
    Vector2 currentVelocity;
    float smoothXTime = 10f;


    //Animation
    [Header("Animation---")]
    [SerializeField] public Animator PlayerAnim;

    [Header("Flash Logic")]
    public GameObject MyVCam;
    public bool isFlashing;
    [SerializeField] float FlashTimeLimit;
    [Range(0f, 1000f)]
    [SerializeField] float FlashSpeed;
    [SerializeField] ParticleSystem FlashEfx;

    [Header("Spin Logic")]
    [SerializeField] float RotateSpeed;
    [SerializeField] float FlySpeed;
    [Range(0f,1000f)]
    [SerializeField] float FlyAltitude;
    [SerializeField] ParticleSystem FlyEfx;
    public bool isSpin;
    public bool haveSpinPower;

    //projectile reference
    private ProjectileShoot projectileShoot;

    private void Start()
    {
        projectileShoot = GetComponent<ProjectileShoot>();
        characterController =GetComponent<CharacterController>();
        AudioManager.instance.FlashRunaudioSource.mute = true;
        AudioManager.instance.FlyRotateFlashRunaudioSource.mute = true;
        haveSpinPower = true;

    }

    private void Update()
    {
        // dash Time Rate
        if (dashRate >= 0)
        {
            dashRate -= Time.deltaTime;
        }

        // jump true
        if (isJumping == true)
        {
            CharacterJump();
        }

       // spin true
        if (haveSpinPower)
        {
            Spin();
        }


        CharacterLocomotion();
        Flash();



    }




    void CharacterJump()
    {
        

        if (Physics.Raycast(groundCheck.position, -groundCheck.up, maxRange))
        {
            isGrounded = true;
            isSpin = false;
            PlayerAnim.SetBool("isLanding", false);
            haveSpinPower = true;
            // landEfx-------------
            if (hasPlayed == true)
            {
                AudioManager.instance.audioSource.PlayOneShot(AudioManager.instance.LandSfx);
                GameObject LandSmokeEfx = Instantiate(LandParticle, groundCheck.position, Quaternion.identity) as GameObject;
                Destroy(LandSmokeEfx, 2f);
                hasPlayed= false;
            }
            
        }
        else
        {
            hasPlayed = true;
            isGrounded= false;
            isDashing = false;
            haveSpinPower = false;
            PlayerAnim.SetBool("isJumping", false);
            projectileShoot.isShoot = false;

        }

        if (isGrounded==true && Input.GetButtonDown("Jump"))
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
        isDashing = true;
        if(isDashing && isFlashing==false)
        {
            
            if (dashRate <= 0 && Input.GetKeyDown(KeyCode.Q))
            {
                
                //dashIcon.color = Color.gray;
                projectileShoot.isShoot = false;
                AudioManager.instance.audioSource.PlayOneShot(AudioManager.instance.DashSfx);
                characterController.Move(moveInput * dashSpeed * Time.deltaTime);
                GameObject dashEffect = Instantiate(dashEfx, transform.position, Quaternion.identity) as GameObject;
                Destroy(dashEffect, 2);
                dashRate = 2;
                
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
               
                if (FlashTimeLimit <= 0)
                {
                    FlashTimeLimit = 10;
                    
                    isFlashing = false;
                    haveSpinPower = true;

                }
            }


            isJumping = false;
            isDashing = false;
            
            if (moveInput != Vector3.zero)
            {
                projectileShoot.isShoot = false;
                AudioManager.instance.FlashRunaudioSource.mute = false;
                ChangeCinemachineOrbit(50);
                PlayerAnim.SetBool("isFlashing", true);
                FlashEfx.Play();
                Physics.gravity = new Vector3(0, -9.81f, 0);
                characterController.Move(moveInput * FlashSpeed * Time.deltaTime);
            }
            else
            {
                AudioManager.instance.FlashRunaudioSource.mute = true;
                FlashEfx.Stop();
                PlayerAnim.SetBool("isFlashing", false);
            }

        }
        else
        {
            projectileShoot.isShoot = true;
            AudioManager.instance.FlashRunaudioSource.mute = true;
            FlashEfx.Stop();
            PlayerAnim.SetBool("isFlashing", false);
            ChangeCinemachineOrbit(25);
        }
    }

    void Spin()
    {     

        if (Input.GetKey(KeyCode.R))
        {
            isJumping = false;
            isSpin = true;
            isDashing = false;
            isFlashing = false;
           

            ChangeCinemachineOrbit(50);
            CinemachineFreeLook VCamControl = MyVCam.GetComponent<CinemachineFreeLook>();
            ChangeCinemachineOrbit(50);


            if (transform.position.y <= FlyAltitude)
            {
                projectileShoot.isShoot = false;
                FlyEfx.Play();
                AudioManager.instance.FlyRotateFlashRunaudioSource.mute = false;
                Vector3 velocity = transform.up * 50;
                characterController.Move(velocity * Time.deltaTime);
                Physics.gravity = Vector3.zero;
                moveVelocity.y += 0;
            }

        }
        else
        {
            
            isJumping = true;
        }

        if (isSpin)
        {
            transform.Rotate(0, RotateSpeed * Time.deltaTime, 0);
        }
        else
        {
            FlyEfx.Stop();
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
