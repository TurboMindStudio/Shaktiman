using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyHealth : MonoBehaviour
{
    public int health;


    public void deducthealth(int deducthealth)
    {
        health -= deducthealth;
        Debug.Log(health);
        if(health <= 0)
        {
            Destroy(this.gameObject);
        }
    }
}
