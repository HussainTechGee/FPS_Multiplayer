using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.EnhancedTouch;
using UnityEngine.InputSystem.Layouts;
using UnityEngine.InputSystem.OnScreen;
using UnityEngine.Serialization;


public class CameraLook : MonoBehaviour
{/*
    protected override string controlPathInternal
    {
        get => m_ControlPath;
        set => m_ControlPath = value;
    }

    private Camera m_MainCamera;
    [HideInInspector]
    public Vector2 TouchDist;
    [HideInInspector]
    public Vector2 PointerOld;
    [HideInInspector]
    protected int PointerId;
    [HideInInspector]
    public bool Pressed;

    private void Awake()
    {

        m_MainCamera = Camera.main;//GameObject.FindGameObjectWithTag(Constants.MainCamera).GetComponent<Camera>();
    }
    private void Start()
    {
        m_StartPos = ((RectTransform)transform).anchoredPosition;
    }


    void Update()
    {
        if (Pressed)
        {
            if (PointerId >= 0 && PointerId < Input.touches.Length)
            {
                TouchDist = Input.touches[PointerId].position - PointerOld;
                PointerOld = Input.touches[PointerId].position;
            }
            else
            {
                TouchDist = new Vector2(Touchscreen.current.primaryTouch.position.ReadValue().x, Touchscreen.current.primaryTouch.position.ReadValue().y) - PointerOld;
                PointerOld = Input.mousePosition;
            }
        }
        else
        {
            TouchDist = new Vector2();

        }
        Debug.Log(TouchDist);
        SendValueToControl(TouchDist);
    }

   

    public void OnPointerDown(PointerEventData eventData)
    {
        Pressed = true;
        PointerId = eventData.pointerId;
        PointerOld = eventData.position;













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
        Pressed = false;





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

*/


    public InputActionReference inputAction;
    public RectTransform restrictedArea;

    private void OnEnable()
    {
        inputAction.action.Enable();
    }

    private void OnDisable()
    {
        inputAction.action.Disable();
    }

    private void Update()
    {
        Vector2 rawInput = inputAction.action.ReadValue<Vector2>();
        Vector2 clampedInput = new Vector2(
            Mathf.Clamp(rawInput.x, restrictedArea.rect.xMin, restrictedArea.rect.xMax),
            Mathf.Clamp(rawInput.y, restrictedArea.rect.yMin, restrictedArea.rect.yMax)
        );

        // Do something with the clampedInput, e.g., move the player, camera, etc.
        // For demonstration purposes, we'll just print the clamped input.
        Debug.Log("Clamped Input: " + clampedInput);
 
    }

   


}
