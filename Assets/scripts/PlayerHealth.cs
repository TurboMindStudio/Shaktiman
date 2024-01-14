using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class PlayerHealth : MonoBehaviour
{
    public int health = 100;
    public TextMeshProUGUI healthText;
    public Animator PlayerAnim;

    public ProjectileShoot projectileShoot;
    public void Update()
    {
       
    }
    public void deductHealth(int deductHealth)
    {
        health -= deductHealth;
        healthText.text = health.ToString();

        if(health <= 0)
        {
            health=0;
            healthText.text = health.ToString();
            PlayerAnim.SetTrigger("death");
            GameManager.Instance.isEnemyAttacking = false;
            kilvish kilvishAnim=GameObject.FindGameObjectWithTag("kilvish").GetComponent<kilvish>();
            kilvishAnim.animator.SetTrigger("dance");
            projectileShoot.isShoot = false;
            PlayerController playerController=GameObject.FindGameObjectWithTag("Player").GetComponentInParent<PlayerController>(); 
            playerController.canControl = false;
           //playerController.characterController.height = 2;
            Debug.Log("the End");
        }
        else if (health <= 60)
        {
            healthText.color = Color.red;
        }
    }
}
