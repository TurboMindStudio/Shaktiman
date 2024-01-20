using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Suit : MonoBehaviour
{

    [SerializeField] GameObject collectEfx;

    private void Update()
    {
        transform.Rotate(Vector3.up * 30f * Time.deltaTime);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            GameManager.Instance.CharacterAvaters[1].SetActive(true);
            GameManager.Instance.CharacterAvaters[0].SetActive(false);
            GameManager.Instance.bots.SetActive(true);
            GameManager.Instance.kilvish.SetActive(true);
            GameManager.Instance.powerPanel.SetActive(true);
            AudioManager.instance.ShaktimanBgm.enabled=true;
            AudioManager.instance.audioSource.PlayOneShot(AudioManager.instance.CollectSfx);
            GameObject Collectfx = Instantiate(collectEfx, this.transform.position, Quaternion.identity) as GameObject;
            Destroy(Collectfx, 2f);
            Destroy(this.gameObject);

            UiManager.instance.updateInfoText("Andhera Kayam Rahe !!");
            UiManager.instance.StartCoroutine(UiManager.instance.disapparText());

            //UiManager.instance.PowerUiButtons.SetActive(true);
            
            
        }
    }
}
