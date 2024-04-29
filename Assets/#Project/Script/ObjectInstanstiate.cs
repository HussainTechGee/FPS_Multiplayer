using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectInstanstiate : MonoBehaviour
{
    public GameObject Player;
    void Start()
    {
        Instantiate(Player, Vector3.zero,Quaternion.identity);
    }

}
