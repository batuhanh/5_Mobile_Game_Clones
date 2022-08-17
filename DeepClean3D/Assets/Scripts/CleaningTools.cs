using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CleaningTools : MonoBehaviour
{
    [SerializeField] protected GameObject moveObj;
    private bool canMove = false;
    private Vector3 mousePosition;
    private float horizontal;
    private float vertical;
    private float moveSpeed = 400f;
    public virtual void Update()
    {
        if (canMove)
        {
            if (Input.GetMouseButtonDown(0))
            {
                mousePosition = Input.mousePosition;
            }
            else if (Input.GetMouseButton(0))
            {

                horizontal = (Input.mousePosition.x - mousePosition.x) / Screen.width * 2.5f;
                vertical = (Input.mousePosition.y - mousePosition.y) / Screen.width * 2.5f;
                mousePosition = Input.mousePosition;
            }
            else
            {
                horizontal = 0;
                vertical = 0;
            }

            moveObj.transform.position = new Vector3(Mathf.Clamp(moveObj.transform.position.x + (moveSpeed * horizontal * Time.deltaTime), -2.5f, 2.5f), moveObj.transform.position.y,
                Mathf.Clamp(moveObj.transform.position.z + (moveSpeed * vertical * Time.deltaTime), -5.8f, 3f));

            transform.position = Vector3.Lerp(transform.position,moveObj.transform.position, Time.deltaTime * 4f);

        }
    }
   
    private void MakeItActive()
    {
        canMove = true;
    }
    void OnEnable()
    {
        EventManager.myLevelStarted += MakeItActive;

    }
    void OnDisable()
    {
        EventManager.myLevelStarted -= MakeItActive;
    }
}
