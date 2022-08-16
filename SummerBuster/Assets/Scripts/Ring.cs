using System.Collections;
using System.Collections.Generic;
using UnityEngine;



public class Ring : Moveable
{
    [SerializeField] private RingColorType myColorType;
    [SerializeField] private RingProperties ringProperties;
    [SerializeField] private MeshRenderer myMR;
    [SerializeField] private Material myGhostMat;
    [SerializeField] private GameObject ghostRing;
    private Vector3 startPos;
    void Start()
    {
        startPos = transform.position;
        ringProperties.SetRingColor(myColorType, ref myMR, ref myGhostMat);
        CheckForClickedStatus();

    }
    private void Update()
    {
        if (moveState==MoveState.Moving)
        {
            if (isClosedToTarget)
            {
                GameObject closestTarget = GetClosestTargetObj();
                if (IsTargetAvailable(closestTarget))
                {
                    ghostRing.transform.position = closestTarget.transform.position + new Vector3(0, 1f + closestTarget.GetComponent<Ringholder>().GetListLength() * 2f, 0);
                }
            }
            else
            {
                ghostRing.transform.position = new Vector3(0, -50, 0);
            }
            
        }
    }
    public override void OnMouseUp()
    {
        base.OnMouseUp();
        if (isClosedToTarget)
        {
            transform.position = ghostRing.transform.position;
        }
        else
        {
            Debug.Log(startPos);
            transform.position = startPos;
        }
    }
    private bool IsTargetAvailable(GameObject target)
    {
        return true;
    }
    private GameObject GetClosestTargetObj()
    {
        float minDistance = float.MaxValue;
        GameObject closestObj = new GameObject();
        Vector3 offset = new Vector3(0, 5, 0);
        foreach (GameObject t in targetObjs)
        {
            float distance = Vector3.Distance(transform.position, t.transform.position + offset);
            if (distance < minDistance)
            {
                minDistance = distance;
                closestObj = t;
            }
        }
        return closestObj;
    }
    private void CheckForClickedStatus()
    {
        canBeClicked = true;
        UpdateGhostColor();

    }
    private void UpdateGhostColor()
    {
        if (moveState == MoveState.Moving)
        {
            ghostRing.gameObject.GetComponent<MeshRenderer>().material = myGhostMat;
        }
    }
    public void UpdateRingPos(GameObject ringHolderObject, int index)
    {
        transform.position = ringHolderObject.transform.position + new Vector3(0, 1f + index * 2f, 0);
    }
    void OnEnable()
    {
        EventManager.ringsOrderChanged += CheckForClickedStatus;
        EventManager.ringClicked += UpdateGhostColor;

    }
    void OnDisable()
    {
        EventManager.ringsOrderChanged -= CheckForClickedStatus;
        EventManager.ringClicked -= UpdateGhostColor;

    }

}
