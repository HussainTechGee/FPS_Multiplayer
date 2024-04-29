using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Fusion;
using Fusion.Sockets;
using System;
using InfimaGames.LowPolyShooterPack;
public class FusionConnection : FusionMonoBehaviour, INetworkRunnerCallbacks
{
    public bool connectOnAwake = false;
    [HideInInspector] public NetworkRunner runner;
    [SerializeField] NetworkObject NetworkObj;

    [SerializeField] private NetworkPrefabRef playerPrefab;
    [SerializeField] Transform[] SpawnPositionList;

    private NetInput accumulatedInput;
    public Vector3 currentPos = new Vector3();
    int index;
    private Dictionary<PlayerRef, NetworkObject> Players = new Dictionary<PlayerRef, NetworkObject>();

    public static FusionConnection instance;
    void Start()
    {
        if(!instance)
        {
            instance = this;
        }
        if(connectOnAwake)
        {
            connectToRunner();
        }
    }
    public async void connectToRunner()
    {
        GameUIScript.instance.LoadingPanel.SetActive(true);
        string roomName = "MyRoom"; //+ UnityEngine.Random.Range(1000, 9999);
        if(!runner)
        {
            runner = gameObject.AddComponent<NetworkRunner>();
        }
        await runner.StartGame(new StartGameArgs()
        {
            GameMode = GameMode.Shared,
            SessionName = roomName,
            PlayerCount = 4,

        });

    }
    

    //public override void FixedUpdateNetwork()
    //{
    //    Debug.Log($"Elapsed {MyTickTimer.ElapsedTicks(runner)} ticks.");
    //    Debug.Log($"Normalized Value {MyTickTimer.NormalizedValue(runner)}.");
    //    MainUIScript.instance.RoomPanelTimeText.text = MyTickTimer.ElapsedTicks(runner).ToString();
    //    if (MyTickTimer.Expired(runner))
    //    {
    //        // Execute Logic

    //        // Reset timer
    //        MyTickTimer = default;

    //        Debug.Log("Timer Finished on tick: " + runner.SimulationTime);
    //    }
    //}

    void voidRoomEnterUIUpdate()
    {
        GameUIScript.instance.RoomPanelNameText.text = runner.SessionInfo.Name;
        GameUIScript.instance.RoomPanelPlayerCountText.text = runner.SessionInfo.PlayerCount + " / " + runner.SessionInfo.MaxPlayers;
    }
    #region Fusion Callbacks
    public void OnConnectedToServer(NetworkRunner runner)
    {  
        
        Debug.Log("OnConnectedToServer");
        if (runner.IsSharedModeMasterClient)
        {
            runner.Spawn(NetworkObj, new Vector3(0, 0, 0), Quaternion.identity);
           // GameUIScript.instance.StartGameButton.interactable = true;
        }
        //else
        //{
        //    GameUIScript.instance.StartGameButton.interactable = true;
        //}

    }
    public void OnConnectFailed(NetworkRunner runner, NetAddress remoteAddress, NetConnectFailedReason reason)
    {
         
    }

    public void OnConnectRequest(NetworkRunner runner, NetworkRunnerCallbackArgs.ConnectRequest request, byte[] token)
    {
         
    }

    public void OnCustomAuthenticationResponse(NetworkRunner runner, Dictionary<string, object> data)
    {
         
    }

    public void OnDisconnectedFromServer(NetworkRunner runner, NetDisconnectReason reason)
    {
         
    }

    public void OnHostMigration(NetworkRunner runner, HostMigrationToken hostMigrationToken)
    {
         
    }

    public void OnInput(NetworkRunner runner, NetworkInput input)
    {
        //accumulatedInput.Direction = currentPos;
        //input.Set(accumulatedInput);
        //Debug.Log("Set: " + accumulatedInput.Direction);
        // if(playerMovement!=null)
        //{
        //   // input.Set(playerMovement.GetNetworkInput());
        //}
    }

    public void OnInputMissing(NetworkRunner runner, PlayerRef player, NetworkInput input)
    {
         
    }

    public void OnObjectEnterAOI(NetworkRunner runner, NetworkObject obj, PlayerRef player)
    {
         
    }

    public void OnObjectExitAOI(NetworkRunner runner, NetworkObject obj, PlayerRef player)
    {
         
    }

    public void OnPlayerJoined(NetworkRunner runner, PlayerRef player)
    {
        Debug.Log("Player joined!");
        if (player == runner.LocalPlayer)
        {
            NetworkObject playerObject = runner.Spawn(playerPrefab, SpawnPositionList[(index % SpawnPositionList.Length)].position, Quaternion.identity, player);
            playerObject.gameObject.SetActive(true);
            Players.Add(player, playerObject);
            
            index++;
        }
        voidRoomEnterUIUpdate();
        GameUIScript.instance.RoomScreen();
    }

    public void OnPlayerLeft(NetworkRunner runner, PlayerRef player)
    {
        voidRoomEnterUIUpdate();
        if (Players.TryGetValue(player, out NetworkObject playerObject))
        {
            runner.Despawn(playerObject);
            Players.Remove(player);
        }
    }
        public void OnReliableDataProgress(NetworkRunner runner, PlayerRef player, ReliableKey key, float progress)
    {
         
    }

    public void OnReliableDataReceived(NetworkRunner runner, PlayerRef player, ReliableKey key, ArraySegment<byte> data)
    {
         
    }

    public void OnSceneLoadDone(NetworkRunner runner)
    {
         
    }

    public void OnSceneLoadStart(NetworkRunner runner)
    {
         
    }

    public void OnSessionListUpdated(NetworkRunner runner, List<SessionInfo> sessionList)
    {
         
    }

    public void OnShutdown(NetworkRunner runner, ShutdownReason shutdownReason)
    {
         
    }

    public void OnUserSimulationMessage(NetworkRunner runner, SimulationMessagePtr message)
    {
         
    }
    #endregion

  
}
