using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using Cinemachine;

public class camManager : MonoBehaviour,IDragHandler,IPointerDownHandler,IPointerUpHandler
{
    private Image camControlImg;
    Vector2 camInput;
    [SerializeField] CinemachineFreeLook[] TpsCam;
    string XInputAxis = "Mouse X", YInputAxis = "Mouse Y";
    private void Start()
    {
        camControlImg = GetComponent<Image>();
    }

    public void OnDrag(PointerEventData eventData)
    {
        if(RectTransformUtility.ScreenPointToLocalPointInRectangle(camControlImg.rectTransform,eventData.position,eventData.pressEventCamera,out camInput))
        {
            Debug.Log(camInput);
            for (int i = 0; i < TpsCam.Length; i++)
            {
                TpsCam[i].m_XAxis.m_InputAxisName = XInputAxis;
                TpsCam[i].m_YAxis.m_InputAxisName = YInputAxis;
            }
          

          
        }
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        OnDrag(eventData);
    }

    public void OnPointerUp(PointerEventData eventData)
    {
        for (int i = 0; i < TpsCam.Length; i++)
        {
            TpsCam[i].m_XAxis.m_InputAxisName = null;
            TpsCam[i].m_YAxis.m_InputAxisName = null;
            TpsCam[i].m_XAxis.m_InputAxisValue = 0;
            TpsCam[i].m_YAxis.m_InputAxisValue = 0;
        }

      
    }
}
