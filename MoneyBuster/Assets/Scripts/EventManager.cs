using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*
 * This is my EventManager Class, It holds all of the event that this 
 * project has. So it is became to more easy to understand Events of project.
 */
public class EventManager : MonoBehaviour
{
    public delegate void MagnifiyingGlassSelected();
    public static event MagnifiyingGlassSelected magnifiyingGlassSelected;

    public delegate void UVLightSelected();
    public static event UVLightSelected uvLightSelected;

    public delegate void LevelStarted();
    public static event LevelStarted myLevelStarted;

    public delegate void LevelCompleted();
    public static event LevelCompleted myLevelCompleted;

    public delegate void LevelFailed();
    public static event LevelFailed myLevelFailed;
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
    public void CallLevelCompletedEvent()
    {
        if (myLevelCompleted != null)
            myLevelCompleted();
    }
    public void CallLevelFailedEvent()
    {
        if (myLevelFailed != null)
            myLevelFailed();
    }


}



