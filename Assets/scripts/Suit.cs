using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Suit : MonoBehaviour
{

    [SerializeField] GameObject collectEfx;

    private void Update()
    {
        transform.Rotate(Vector3.up * 20f * Time.deltaTime);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            
            AudioManager.instance.audioSource.PlayOneShot(AudioManager.instance.CollectSfx);
            GameObject Collectfx = Instantiate(collectEfx, this.transform.position, Quaternion.identity) as GameObject;
            Destroy(Collectfx, 2f);
            Destroy(this.gameObject);
        }
    }
}
