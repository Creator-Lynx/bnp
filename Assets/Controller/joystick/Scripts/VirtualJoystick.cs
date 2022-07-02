using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class VirtualJoystick : MonoBehaviour, IDragHandler, IPointerUpHandler, IPointerDownHandler
{
    [SerializeField] private Image JoystickLayout;
    [SerializeField] private Image Joystick;
    public Vector2 JoystickOutput { get { return joystickOutput; } }
    private Vector2 joystickOutput;
    private Vector2 joystickOutputRaw;

    private void Update()
    {
        Joystick.rectTransform.localPosition = joystickOutputRaw;
        //Debug.Log(joystickInput);
    }

    public void OnDrag(PointerEventData eventData)
    {
        if (RectTransformUtility.ScreenPointToLocalPointInRectangle
            (
                JoystickLayout.rectTransform,
                eventData.position,
                eventData.pressEventCamera,
                out joystickOutputRaw
            ))
        {
            var radius = JoystickLayout.rectTransform.sizeDelta.x / 2;
            var x = joystickOutputRaw.x;
            var y = joystickOutputRaw.y;

            if (x * x + y * y > radius * radius)
            {
                joystickOutputRaw = joystickOutputRaw.normalized * radius;
            }

            joystickOutput.x = joystickOutputRaw.x / radius;
            joystickOutput.y = joystickOutputRaw.y / radius;
        }
    }

    public void OnPointerUp(PointerEventData eventData)
    {
        joystickOutput = Vector2.zero;
        joystickOutputRaw = Vector2.zero;
    }

    public void OnPointerDown(PointerEventData eventData)
    {

    }
}
