using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveablePoint : MonoBehaviour
{
    private Vector3 mOffset;
    private float mZCoord;
    [SerializeField] private GameObject positionSphere;
    private Vector3 startLocalPos;
    private bool canRaycast = false;
    [SerializeField] private LayerMask layerMask;
    [SerializeField] private Color greenColor;
    [SerializeField] private Color redColor;
    [SerializeField] private EventManager eventManager;
    private MeshRenderer positionSphereMR;
    public static bool isFailed = false; // this bool is static to call failed event just once
    private void Awake()
    {
        transform.position = positionSphere.transform.position;
        startLocalPos = transform.localPosition;
        positionSphereMR = positionSphere.gameObject.GetComponent<MeshRenderer>();

    }
    private void FixedUpdate()
    {
        if (canRaycast)
        {
            RaycastHit hit;
            if (Physics.Raycast(transform.position, transform.forward, out hit, Mathf.Infinity, layerMask))
            {
                // Debug.DrawRay(transform.position, transform.forward, Color.yellow);
                //Hitted
                if (positionSphereMR.material.GetColor("_Color") != redColor)
                {
                    positionSphereMR.material.SetColor("_Color", redColor);
                }
              
                if (hit.distance<1.1f && !isFailed)
                {
                    //failed
                    Debug.Log("Failed");
                    isFailed = true;
                    eventManager.CallLevelFailedEvent();
                }
            }
            else
            {
                //did not hitted
                if (positionSphereMR.material.GetColor("_Color") != greenColor)
                {
                    positionSphereMR.material.SetColor("_Color", greenColor);
                }
            }
        }
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
        transform.position = positionSphere.transform.position;
    }
    private Vector3 GetMouseAsWorldPoint()
    {
        Vector3 mousePoint = Input.mousePosition;
        mousePoint.z = mZCoord;
        return Camera.main.ScreenToWorldPoint(mousePoint);
    }
    private void SetStartPosition()
    {
        transform.localPosition = startLocalPos;
    }
    private void MakeItRaycastable()
    {
        canRaycast = true;
    }
    void OnEnable()
    {
        EventManager.hittedFreeMoveBox += SetStartPosition;
        EventManager.hittedPositionBox += MakeItRaycastable;
    }
    void OnDisable()
    {
        EventManager.hittedFreeMoveBox -= SetStartPosition;
        EventManager.hittedPositionBox -= MakeItRaycastable;
    }
}
