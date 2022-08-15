using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoneyShredder : MoneyPlacementObject
{
    private bool willWin = false;
    [SerializeField] private Animator stackerAnim;
    public void CheckMoney(MoneyState moneyState)
    {
        myAnim.SetTrigger("MoneyShredStart");
        if (moneyState == MoneyState.GreenFake || moneyState == MoneyState.PurpleFake)
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
        myAnim.SetTrigger("MoneyShredEnd");
        if (willWin)
        {
            //win
            Debug.Log("Level Win");
            eventManager.CallLevelCompletedEvent();
            stackerAnim.SetTrigger("LevelCompleted");


        }
        else
        {
            //fail
            Debug.Log("Level Failed");
            eventManager.CallLevelFailedEvent();
        }
    }
    
}
