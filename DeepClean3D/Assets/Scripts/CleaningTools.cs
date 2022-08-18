using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CleaningTools : MonoBehaviour
{
    [SerializeField] protected GameObject moveObj;
    protected bool canMove = false;
    private Vector3 mousePosition;
    private float horizontal;
    private float vertical;
    protected float moveSpeed = 200f;
    protected float lerpSpeed = 4f;
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

            transform.position = Vector3.Lerp(transform.position,moveObj.transform.position, Time.deltaTime * lerpSpeed);

        }
    }
   
    private void MakeItActive()
    {
        Debug.Log("MakeItACtive");
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
