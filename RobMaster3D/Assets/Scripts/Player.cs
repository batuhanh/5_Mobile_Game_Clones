using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public enum PlayerState
{
    FreeMoving,
    Positioning
}
public class Player : MonoBehaviour
{
    private float speed = 10f;
    private PlayerState playerState;
    private bool canMove = false;

    private void Update()
    {
        if (canMove)
        {
            transform.position += new Vector3(0, Time.deltaTime * (-speed), 0);
        }
    }
    private void StartPlayer()
    {
        canMove = true;
    }
    void OnEnable()
    {
        EventManager.myLevelStarted += StartPlayer;
    }
    void OnDisable()
    {
        EventManager.myLevelStarted -= StartPlayer;
    }
}
