using System.Collections;
using System.Collections.Generic;
using System.Net.Http.Headers;
using Unity.VisualScripting;
using UnityEngine;
using Cinemachine;
using UnityEngine.UI;
using TMPro;

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

    public TextMeshProUGUI infoText;

    public GameObject chakrasScorePng;
    public TextMeshProUGUI chakrasScoreText;

    //camera setting;
   /* [Header("Camera Settings")]
    [SerializeField] CinemachineFreeLook[] cinemachineFreeLook;
    [Range(0f,1000f)]
    [SerializeField] int Xsensitivity;
    [Range(0f, 100f)]
    [SerializeField] int Ysensitivity;
    public bool XInvert;
    public bool YInvert;
    public Slider XSenstivitySlider;
    public Slider YSenstivitySlider;
    public TextMeshProUGUI XsensitivityText;
    public TextMeshProUGUI YsensitivityText;
    public Toggle XInvertToggle;
    public Toggle YInvertToggle; */

    public GameObject settingPanel;
    public freeLookCamRotation flControl;
  
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
        
        updateInfoText(string.Empty);
        chakrasScorePng.SetActive(false);
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
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
        infoText.gameObject.SetActive(false);
    }

    public void BookManager()
    {
        if (haveBook)
        {
            GameManager.Instance.chakrasObj.SetActive(true);
            bookPng.SetActive(true);
            if (Input.GetKeyDown(KeyCode.B) && !isBookOpen)
            {
                isBookOpen = !isBookOpen;
                BookPanel.SetActive(true);
                projectileShoot.isShoot = false;
                AudioManager.instance.audioSource.PlayOneShot(AudioManager.instance.bookSfx);
                Cursor.lockState = CursorLockMode.Confined;
                Cursor.visible = true;
            }
            else if (Input.GetKeyDown(KeyCode.B) && isBookOpen)
            {
                isBookOpen = !isBookOpen;
                BookPanel.SetActive(false);
                projectileShoot.isShoot = true;
                AudioManager.instance.audioSource.PlayOneShot(AudioManager.instance.bookSfx);
                Cursor.lockState = CursorLockMode.Locked;
                Cursor.visible = false;

            }
        }
    }

   /* public void cameraSetting()
    {
        cinemachineFreeLook.m_XAxis.m_MaxSpeed = Xsensitivity;
        cinemachineFreeLook.m_YAxis.m_MaxSpeed = Ysensitivity;

        cinemachineFreeLook.m_XAxis.m_InvertInput = XInvert;
        cinemachineFreeLook.m_YAxis.m_InvertInput = YInvert;

        XSenstivitySlider.value = Xsensitivity;
        YSenstivitySlider.value = Ysensitivity;
        XsensitivityText.text=Xsensitivity.ToString();
        YsensitivityText.text=Ysensitivity.ToString();

        XInvertToggle.isOn = XInvert;
        YInvertToggle.isOn = YInvert;

    }
   */
   

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
}
