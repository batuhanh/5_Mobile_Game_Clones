using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EventManager : MonoBehaviour
{
    public delegate void MagnifiyingGlassSelected();
    public static event MagnifiyingGlassSelected magnifiyingGlassSelected;

    public delegate void UVLightSelected();
    public static event UVLightSelected uvLightSelected;

    public delegate void MoneyDroppedToTarget();
    public static event MoneyDroppedToTarget moneyDroppedToTarget;

    public delegate void levelStarted();
    public static event levelStarted myLevelStarted;

    public void CallMagnifiyingGlassSelectedEvent()
    {
        if (magnifiyingGlassSelected != null)
            magnifiyingGlassSelected();
    }
    public void CallUVLightSelectedEvent()
    {
        if (uvLightSelected != null)
            uvLightSelected();
    }
    public void CallLevelStartedEvent()
    {
        if (myLevelStarted != null)
            myLevelStarted();
    }


}



