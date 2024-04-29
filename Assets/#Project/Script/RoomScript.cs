using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Fusion;
public class RoomScript : NetworkBehaviour
{
    //[Networked]
    //public FusionConnection.CustomTickTimer MyTickTimer { get; set; }
   
    private void Start()
    {
        Invoke("StartTimer",1f);
    }
    [Networked] public float Health { get; set; }

    public override void FixedUpdateNetwork()
    {
       // Health += Runner.DeltaTime * HealthRegen;
    }

    //public void StartTimer()
    //{
    //    if (!FusionConnection.instance.runner.IsClient)
    //    {
    //        MyTickTimer = FusionConnection.CustomTickTimer.CreateFromTicks(FusionConnection.instance.runner, 120);
    //        Debug.Log("Shahid Start Time");

    //    }
    //}


    //public override void FixedUpdateNetwork()
    //{
    //    if (FusionConnection.instance.runner.IsClient)
    //    {
    //        return;
    //    }
    //    Debug.Log("Shahid Update");
    //    Debug.Log($"Elapsed {MyTickTimer.ElapsedTicks(FusionConnection.instance.runner)} ticks.");
    //    Debug.Log($"Normalized Value {MyTickTimer.NormalizedValue(FusionConnection.instance.runner)}.");
    //    MainUIScript.instance.RoomPanelTimeText.text = (120- MyTickTimer.ElapsedTicks(FusionConnection.instance.runner)).ToString();
    //    if (MyTickTimer.Expired(FusionConnection.instance.runner))
    //    {
    //        // Execute Logic

    //        // Reset timer
    //        MyTickTimer = default;

    //        Debug.Log("Timer Finished on tick: " + FusionConnection.instance.runner.SimulationTime);
    //    }
    //}
}
