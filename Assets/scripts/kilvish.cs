using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class kilvish : MonoBehaviour
{
    private NavMeshAgent m_Agent;
    private Transform player;

    private float velocity;
    [SerializeField] float smoothTime;
    [HideInInspector]public Animator animator;


    //attack
    [Header("attack")]
    [SerializeField] GameObject fireBallPrefab;
    [SerializeField] Transform fireShootPoint;
    [SerializeField] float shootForce;

    private float firerateTime;
    [SerializeField] float rateOfFire;
    private void Start()
    {
        m_Agent = GetComponent<NavMeshAgent>();
        animator=GetComponent<Animator>();
        GameManager.Instance.isEnemyAttacking = true;
    }


    private void Update()
    {
        player=GameObject.FindGameObjectWithTag("Player").GetComponent<Transform>();

        FollowPlayer();
    }


    void FollowPlayer()
    {
       
        float distanceBetween_Player_Enmey = Vector3.Distance(this.transform.position, player.position);
        Vector3 direction = (player.position - transform.position).normalized;
        float angle = Mathf.Atan2(direction.x, direction.z) * Mathf.Rad2Deg;
        float targetAngle = Mathf.SmoothDampAngle(transform.eulerAngles.y, angle, ref velocity, smoothTime * Time.deltaTime);
        transform.rotation = Quaternion.Euler(0, targetAngle, 0);

        if(distanceBetween_Player_Enmey <= 70)
        {
            if (GameManager.Instance.isEnemyAttacking)
            {
                attack();
            }
           
            m_Agent.speed = 0;
        }
        else if(distanceBetween_Player_Enmey >= 70)
        {
            m_Agent.destination = player.position;
            animator.SetBool("shoot", false);
            m_Agent.speed = 15;
        }
    }

    void attack()
    {
        animator.SetBool("shoot", true);

        firerateTime -= Time.deltaTime;

        if (firerateTime <= 0)
        {
            AudioManager.instance.audioSource.PlayOneShot(AudioManager.instance.ShootSfx);
            GameObject fire = Instantiate(fireBallPrefab, fireShootPoint.position, Quaternion.identity) as GameObject;
            Destroy(fire, 20);
            firerateTime = rateOfFire;
        }
    }


}
