using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProjectileShoot : MonoBehaviour
{
    [SerializeField] Transform shootPoint;
    [SerializeField] GameObject bulletPrefab;
    [SerializeField] float bulletForce;
    [SerializeField] ParticleSystem muzzleFlash;
    [SerializeField] float actualShootDelay;
    float shootDelay = 0;
    private PlayerController pc;
    public bool isShoot = false;

    private void Start()
    {
       pc = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerController>();
    }

    private void Update()
    {
        shootDelay -= Time.deltaTime;

        if (isShoot == true)
        {
            if (Input.GetButtonDown("Fire1"))
            {
                
                if (shootDelay <= 0)
                {
                    Invoke("shoot", .2f);
                    pc.PlayerAnim.SetBool("isShooting", true);
                    shootDelay = actualShootDelay;
                    transform.position = Vector3.zero;
                    muzzleFlash.Play();
                }

            }
            else
            {

                 pc.PlayerAnim.SetBool("isShooting", false);
            }
        }

    }

    void shoot()
    {
        AudioManager.instance.audioSource.PlayOneShot(AudioManager.instance.ShootSfx);
        GameObject bullet = Instantiate(bulletPrefab, shootPoint.position, shootPoint.rotation) as GameObject;
        bullet.GetComponent<Rigidbody>().velocity = shootPoint.forward * bulletForce;
        Destroy(bullet, 5);

    }
}

