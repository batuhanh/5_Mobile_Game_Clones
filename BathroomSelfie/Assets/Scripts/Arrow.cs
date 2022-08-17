using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Arrow : MonoBehaviour
{
    public EventManager eventManager;
    public RectTransform targetBox;
    public ArrowDirection myArrowDirection;
    private float speed = 200f;
    [SerializeField] private ArrowState arrowState;

    private void Start()
    {
        SetupArrow();
    }
    private void Update()
    {
        if (gameObject.activeSelf)
        {
            transform.position += new Vector3(Time.deltaTime * (-speed), 0, 0);
            if (transform.position.x < -800)
            {
                gameObject.SetActive(false);
            }
            if (arrowState != ArrowState.Used)
            {
                if (Mathf.Abs(transform.position.x - targetBox.transform.position.x) < 150f)
                {
                    arrowState = ArrowState.Near;
                }
                else
                {
                    arrowState = ArrowState.Spawned;
                }
            }
           
        }


    }
    public void SetupArrow()
    {
        SetRandomDirection();
        arrowState = ArrowState.Spawned;
    }
    public void SetRandomDirection()
    {
        int myRandomVal = UnityEngine.Random.Range(0, 4);
        if (myRandomVal == 0) //Up
        {
            myArrowDirection = ArrowDirection.Up;
            transform.rotation = Quaternion.Euler(0, 0, 0);
        }
        else if (myRandomVal == 1)//Down
        {
            myArrowDirection = ArrowDirection.Down;
            transform.rotation = Quaternion.Euler(0, 0, 180);
        }
        else if (myRandomVal == 2)//Right
        {
            myArrowDirection = ArrowDirection.Right;
            transform.rotation = Quaternion.Euler(0, 0, 270);
        }
        else//Left
        {
            myArrowDirection = ArrowDirection.Left;
            transform.rotation = Quaternion.Euler(0, 0, 90);
        }
    }
    private void CheckArrowState(SwipeDirection swipeDirection)
    {
      
        if (arrowState == ArrowState.Near)
        {
            arrowState = ArrowState.Used;
            if (CheckDirectionMatch(swipeDirection))
            {
                eventManager.CallTrueDirectionSwipedEvent();
                Debug.Log("True Swiped");
            }
            else
            {
                eventManager.CallWrongDriectionSwipedEvent();
                Debug.Log("True Swiped");
            }
        }
    }
    private bool CheckDirectionMatch(SwipeDirection swipeDirection)
    {
        if (swipeDirection==SwipeDirection.Up && myArrowDirection==ArrowDirection.Up)
        {
            return true;
        }
        else if (swipeDirection == SwipeDirection.Down && myArrowDirection == ArrowDirection.Down)
        {
            return true;
        }
        else if (swipeDirection == SwipeDirection.Right && myArrowDirection == ArrowDirection.Right)
        {
            return true;
        }
        else if (swipeDirection == SwipeDirection.Left && myArrowDirection == ArrowDirection.Left)
        {
            return true;
        }
        else
        {
            return false;
        }
        
        
    }
    void OnEnable()
    {
        EventManager.fingerSwiped += CheckArrowState;
    }
    void OnDisable()
    {
        EventManager.fingerSwiped -= CheckArrowState;
    }

}
