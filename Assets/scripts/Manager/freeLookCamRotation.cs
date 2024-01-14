using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;
using UnityEngine.EventSystems;
using Cinemachine;

public class freeLookCamRotation : MonoBehaviour//IDragHandler,IPointerDownHandler, IPointerUpHandler
{

   // public Image imageCamControlArea;
  //  string strMouseX = "Mouse X", strMouseY = "Mouse Y";
    public CinemachineFreeLook[] cinemachinefreelook;

    private float flcamSensitivity;
    public Slider flcamsensitivitySlider;

    float speedX;
    float speedY;
    void Start()
    {
        flcamsensitivitySlider.onValueChanged.AddListener(lookSensitivity);

        flcamSensitivity = 0.6f;
        for (int i = 0; i < cinemachinefreelook.Length; i++)
        {
            speedX = cinemachinefreelook[i].m_XAxis.m_MaxSpeed;
            speedY = cinemachinefreelook[i].m_YAxis.m_MaxSpeed;
            cinemachinefreelook[i].m_XAxis.m_MaxSpeed = speedX * flcamSensitivity;
            cinemachinefreelook[i].m_YAxis.m_MaxSpeed = speedY * flcamSensitivity;
        }
    }
    /*  public void OnDrag(PointerEventData eventData)
      {
           if(RectTransformUtility.ScreenPointToLocalPointInRectangle(imageCamControlArea.rectTransform,eventData.position,eventData.enterEventCamera,out Vector2 posOut))
           {
               Debug.Log(posOut);
               cinemachinefreelook.m_XAxis.m_InputAxisName = strMouseX;
               cinemachinefreelook.m_YAxis.m_InputAxisName = strMouseY;

           }
      }

       public void OnPointerDown(PointerEventData eventData)
       {
           OnDrag(eventData);
       }

       public void OnPointerUp(PointerEventData eventData)
       {
           cinemachinefreelook.m_XAxis.m_InputAxisName = null;
           cinemachinefreelook.m_YAxis.m_InputAxisName = null;
           cinemachinefreelook.m_XAxis.m_InputAxisValue = 0;
           cinemachinefreelook.m_YAxis.m_InputAxisValue = 0;
       }
    */

    public void lookSensitivity(float sensitivity)
    {
        flcamSensitivity = sensitivity;
        Debug.Log(flcamSensitivity);
    }

    public void setflCam()
    {
       for (int i = 0;i < cinemachinefreelook.Length;i++)
       {
            cinemachinefreelook[i].m_XAxis.m_MaxSpeed = speedX * flcamSensitivity;
            cinemachinefreelook[i].m_YAxis.m_MaxSpeed = speedY * flcamSensitivity;
            AudioManager.instance.audioSource.PlayOneShot(AudioManager.instance.clickSfx);
       }
    }
}
