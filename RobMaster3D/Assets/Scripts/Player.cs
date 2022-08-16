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
    private bool canTrigger = false;
    [SerializeField] private EventManager eventManager;
    [SerializeField] private GameObject[] visualSpheres;
    [SerializeField] private GameObject[] physicalSpheres;


    private void Start()
    {
        ChangeActivenessOfSpheres(false);
    }
    private void Update()
    {
        if (canMove)
        {
            transform.position += new Vector3(0, Time.deltaTime * (-speed), 0);
        }
    }
    private void OnTriggerEnter(Collider other)
    {
        if (canTrigger)
        {
            if (other.gameObject.CompareTag("PositionBox"))
            {
                Debug.Log("Player hitted PositionBox");
                Destroy(other.gameObject);
                ChangeActivenessOfSpheres(true);
                eventManager.CallHittedPositionBoxEvent();
                speed = 2.5f;

            }
            if (other.gameObject.CompareTag("FreeMoveBox"))
            {
                Debug.Log("Player hitted FreeMoveBox");
                Destroy(other.gameObject);
                eventManager.CallHittedFreeMoveBoxEvent();
                speed = 10f;
                ChangeActivenessOfSpheres(false);
            }
            if (other.gameObject.CompareTag("LevelWinCube"))
            {
                other.gameObject.tag = "Untagged";
                eventManager.CallLevelCompletedEvent();
                canMove = false;

            }
        }
        
    }
    private void ChangeActivenessOfSpheres(bool willBeActive)
    {
        foreach (GameObject s in physicalSpheres)
        {
            s.SetActive(willBeActive);
        }
        foreach (GameObject s in visualSpheres)
        {
            s.SetActive(willBeActive);
        }
    }
    private void StartPlayer()
    {
        canMove = true;
        canTrigger = true;
    }
    private void StopPlayer()
    {
        canMove = false;
        canTrigger = false;
        ChangeActivenessOfSpheres(false);
    }
    void OnEnable()
    {
        EventManager.myLevelStarted += StartPlayer;
        EventManager.myLevelFailed += StopPlayer;
    }
    void OnDisable()
    {
        EventManager.myLevelStarted -= StartPlayer;
        EventManager.myLevelFailed -= StopPlayer;
    }
}
