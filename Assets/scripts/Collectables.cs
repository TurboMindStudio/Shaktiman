using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Collectables : MonoBehaviour
{
    [SerializeField] GameObject collectEfx;

    private void Update()
    {
        transform.Rotate(Vector3.forward*20f*Time.deltaTime);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            GameManager.Instance.collectedChakras++;
            AudioManager.instance.audioSource.PlayOneShot(AudioManager.instance.CollectSfx);
            GameObject Collectfx = Instantiate(collectEfx,this.transform.position,Quaternion.identity) as GameObject;
            Destroy(Collectfx, 2f);
            if (GameManager.Instance.collectedChakras == 7)
            {
                GameManager.Instance.AllChakrasCollected = true;
            }
            Destroy(this.gameObject);
        }
    }
}
