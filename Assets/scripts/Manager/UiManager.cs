using System.Collections;
using System.Collections.Generic;
using System.Net.Http.Headers;
using Unity.VisualScripting;
using UnityEngine;

public class UiManager : MonoBehaviour
{
    public static UiManager instance;
    public bool haveBook;
    public bool isBookOpen;
    public GameObject BookPanel;
    public GameObject titlePanel;
    public GameObject UiPanel;
    public GameObject bookPng;
    public ProjectileShoot projectileShoot;
    public Gangadhar gangadhar;
    private void Awake()
    {
        instance = this;
    }

    private void Start()
    {
        haveBook = false;
        titlePanel.SetActive(true);
        bookPng.SetActive(false);
        UiPanel.SetActive(false);
    }

    private void Update()
    {
        if (haveBook)
        {
            GameManager.Instance.chakrasObj.SetActive(true);
            bookPng.SetActive(true);
            if(Input.GetKeyDown(KeyCode.B) && !isBookOpen)
            {
                isBookOpen = !isBookOpen;
                BookPanel.SetActive(true);
                projectileShoot.isShoot = false;
                AudioManager.instance.audioSource.PlayOneShot(AudioManager.instance.bookSfx);
            }
            else if(Input.GetKeyDown(KeyCode.B) && isBookOpen)
            {
                isBookOpen = !isBookOpen;
                BookPanel.SetActive(false);
                projectileShoot.isShoot = true;
                AudioManager.instance.audioSource.PlayOneShot(AudioManager.instance.bookSfx);

            }
        }
    }

    public void StartGame()
    {
       gangadhar.canControl = true;
        titlePanel.SetActive(false);
        UiPanel.SetActive(true);
        AudioManager.instance.audioSource.PlayOneShot(AudioManager.instance.clickSfx);
    }
}
