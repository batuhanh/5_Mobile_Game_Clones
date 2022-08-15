using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/* 
 This class is for object that can be move with mouse.
 */
public class Moveable : MonoBehaviour
{
    
    [SerializeField] private float moveHeight; 
    [SerializeField] protected float moveBackSpeed = 10f; 

    protected Vector3 startPos;
    protected Quaternion startRot; 

    private Vector3 mOffset; 
    private float mZCoord;
    protected bool canMoveBack = false;
    protected Vector3 targetPos;

    public virtual void Start()
    {
        startPos = transform.position;
    }
    public virtual void Update() //just lerping position and rotation vectors inside Update method. 
    {
        if (canMoveBack) 
        {
            transform.position = Vector3.Lerp(transform.position, targetPos, Time.deltaTime * moveBackSpeed);
            transform.rotation = Quaternion.Lerp(transform.rotation, startRot, Time.deltaTime * moveBackSpeed);
            if (transform.position==startPos)
            {
                canMoveBack = false;
            }
        }
    }

    public virtual void OnMouseDown() 
    {
        Debug.Log("OnMouseDown");
        canMoveBack = false;
        mZCoord = Camera.main.WorldToScreenPoint(
            gameObject.transform.position).z;
        mOffset = gameObject.transform.position - GetMouseAsWorldPoint(); //calcualting offset between mouseworld position to object position
    }

    void OnMouseDrag()
    {
        transform.position = new Vector3((GetMouseAsWorldPoint() + mOffset).x, moveHeight, (GetMouseAsWorldPoint() + mOffset).z) ;
    }
  

    private Vector3 GetMouseAsWorldPoint() //This method is converting mouse position to World Space Coordinate Positon
    {
        Vector3 mousePoint = Input.mousePosition;
        mousePoint.z = mZCoord;
        return Camera.main.ScreenToWorldPoint(mousePoint);
    }
    


}
