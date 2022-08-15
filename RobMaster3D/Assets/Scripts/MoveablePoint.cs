using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveablePoint : MonoBehaviour
{
    private Vector3 mOffset;
    private float mZCoord;
    [SerializeField] private GameObject positionSphere;
    private void Start()
    {
        transform.position = positionSphere.transform.position;
    }
    private void OnMouseDown()
    {
        mZCoord = Camera.main.WorldToScreenPoint(
            gameObject.transform.position).z;
        mOffset = gameObject.transform.position - GetMouseAsWorldPoint(); //calcualting offset between mouseworld position to object position
    }

    private void OnMouseDrag()
    {
        transform.position = new Vector3((GetMouseAsWorldPoint() + mOffset).x, transform.position.y, (GetMouseAsWorldPoint() + mOffset).z);
    }
    private void OnMouseUp()
    {
       // transform.position = new Vector3(positionSphere.transform.position.x,transform.position.y, positionSphere.transform.position.z);
        transform.position = positionSphere.transform.position;
    }
    private Vector3 GetMouseAsWorldPoint() 
    {
        Vector3 mousePoint = Input.mousePosition;
        mousePoint.z = mZCoord;
        return Camera.main.ScreenToWorldPoint(mousePoint);
    }
}
