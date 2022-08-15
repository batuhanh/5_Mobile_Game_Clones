using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//This enum is holding the names of MoneyState
public enum MoneyState
{
    GreenNormal,
    GreenFake,
    PurpleNormal,
    PurpleFake
}
/*
 * This class is inherit from Moveable
 */
public class Money : Moveable
{
    [SerializeField] private bool isHaveTargetPoint = false;
    [SerializeField] private GameObject[] targetPoints;//MoneyStacker and MoneyShredder Objects
    [SerializeField] private GameObject[] moneyStateObjects;// the child objects for states
    public MoneyState actualMoneyState; // our current state of money
    
    public override void Start()
    {
        base.Start();
        SetupMoneyObjects();
    }
    public override void Update()
    {
        base.Update();
    }
    private void OnMouseUp()
    {
        targetPos = DetectTargetPos();
        canMoveBack = true;

    }
    public void SetupMoneyObjects() //setting active the right object for our current state and deactive others.
    {
        foreach (GameObject M in moneyStateObjects)
        {
            M.SetActive(false);
        }
        if (actualMoneyState==MoneyState.GreenNormal)
        {
            moneyStateObjects[0].SetActive(true);
        }
        else if(actualMoneyState == MoneyState.GreenFake)
        {
            moneyStateObjects[1].SetActive(true);
        }
        else if (actualMoneyState == MoneyState.PurpleNormal)
        {
            moneyStateObjects[2].SetActive(true);
        }
        else if (actualMoneyState == MoneyState.PurpleFake)
        {
            moneyStateObjects[3].SetActive(true);
        }
    }
    private Vector3 DetectTargetPos() //If is there any closest target this method is returns target position value as a Vector3
    {
        Vector3 targetPos = new Vector3();
        if (isHaveTargetPoint && IsCloseAnyTargetPoint())
        {
            targetPos = GetClosestTargetPoint();

        }
        else
        {
            targetPos = startPos;
        }
        return targetPos;

    }
    private bool IsCloseAnyTargetPoint()// Checking for are there any close target object if is exist returns true
    {
        foreach (GameObject tp in targetPoints)
        {
            if (Vector3.Distance(transform.position, tp.transform.position) < 3f)
            {
                return true;
            }
        }
        return false;
    }

    private Vector3 GetClosestTargetPoint() //This method is detecting closest target object and returns its position
    {
        float closestDistance = float.MaxValue;
        GameObject closestTarget = new GameObject();
        Vector3 closestTargetPoint = targetPoints[0].gameObject.transform.position;
        foreach (GameObject tp in targetPoints)
        {
            if (Vector3.Distance(transform.position, tp.transform.position) < closestDistance)
            {
                closestTargetPoint = tp.transform.position;
                closestDistance = Vector3.Distance(transform.position, tp.transform.position);
                closestTarget = tp;
            }
        }
        CheckPlacementType(closestTarget); 
        return closestTargetPoint;
    }
    private void CheckPlacementType(GameObject targetObj)//this method is checking which type of target is money landed
    {
        if (targetObj.gameObject.GetComponent<MoneyPlacementObject>())
        {
            if (targetObj.gameObject.GetComponent<MoneyPlacementObject>().myMoneyPlacementType == MoneyPlacementType.Shredder)
            {
                gameObject.SetActive(false);
                targetObj.gameObject.GetComponent<MoneyShredder>().CheckMoney(actualMoneyState);
            }
            else if (targetObj.gameObject.GetComponent<MoneyPlacementObject>().myMoneyPlacementType == MoneyPlacementType.Stacker)
            {
                startRot = Quaternion.Euler(startRot.x, 90f, startRot.z);
                StartCoroutine(DeactiveWithDelay());
                targetObj.gameObject.GetComponent<MoneyStacker>().CheckMoney(actualMoneyState);
            }
        }
    }
    private IEnumerator DeactiveWithDelay()
    {
        yield return new WaitForSeconds(0.6f);
        gameObject.SetActive(false);
    }
    private void SetVisualForMagnificentGlass()//This method is opening right child object for Magnificent Glass
    {
        foreach (GameObject M in moneyStateObjects)
        {
            M.SetActive(false);
        }
        if (actualMoneyState==MoneyState.GreenFake)
        {
            moneyStateObjects[1].SetActive(true);
        }
        else
        {
            moneyStateObjects[0].SetActive(true);
        }
    }
    private void SetVisualForUVLight()//This method is opening right child object for UVlight
    {
        foreach (GameObject M in moneyStateObjects)
        {
            M.SetActive(false);
        }
        if (actualMoneyState == MoneyState.PurpleFake)
        {
            moneyStateObjects[3].SetActive(true);
        }
        else
        {
            moneyStateObjects[2].SetActive(true);
        }
    }
    void OnEnable()
    {
        EventManager.magnifiyingGlassSelected += SetVisualForMagnificentGlass;
        EventManager.uvLightSelected += SetVisualForUVLight;

    }
    void OnDisable()
    {
        EventManager.magnifiyingGlassSelected -= SetVisualForMagnificentGlass;
        EventManager.uvLightSelected -= SetVisualForUVLight;

    }
}
