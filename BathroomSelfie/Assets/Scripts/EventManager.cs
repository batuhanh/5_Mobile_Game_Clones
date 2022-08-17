using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*
 * This is my EventManager Class, It holds all of the event that this 
 * project has. So it is became to more easy to understand Events of project.
 */
public class EventManager : MonoBehaviour
{
    public delegate void LevelStarted();
    public static event LevelStarted myLevelStarted;

    public delegate void LevelCompleted();
    public static event LevelCompleted myLevelCompleted;

    public delegate void LevelFailed();
    public static event LevelFailed myLevelFailed;

    public delegate void FingerSwiped(SwipeDirection swipeDirection);
    public static event FingerSwiped fingerSwiped;

    public delegate void TrueDirectionSwiped();
    public static event TrueDirectionSwiped trueDirectionSwiped;

    public delegate void WrongDirectionSwiped();
    public static event WrongDirectionSwiped wrongDirectionSwiped;

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

    public void CallFingerSwipedEvent(SwipeDirection swipeDirection)
    {
        if (fingerSwiped != null)
            fingerSwiped(swipeDirection);
    }

    public void CallTrueDirectionSwipedEvent()
    {
        if (trueDirectionSwiped != null)
            trueDirectionSwiped();
    }
    public void CallWrongDriectionSwipedEvent()
    {
        if (wrongDirectionSwiped != null)
            wrongDirectionSwiped();
    }



}



