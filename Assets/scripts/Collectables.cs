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
            UiManager.instance.chakrasScoreText.text = GameManager.Instance.collectedChakras.ToString();
            AudioManager.instance.audioSource.PlayOneShot(AudioManager.instance.CollectSfx);
            GameObject Collectfx = Instantiate(collectEfx,this.transform.position,Quaternion.identity) as GameObject;
            Destroy(Collectfx, 2f);
            if (GameManager.Instance.collectedChakras == 7)
            {
                UiManager.instance.chakrasScorePng.SetActive(false);
                GameManager.Instance.AllChakrasCollected = true;
                GameManager.Instance.caveDoorAnimator.SetTrigger("openDoor");

                UiManager.instance.updateInfoText("Cave is Opened");
                UiManager.instance.StartCoroutine(UiManager.instance.disapparText());
            }
            Destroy(this.gameObject);
        }
    }
}
