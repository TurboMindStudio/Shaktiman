using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class fireball : MonoBehaviour
{
    [SerializeField] GameObject hitEfx;
    [SerializeField] int deductHealth;
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Enemy"))
        {
            GameObject hitimpact = Instantiate(hitEfx, this.transform.position, Quaternion.identity) as GameObject;
            Destroy(hitimpact, 0.5f);
            Destroy(this.gameObject);

            EnemyHealth enemyHealth=GameObject.FindGameObjectWithTag("Enemy").GetComponent<EnemyHealth>();
            enemyHealth.deducthealth(deductHealth);
            
        }

        if (other.CompareTag("kilvish"))
        {
            GameObject hitimpact = Instantiate(hitEfx, this.transform.position, Quaternion.identity) as GameObject;
            Destroy(hitimpact, 0.5f);
            Destroy(this.gameObject);

            kilvishHealth enemyHealth = GameObject.FindGameObjectWithTag("kilvish").GetComponent<kilvishHealth>();
            enemyHealth.deducthealth(deductHealth);
        }

        if (other.CompareTag("Obstacles"))
        {
            GameObject hitimpact = Instantiate(hitEfx, this.transform.position, Quaternion.identity) as GameObject;
            Destroy(hitimpact, 0.5f);
            Destroy(this.gameObject);
        }
    }
}
