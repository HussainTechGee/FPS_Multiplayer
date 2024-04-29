using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.InputSystem.OnScreen;

public class CustomJoystickArea : MonoBehaviour, IPointerDownHandler, IDragHandler, IPointerUpHandler
{
    #region Private_Vars
    [SerializeField]
    GameObject m_JoyStick;

    [SerializeField]
    private CustomJoystick m_MainStick;

    private PointerEventData m_InitialData;
    Vector3 _InitialStickPos;
    #endregion

    #region Unity_callbacks

    private void Start()
    {
        _InitialStickPos = m_JoyStick.transform.position;
    }
/*    private void Update()
    {
        CheckInputs();
    }*/

    #endregion
    #region Private_methods
  /*  private void CheckInputs()
    {
        *//*     if (Input.GetMouseButtonUp(0) && m_Instantiated)
             {
                 m_JoyStick.gameObject.SetActive(false);
                 //Destroy(m_CurrentJoystick);
                 m_Instantiated = false;
                 m_IsClicked = false;
                 *//* if (m_CurrentJoystick != null)
                  {
                      m_JoyStick.gameObject.SetActive(false);
                      //Destroy(m_CurrentJoystick);
                      m_Instantiated = false;
                      m_IsClicked = false;
                  }*//*
             }*/
        /*  if (Input.GetMouseButton(0) && !m_Instantiated && m_IsClicked)
          {
              m_Instantiated = true;
              Vector3 position = m_InitialData.position;
              m_JoyStick.transform.position = position;
              m_JoyStick.gameObject.SetActive(true);
              m_MainStick.OnPointerDown(m_InitialData);

              // code before 16/01/23 
              *//* m_Instantiated = true;
               Vector3 position = Input.mousePosition;
               m_JoyStick.transform.position = Input.mousePosition;
               m_JoyStick.gameObject.SetActive(true);
               m_MainStick.OnPointerDown(m_InitialData);*//*
              //m_CurrentJoystick = Instantiate(m_JoyStick, position, Quaternion.identity,this.transform);
          }*//*

    }
*/
    #endregion

    #region Public_Methods


    void IPointerDownHandler.OnPointerDown(PointerEventData eventData)
    {
        m_InitialData = eventData;
 
        Vector3 position = eventData.position;// m_InitialData.position;
        m_JoyStick.transform.position = position;
        m_MainStick.OnPointerDown(m_InitialData);


    }

    void IDragHandler.OnDrag(PointerEventData eventData)
    {
        if (m_MainStick)
            m_MainStick.OnDrag(eventData);

    }

    void IPointerUpHandler.OnPointerUp(PointerEventData eventData)
    {
        m_JoyStick.transform.position = _InitialStickPos;
        if (m_MainStick)
            m_MainStick.OnPointerUp(eventData);

    }

    #endregion
}
