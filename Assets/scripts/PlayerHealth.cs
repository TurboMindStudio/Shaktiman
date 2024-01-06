using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class PlayerHealth : MonoBehaviour
{
    public int health = 100;
    public TextMeshProUGUI healthText;


    public void deductHealth(int deductHealth)
    {
        health -= deductHealth;
        healthText.text = health.ToString();

        if(health <= 0)
        {
            health=0;
            healthText.text = health.ToString();
            Debug.Log("the End");
        }
        else if (health <= 60)
        {
            healthText.color = Color.red;
        }
    }
}
