using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Money : Moveable
{
    [SerializeField] private bool isHaveTargetPoint = false;
    [SerializeField] private GameObject[] targetPoints;
    public override void Start()
    {
        base.Start();
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
                //disable money
                gameObject.SetActive(false);
            }
            else if (targetObj.gameObject.GetComponent<MoneyPlacementObject>().myMoneyPlacementType == MoneyPlacementType.Stacker)
            {
                //rotate money
                startRot = Quaternion.Euler(startRot.x, 90f, startRot.z);
               
            }
        }
    }
}
