using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum MoneyState
{
    GreenNormal,
    GreenFake,
    PurpleNormal,
    PurpleFake
}

public class Money : Moveable
{
    [SerializeField] private bool isHaveTargetPoint = false;
    [SerializeField] private GameObject[] targetPoints;
    [SerializeField] private GameObject[] moneyStateObjects;
    public MoneyState actualMoneyState;
    
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
    public void SetupMoneyObjects()
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
    private Vector3 DetectTargetPos()
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
    private bool IsCloseAnyTargetPoint()
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

    private Vector3 GetClosestTargetPoint()
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
    private void CheckPlacementType(GameObject targetObj)
    {
        if (targetObj.gameObject.GetComponent<MoneyPlacementObject>())
        {
            if (targetObj.gameObject.GetComponent<MoneyPlacementObject>().myMoneyPlacementType == MoneyPlacementType.Shredder)
            {
                gameObject.SetActive(false);
            }
            else if (targetObj.gameObject.GetComponent<MoneyPlacementObject>().myMoneyPlacementType == MoneyPlacementType.Stacker)
            {
                startRot = Quaternion.Euler(startRot.x, 90f, startRot.z);
                StartCoroutine(DeactiveWithDelay());
                
            }
        }
    }
    private IEnumerator DeactiveWithDelay()
    {
        yield return new WaitForSeconds(0.6f);
        gameObject.SetActive(false);
    }
    private void SetVisualForMagnificentGlass()
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
    private void SetVisualForUVLight()
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
