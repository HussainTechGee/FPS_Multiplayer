using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SceneManagement;
public class MainUIScript : MonoBehaviour
{
    public static MainUIScript instance;

    public GameObject MainPanel, LoadingPanel;
    private void Awake()
    {
        if(!instance)
        {
            instance = this;
        }

    }
    private void OnEnable()
    {
        LoadingPanel.SetActive(false);
        MainPanel.SetActive(true);
    }

    public void MainScreenStartClick()
    {
        LoadingPanel.SetActive(true);
        SceneManager.LoadScene(1);
    }
    
}
