using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum MoveState
{
    Locked,
    Moving
}

/* 
 This class contain Movement mechanic for Rings
 */
public class Moveable : MonoBehaviour
{
    
    private Vector3 mOffset;
    private float mZCoord;
    protected Vector3 targetPos;
    protected bool canBeClicked = false;
    protected bool isClosedToTarget = false;
    [SerializeField] protected GameObject[] targetObjs;
    [SerializeField] protected EventManager eventManager;
    [SerializeField] protected MoveState moveState;
 
    void Start()
    {
      
    }

    void OnMouseDown()
    {
        if (canBeClicked)
        {
            moveState = MoveState.Moving;
            eventManager.CallRingClickedEvent();
            mZCoord = Camera.main.WorldToScreenPoint(
            gameObject.transform.position).z;
            mOffset = gameObject.transform.position - GetMouseAsWorldPoint(); //calcualting offset between mouseworld position to object position

        }
    }

    void OnMouseDrag()
    {
        if (canBeClicked)
        {
            transform.position = new Vector3((GetMouseAsWorldPoint() + mOffset).x, (GetMouseAsWorldPoint() + mOffset).y, transform.position.z);
            isClosedToTarget = IsCloseAnyTarget();
        }
    }
    public virtual void OnMouseUp()
    {
        if (canBeClicked)
        {
            moveState = MoveState.Locked;
        }
    }

    private bool IsCloseAnyTarget()
    {
        float enoughDistance = 5f;
        Vector3 offset = new Vector3(0,5,0);
        foreach (GameObject t in targetObjs)
        {
            if (Vector3.Distance(transform.position,t.transform.position+ offset) <enoughDistance)
            {
                return true;
            }
        }
        return false;
    }
    private Vector3 GetMouseAsWorldPoint() //This method is converting mouse position to World Space Coordinate Positon
    {
        Vector3 mousePoint = Input.mousePosition;
        mousePoint.z = mZCoord;
        return Camera.main.ScreenToWorldPoint(mousePoint);
    }



}
