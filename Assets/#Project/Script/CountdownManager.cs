using Fusion;
using UnityEngine;
using UnityEngine.UI;

public class CountdownManager : NetworkBehaviour
{
    private float currentTime = 0f;
    private bool countdownStarted = false;
    
    [Networked, OnChangedRender(nameof(TimeUpdate))]
    public float countdownTime { get; set; } = 120;

    private void Start()
    {
        if(FusionConnection.instance.runner.IsSharedModeMasterClient)
        {
            countdownStarted = true;
            currentTime = countdownTime;
        }
        
    }

    void TimeUpdate()
    {
        Debug.Log($"Health changed to: {countdownTime}");
        GameUIScript.instance.RoomPanelTimeText.text = countdownTime.ToString();
        if(countdownTime<=0)
        {
            GameUIScript.instance.GameStartClick();
        }
    }

    [Rpc(RpcSources.All, RpcTargets.All)]
    public void DealDamageRpc(int RemainingTime)
    {
        // The code inside here will run on the client which owns this object (has state and input authority).
        Debug.Log("Received DealDamageRpc on StateAuthority, modifying Networked variable");
         countdownTime = RemainingTime;
        
    }


    void Update()
    {
        if (countdownStarted)
        {
            currentTime -= Time.deltaTime;
            if (currentTime <= 0)
            {
                currentTime = 0;
                countdownStarted = false;
                // Perform actions when countdown ends
                Debug.Log("Countdown ended!");
                
            }
            int seconds = (int)currentTime;
            DealDamageRpc(seconds);
        }
    }

  
}