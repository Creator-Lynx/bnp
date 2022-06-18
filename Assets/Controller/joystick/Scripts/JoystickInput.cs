using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class JoystickInput : MonoBehaviour, IPointerDownHandler, IDragHandler, IPointerUpHandler
{
    static JoystickInput instance;
    [SerializeField] RectTransform stick;
    [SerializeField] RectTransform bg;

    /// <summary>
    /// koef; equals real world distance between
    /// centre of joystick and max delta-move of stick
    /// </summary>
    [SerializeField] float handleRange = 1, deadZone = 0;
    Canvas canvas;
    Camera cam;


    [SerializeField] Vector2 input = Vector2.zero;
    void Start()
    {
        instance = this;
        canvas = GetComponentInParent<Canvas>();
        cam = Camera.main;
        position = RectTransformUtility.WorldToScreenPoint(cam, bg.position);
    }
    Vector2 position;
    public void OnPointerDown(PointerEventData eventData)
    {
        OnDrag(eventData);
    }
    public void OnDrag(PointerEventData eventData)
    {

        Vector2 radius = bg.sizeDelta / 2;

        input = (eventData.position - position) / (radius * canvas.scaleFactor);
        HandleInput();
        stick.anchoredPosition = input * radius * handleRange;
    }
    void HandleInput()
    {
        if (input.magnitude > deadZone)
        {
            if (input.magnitude > 1) input.Normalize();
        }
        else input = Vector2.zero;
    }
    public void OnPointerUp(PointerEventData eventData)
    {
        stick.anchoredPosition = Vector2.zero;
        input = Vector2.zero;
    }


    public static float GetHorizontalAxis()
    {
        return instance.input.x;
    }
    public static float GetVerticalAxis()
    {
        return instance.input.y;
    }
}
