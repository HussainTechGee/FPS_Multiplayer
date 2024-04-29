using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MyGameManager : MonoBehaviour
{
    public static MyGameManager instance;
    public List<GameObject> AllPlayersObj = new List<GameObject>();
    // Start is called before the first frame update
    void Awake()
    {
        if(!instance)
        {
            instance = this;
        }
    }
    private void OnEnable()
    {
        //EnableAllPlayers();
        GameUIScript.instance.LoadingPanel.SetActive(false);
    }

    public void EnableAllPlayers()
    {
        GameObject[] AllPlayersObj = GameObject.FindGameObjectsWithTag("Player");
        foreach(GameObject Obj in AllPlayersObj)
        {
            Obj.SetActive(true);
        }
    }

}
