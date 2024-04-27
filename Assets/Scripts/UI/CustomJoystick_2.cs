using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.InputSystem.Layouts;
using UnityEngine.InputSystem.OnScreen;
using UnityEngine.Serialization;
using UnityEngine.UIElements;


public class CustomJoystick_2 : OnScreenControl
{
    protected override string controlPathInternal
    {
        get => m_ControlPath;
        set => m_ControlPath = value;
    }

    private Camera m_MainCamera;

    private void Awake()
    {

        m_MainCamera = Camera.main;//GameObject.FindGameObjectWithTag(Constants.MainCamera).GetComponent<Camera>();
    }
    private void Start()
    {
        m_StartPos = ((RectTransform)transform).anchoredPosition;
    }
    public void OnPointerDown(PointerEventData eventData)
    {
        Debug.Log("Pointer down detected");
        if (eventData == null)
            throw new System.ArgumentNullException(nameof(eventData));
        else
            RectTransformUtility.ScreenPointToLocalPointInRectangle(transform.parent.GetComponentInParent<RectTransform>(), eventData.position, eventData.pressEventCamera, out m_PointerDownPos);
    }

    public void OnDrag(PointerEventData eventData)
    {
        if (eventData == null)
            throw new System.ArgumentNullException(nameof(eventData));

        RectTransformUtility.ScreenPointToLocalPointInRectangle(transform.parent.GetComponentInParent<RectTransform>(), eventData.position, eventData.pressEventCamera, out var position);
        var delta = position - m_PointerDownPos;
        m_PointerDownPos = position;
        delta = Vector2.ClampMagnitude(delta, movementRange);
        ((RectTransform)transform).anchoredPosition = m_StartPos + (Vector3)delta;

        var newPos = new Vector2(delta.x / movementRange, delta.y / movementRange);
        SendValueToControl(newPos);
    
    }

    public void OnPointerUp(PointerEventData eventData)
    {
        ((RectTransform)transform).anchoredPosition = m_StartPos;
        SendValueToControl(Vector2.zero);
    }


    public float movementRange
    {
        get => m_MovementRange;
        set => m_MovementRange = value;
    }

    [FormerlySerializedAs("movementRange")]
    [SerializeField]
    private float m_MovementRange = 50;

    [InputControl(layout = "Vector2")]
    [SerializeField]
    private string m_ControlPath;

    private Vector3 m_StartPos;
    private Vector2 m_PointerDownPos;


}
