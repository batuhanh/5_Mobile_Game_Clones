using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;


public class Ring : Moveable
{
    [SerializeField] private RingColorType myColorType;
    [SerializeField] private RingProperties ringProperties;
    [SerializeField] private MeshRenderer myMR;
    [SerializeField] private Material myGhostMat;
    [SerializeField] private GameObject ghostRing;
    private Ringholder myRingHolder;
    private GameObject closestRingHolder;
    private Vector3 defaultPos;
    void Start()
    {
        defaultPos = transform.position;
        ringProperties.SetRingColor(myColorType, ref myMR, ref myGhostMat);
        UpdateGhostColor();

    }
    private void Update()
    {
        if (moveState == MoveState.Moving)
        {
            if (isClosedToTarget)
            {
                closestRingHolder = GetClosestTargetObj();
                if (closestRingHolder.GetComponent<Ringholder>().IsListAvailable())
                {
                    ghostRing.transform.position = closestRingHolder.transform.position + new Vector3(0, 1f + closestRingHolder.GetComponent<Ringholder>().GetListLength() * 2f, 0);
                }
            }
            else
            {
                closestRingHolder = null;
                ghostRing.transform.position = new Vector3(0, -50, 0);
            }

        }
    }
    public override void OnMouseDown()
    {
        base.OnMouseDown();
        if (canBeClicked)
        {
            if (moveState == MoveState.Moving)
            {
                myRingHolder.RemoveFromList(gameObject);
            }
        }
           
    }

    public override void OnMouseUp()
    {
        base.OnMouseUp();
        if (canBeClicked)
        {

            if (isClosedToTarget)
            {
                Vector3 targetPos = ghostRing.transform.position;
                transform.DOMove(closestRingHolder.transform.position + new Vector3(0, 10, 0), 0.1f).OnComplete(() =>
                {
                    transform.DOMove(targetPos, 0.3f).SetEase(Ease.OutBounce);
                });
                closestRingHolder.GetComponent<Ringholder>().AddRingToList(gameObject);
                myRingHolder = closestRingHolder.GetComponent<Ringholder>();
                defaultPos = ghostRing.transform.position;
                eventManager.CallRingsOrderChangedEvent();
            }
            else
            {
                transform.position = defaultPos;
                myRingHolder.AddRingToList(gameObject);

            }
            ghostRing.transform.position = new Vector3(0, -50, 0);

        }
    }
    private GameObject GetClosestTargetObj()
    {
        float minDistance = float.MaxValue;
        GameObject closestObj= targetObjs[0];
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
    public void SetRingHolder(Ringholder newringholder)
    {
        myRingHolder = newringholder;
    }
    void OnEnable()
    {
        EventManager.ringClicked += UpdateGhostColor;

    }
    void OnDisable()
    {
        EventManager.ringClicked -= UpdateGhostColor;

    }

}
