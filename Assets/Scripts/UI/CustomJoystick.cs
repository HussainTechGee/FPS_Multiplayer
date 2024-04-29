using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.InputSystem.Layouts;
using UnityEngine.InputSystem.OnScreen;
using UnityEngine.Serialization;
using UnityEngine.UIElements;


public class CustomJoystick : OnScreenControl
{
    [SerializeField]
    bool moveAsCursor;
    private Vector2 m_PreviousMousePosition;
    private bool isDragging = false;
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
/*        Debug.Log("Pointer down detected");
        if (eventData == null)
            throw new System.ArgumentNullException(nameof(eventData));
        else
            RectTransformUtility.ScreenPointToLocalPointInRectangle(transform.parent.GetComponentInParent<RectTransform>(), eventData.position, eventData.pressEventCamera, out m_PointerDownPos);*/


        if (eventData == null)
            throw new System.ArgumentNullException(nameof(eventData));

        RectTransformUtility.ScreenPointToLocalPointInRectangle(
            transform.parent.GetComponentInParent<RectTransform>(),
            eventData.position,
            eventData.pressEventCamera,
            out var position);

        m_PreviousMousePosition = position;
        isDragging = true;
    }

    public void OnDrag(PointerEventData eventData)
    {
        if (!isDragging)
            return;
        if (eventData == null)
            throw new System.ArgumentNullException(nameof(eventData));

        if (moveAsCursor)
        {
            RectTransformUtility.ScreenPointToLocalPointInRectangle(
                       transform.parent.GetComponentInParent<RectTransform>(),
                       eventData.position,
                       eventData.pressEventCamera,
                       out var position);

            var delta = position - m_PreviousMousePosition;
            m_PreviousMousePosition = position;

            delta = Vector2.ClampMagnitude(delta, movementRange);

            // Adjust the speed factor as needed (e.g., 0.1f for slower movement)
            float speedFactor = 10f;
            delta *= speedFactor;

            ((RectTransform)transform).anchoredPosition += (Vector2)delta;

            var newPos = new Vector2(delta.x / movementRange, delta.y / movementRange);
            SendValueToControl(newPos);
        }


        else
        {
            RectTransformUtility.ScreenPointToLocalPointInRectangle(transform.parent.GetComponentInParent<RectTransform>(), eventData.position, eventData.pressEventCamera, out var position);
            var delta = position - m_PointerDownPos;

            delta = Vector2.ClampMagnitude(delta, movementRange);
            ((RectTransform)transform).anchoredPosition = m_StartPos + (Vector3)delta;

            var newPos = new Vector2(delta.x / movementRange, delta.y / movementRange);
            Debug.Log(newPos);
            SendValueToControl(newPos);
        }
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
