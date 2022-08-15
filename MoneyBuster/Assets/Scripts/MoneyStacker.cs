using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoneyStacker : MoneyPlacementObject
{
    private bool willWin = false;
    public void CheckMoney(MoneyState moneyState)
    {
        myAnim.SetTrigger("AddMoney");
        if (moneyState==MoneyState.GreenNormal || moneyState == MoneyState.PurpleNormal)
        {
            willWin = true;
        }
        else
        {
            willWin = false;
        }
        StartCoroutine(CheckForWin());
    }
    private IEnumerator CheckForWin()
    {
        
        yield return new WaitForSeconds(1f);
       
        if (willWin)
        {
            //win
            Debug.Log("Level Win");
            myAnim.SetTrigger("LevelCompleted");
            eventManager.CallLevelCompletedEvent();
        }
        else
        {
            //fail
            Debug.Log("Level Failed");
            eventManager.CallLevelFailedEvent();
        }
    }
    private void StartEffectOfShreder()
    {
        myAnim.SetTrigger("LevelStarted");
    }
    void OnEnable()
    {
        EventManager.myLevelStarted += StartEffectOfShreder;

    }
    void OnDisable()
    {
        EventManager.myLevelStarted -= StartEffectOfShreder;

    }
}
