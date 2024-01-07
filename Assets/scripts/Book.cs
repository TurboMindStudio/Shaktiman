using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Book : MonoBehaviour
{
  

    private void Update()
    {
        transform.Rotate(Vector3.up* 100 * Time.deltaTime);
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
           
            Destroy(gameObject);
            UiManager.instance.haveBook = true;
            AudioManager.instance.audioSource.PlayOneShot(AudioManager.instance.equipSfx);
           
        }
    }
}
