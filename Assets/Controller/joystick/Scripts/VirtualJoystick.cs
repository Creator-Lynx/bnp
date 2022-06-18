using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class VirtualJoystick : MonoBehaviour, IDragHandler, IPointerUpHandler, IPointerDownHandler
{
    [SerializeField] private Image JoystickLayout;
    [SerializeField] private Image Joystick;
    public Vector2 JoystickInput { get { return joystickInput; } }
    private Vector2 joystickInput;
    private Vector2 joystickInputRaw;
    
    private void Update()
    {
        Joystick.rectTransform.localPosition = joystickInputRaw;
        //Debug.Log(joystickInput);
    }

    public void OnDrag(PointerEventData eventData)
    {
        if (RectTransformUtility.ScreenPointToLocalPointInRectangle
            (
                JoystickLayout.rectTransform,
                eventData.position,
                eventData.pressEventCamera,
                out joystickInputRaw
            ))
        {
            var radius = JoystickLayout.rectTransform.sizeDelta.x / 2;
            var x = joystickInputRaw.x;
            var y = joystickInputRaw.y;

            if (x * x + y * y > radius * radius)
            {
                joystickInputRaw = joystickInputRaw.normalized * radius;
            }

            joystickInput.x = joystickInputRaw.x / radius;
            joystickInput.y = joystickInputRaw.y / radius;            
        }
    }

    public void OnPointerUp(PointerEventData eventData)
    {
        joystickInput = Vector2.zero;
        joystickInputRaw = Vector2.zero;
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        
    }
}
