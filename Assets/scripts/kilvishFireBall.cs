using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class kilvishFireBall : MonoBehaviour
{
    [SerializeField] float speed;
    [SerializeField] GameObject hitEfx;
    Transform hitPos;
    [SerializeField] Transform shootPoint;
    private void Start()
    {
        hitPos = GameObject.FindGameObjectWithTag("hitpoint").GetComponent<Transform>();
    }

    private void Update()
    {
        moveTowardPlayer();
    }
    public void moveTowardPlayer()
    {
        Transform hitPos = GameObject.FindGameObjectWithTag("hitpoint").GetComponent<Transform>();
        transform.position = Vector3.MoveTowards(transform.position, hitPos.position, speed * Time.deltaTime);

    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {

            GameObject hitimpact = Instantiate(hitEfx, hitPos.position, Quaternion.identity) as GameObject;
            Destroy(hitimpact, 0.5f);
            AudioManager.instance.audioSource.PlayOneShot(AudioManager.instance.hurtSfx);
            Destroy(this.gameObject);

            PlayerHealth health = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerHealth>();
            health.deductHealth(Random.Range(1, 5));
        }
        if (other.CompareTag("shield"))
        {
            GameObject hitimpact = Instantiate(hitEfx, hitPos.position, Quaternion.identity) as GameObject;
            Destroy(hitimpact, 0.5f);
            Destroy(this.gameObject);
        }
    }
}
