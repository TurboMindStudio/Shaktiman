using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyNavAi : MonoBehaviour
{
    //patrol
    [Header("Patrol")]
    [SerializeField] NavMeshAgent nva;
    [SerializeField] Transform[] patrolPoint;
    private int randomPosition;
    [SerializeField] int currentIndex;

    //chasing
    [Header("chasing")]
    Transform player;
    public float smoothTime;
    float velocity;

    //attack
    [Header("attack")]
    [SerializeField] GameObject fireBallPrefab;
    [SerializeField] Transform fireShootPoint;
    [SerializeField] float shootForce;
    

    private float firerateTime;
    [SerializeField] float rateOfFire;
  
    void Start()
    {

        detectRandomPosition();
        GameManager.Instance.isEnemyAttacking = true;
        //player = GameObject.FindGameObjectWithTag("Player").GetComponent<Transform>();
    }

    // Update is called once per frame
    void Update()
    {
        patrol();
        Chase();
       
    }

    void patrol()
    {
        float disteanceBeetween_Npc_PatrolPoint = Vector3.Distance(this.transform.position,patrolPoint[currentIndex].transform.position);
        //Debug.Log(disteanceBeetween_Npc_PatrolPoint);
        if (disteanceBeetween_Npc_PatrolPoint < 40)
        {
            detectRandomPosition();
        }

    }

    void Chase()
    {
        Transform player=GameObject.FindGameObjectWithTag("Player").GetComponent<Transform>()as Transform;
        float distanceBetween_Player_Enmey = Vector3.Distance(this.transform.position, player.position);
        // Debug.Log(distanceBetween_Player_Enmey);
        
        Vector3 direction = (player.position - transform.position).normalized;
      //  Debug.DrawLine(player.position, transform.position, Color.red);

        if (distanceBetween_Player_Enmey <= 60)
        {
            float angle = Mathf.Atan2(direction.x, direction.z) * Mathf.Rad2Deg;
            float targetAngle = Mathf.SmoothDampAngle(transform.eulerAngles.y, angle, ref velocity, smoothTime * Time.deltaTime);
            transform.rotation = Quaternion.Euler(0, targetAngle, 0);
            nva.destination = player.position;

            fireBall newFireball=fireBallPrefab.GetComponent<fireBall>();
            newFireball.moveTowardPlayer();
            if (GameManager.Instance.isEnemyAttacking)
            {
                attack();
            }
           
        }
        else if(distanceBetween_Player_Enmey >= 70)
        {
            nva.destination = patrolPoint[currentIndex].position;
        }
        
    }

    void detectRandomPosition()
    {
        randomPosition = Random.Range(0, patrolPoint.Length);
        currentIndex = randomPosition;
        nva.destination = patrolPoint[randomPosition].transform.position;
       
    }

    void attack()
    {
        firerateTime -= Time.deltaTime;

        if( firerateTime <= 0)
        {
            AudioManager.instance.audioSource.PlayOneShot(AudioManager.instance.ShootSfx);
            GameObject fire = Instantiate(fireBallPrefab, fireShootPoint.position, Quaternion.identity) as GameObject;
            Destroy(fire, 20);
            firerateTime = rateOfFire;
        }

        
    }

}
