using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class virtualJoystick : MonoBehaviour,IDragHandler,IPointerDownHandler,IPointerUpHandler
{
    private Image joystickBg;
    private Image joystickThumb;
    private Vector2 posInput;

    private void Start()
    {
        joystickBg = GetComponent<Image>();
        joystickThumb = transform.GetChild(0).GetComponent<Image>();
    }

    public void OnDrag(PointerEventData eventData)
    {
        if(RectTransformUtility.ScreenPointToLocalPointInRectangle(joystickBg.rectTransform,eventData.position,eventData.pressEventCamera,out posInput))
        {
            posInput.x=posInput.x/(joystickBg.rectTransform.sizeDelta.x);
            posInput.y=posInput.y/(joystickBg.rectTransform.sizeDelta.y);
            Debug.Log(posInput.x.ToString() + "/" + posInput.y.ToString());

            if(posInput.magnitude>1)
            {
                posInput = posInput.normalized;
            }

            joystickThumb.rectTransform.anchoredPosition=new Vector2(posInput.x * (joystickBg.rectTransform.sizeDelta.x)/2, posInput.y * (joystickBg.rectTransform.sizeDelta.x)/2);
        }
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        OnDrag(eventData);
    }

    public void OnPointerUp(PointerEventData eventData)
    {
        posInput = Vector2.zero;
        joystickThumb.rectTransform.anchoredPosition = Vector2.zero;
    }

    public float inputHorizontal()
    {
        if (posInput.x != 0)
            return posInput.x;
        else
            return Input.GetAxis("Horizontal");
    }

    public float InputVertical()
    {
        if (posInput.y != 0)
            return posInput.y;
        else
            return Input.GetAxis("Vertical");
    }
}
