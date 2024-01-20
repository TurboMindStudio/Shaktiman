using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SceneManagement;

public class UiManager : MonoBehaviour
{
    public static UiManager instance;
    public bool haveBook;
    public GameObject BookPanel;
    public GameObject titlePanel;
    public GameObject UiPanel;
    public GameObject restartPanel;
    public GameObject bookPng;
    public ProjectileShoot projectileShoot;
    public Gangadhar gangadhar;

    public TextMeshProUGUI infoText;

    public GameObject chakrasScorePng;
    public TextMeshProUGUI chakrasScoreText;

    public GameObject settingPanel;
    public freeLookCamRotation flControl;

    //android 

    //public GameObject PowerUiButtons;
  
    private void Awake()
    {
        instance = this;
    }

    private void Start()
    {
        haveBook = false;
        //titlePanel.SetActive(true);
        bookPng.SetActive(false);
        UiPanel.SetActive(false);
       // PowerUiButtons.SetActive(false);
        updateInfoText(string.Empty);
        chakrasScorePng.SetActive(false);
        restartPanel.SetActive(false);
    }

    private void Update()
    {
        BookManager();

       if(Input.GetKey(KeyCode.Escape))
       {
            settingPanel.SetActive(true);
            Cursor.lockState = CursorLockMode.Confined;
            Cursor.visible = true;
       }
    }

    public void StartGame()
    {
        gangadhar.canControl = true;
        titlePanel.SetActive(false);
        UiPanel.SetActive(true);
        AudioManager.instance.audioSource.PlayOneShot(AudioManager.instance.clickSfx);
       // Cursor.lockState = CursorLockMode.Locked;
       // Cursor.visible = false;
        infoText.gameObject.SetActive(false);
    }

    public void RestartGame()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    public void BookManager()
    {
        if (haveBook)
        {
            GameManager.Instance.chakrasObj.SetActive(true);
            bookPng.SetActive(true);

            if (Input.GetKeyDown(KeyCode.B))
            {
                openBook();
            }
        }
    }

    public void openSettingPanel()
    {
        settingPanel.SetActive(true);
    }

    public void closeSettingPanel()
    {
        settingPanel.SetActive(false);
        flControl.setflCam();
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }

    public void updateInfoText(string text)
    {
        
        infoText.gameObject.SetActive(true);
        infoText.text = text;
        AudioManager.instance.audioSource.PlayOneShot(AudioManager.instance.infoAuraSfx);
    }

    public IEnumerator disapparText()
    {
        yield return new WaitForSeconds(6f);
        infoText.gameObject.SetActive(false);
    }

    public void openBook()
    {
            BookPanel.SetActive(true);
            projectileShoot.isShoot = false;
            AudioManager.instance.audioSource.PlayOneShot(AudioManager.instance.bookSfx);
            Cursor.lockState = CursorLockMode.Confined;
            Cursor.visible = true; 
    }

    public void closeBook()
    {
            BookPanel.SetActive(false);
            projectileShoot.isShoot = true;
            AudioManager.instance.audioSource.PlayOneShot(AudioManager.instance.bookSfx);
            Cursor.lockState = CursorLockMode.Locked;
            Cursor.visible = false;
   
    }

    
}
