using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using TMPro;
public class GameUIScript : MonoBehaviour
{
    public static GameUIScript instance;

    public GameObject LobbyPanel, RoomPanel, LoadingPanel;
    public Button StartGameButton;
    public TMP_Text RoomPanelNameText, RoomPanelPlayerCountText, RoomPanelTimeText;
    private void Awake()
    {
        if (!instance)
        {
            instance = this;
        }

    }
    private void OnEnable()
    {
        //RoomScreen();
    }
    public void RoomScreen()
    {
        RoomPanel.SetActive(true);
        LoadingPanel.SetActive(false);
    }

    public void BackToHomeScene()
    {

    }

    public void GameStartClick()
    {
        RoomPanel.SetActive(false);
        LoadingPanel.SetActive(false);
        //if (FusionConnection.instance.runner.IsSceneAuthority)
        //{
        //    Debug.Log("Game End");

        //    FusionConnection.instance.runner.LoadScene(Fusion.SceneRef.FromIndex(1), LoadSceneMode.Additive);
        //}
    }
}
