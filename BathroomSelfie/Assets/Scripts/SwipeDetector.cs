using UnityEngine;

public enum SwipeDirection
{
    None, Up, Down, Left, Right
}

public class SwipeDetector : MonoBehaviour
{
    [SerializeField] private EventManager eventManager;
    private const float minSwipeLength = 100f;
    private Vector2 currentSwipe;

    private Vector2 fingerStart;
    private Vector2 fingerEnd;
    private bool canSwipe = false;
    public static SwipeDirection lastSwipeDirection;

    void Update()
    {
        if (canSwipe)
        {
            SwipeDetection();
        }

    }

    public void SwipeDetection()
    {
        if (Input.GetMouseButtonDown(0))
        {
            fingerStart = Input.mousePosition;
            fingerEnd = Input.mousePosition;
        }
        if (Input.GetMouseButton(0))
        {
            fingerEnd = Input.mousePosition;

            currentSwipe = new Vector2(fingerEnd.x - fingerStart.x, fingerEnd.y - fingerStart.y);

        }

        if (Input.GetMouseButtonUp(0))
        {

            // Make sure it was a legit swipe, not a tap
            if (currentSwipe.magnitude < minSwipeLength)
            {
                lastSwipeDirection = SwipeDirection.None;
                return;
            }

            float angle = (Mathf.Atan2(currentSwipe.y, currentSwipe.x) / (Mathf.PI));
            //Debug.Log(angle);
            if (angle > 0.25f && angle < 0.75f)
            {
                Debug.Log("Up Swipe");
                lastSwipeDirection = SwipeDirection.Up;
            }
            else if ((angle > 0 && angle < 0.25f) || (angle > -0.25f && angle < 0))
            {
                Debug.Log("Right Swipe");
                lastSwipeDirection = SwipeDirection.Right;
            }
            else if ((angle < 1 && angle > 0.75) || (angle < -0.75f && angle > -1f))
            {
                Debug.Log("Left Swipe");
                lastSwipeDirection = SwipeDirection.Left;
            }
            else
            {
                Debug.Log("Down Swipe");
                lastSwipeDirection = SwipeDirection.Down;
            }
            eventManager.CallFingerSwipedEvent(lastSwipeDirection);
            //swipe yapýdlýðýnda evenmt ç.aðýr o event olduðunda da arrow lar kontrol etsin o anda true olan var mý varsa
            //doðru mu diye doðruysa onlar event çaðýrsýn. Arrowdaki boolu iptal et deðiþtir isimini tek sefer çalýþmasýn
            lastSwipeDirection = SwipeDirection.None;


        }
    }
    private void SetItActive()
    {
        canSwipe = true;
    }
    void OnEnable()
    {
        EventManager.myLevelStarted += SetItActive;

    }
    void OnDisable()
    {
        EventManager.myLevelStarted -= SetItActive;

    }
}