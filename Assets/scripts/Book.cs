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
            UiManager.instance.chakrasScorePng.SetActive(true);
            Destroy(gameObject);
            UiManager.instance.haveBook = true;
            AudioManager.instance.audioSource.PlayOneShot(AudioManager.instance.equipSfx);
            UiManager.instance.updateInfoText("Collect 7 chakras to open cave");
            UiManager.instance.StartCoroutine(UiManager.instance.disapparText());
        }
    }
}
