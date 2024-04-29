using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Fusion;

public class PlayerMove : NetworkBehaviour
{
    [SerializeField]
    private GameObject playerCamera;
    private CharacterController controller;
    
    private void Start()
    {
        controller = GetComponent<CharacterController>();
    }
    public override void Spawned()
    {
        if (HasInputAuthority)
        {
            GameObject Cam = GameObject.FindGameObjectWithTag("FirstCamera");
            if (Cam)
            {
                Cam.SetActive(false);
                playerCamera.SetActive(true);
            }
        }

    }
    public override void FixedUpdateNetwork()
    {
        if(controller!=null)
        {
            if (GetInput(out NetInput input))
            {
                controller.Move(input.Direction*5);
                Debug.Log("Get: " +gameObject.name+" : "+ input.Direction);
                // PreviousButtons = input.Buttons;
            }
        }
        
    }
}