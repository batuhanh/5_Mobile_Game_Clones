using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public enum ArrowDirection
{
    Up,
    Down,
    Right,
    Left
}
public enum ArrowState
{
    Spawned,
    Near,
    Used
}
public class ArrowManager : MonoBehaviour
{
    [SerializeField] private GameObject arrowPanel;
    [SerializeField] private EventManager eventManager;
    [SerializeField] private RectTransform targetBox;
    [SerializeField] private RectTransform spawnPointTransform;
    [SerializeField] private Animator boxAnim;
    private float timer = 2f;
    private bool canSpawn = false;
    private void Update()
    {
        if (canSpawn)
        {
            timer += Time.deltaTime;
            if (timer>2f)
            {
                timer = 0f;
                //Spawn a arrow
                GameObject newArrow = ArrowPool.Instance.RequestArrow();
                newArrow.transform.position = spawnPointTransform.position;
                Arrow arrowScript = newArrow.gameObject.GetComponent<Arrow>();
                arrowScript.SetupArrow();
                arrowScript.targetBox=targetBox;
                arrowScript.eventManager=eventManager;
            }
        }
    }
    private void StartArowSpawning()
    {
        arrowPanel.SetActive(true);
        canSpawn = true;
    }
    private void OpenTrueAnim()
    {
        boxAnim.SetTrigger("True");
    }
    private void OpenWrongAnim()
    {
        boxAnim.SetTrigger("False");
    }
    void OnEnable()
    {
        EventManager.myLevelStarted += StartArowSpawning;
        EventManager.trueDirectionSwiped += OpenTrueAnim;
        EventManager.wrongDirectionSwiped += OpenWrongAnim;

    }
    void OnDisable()
    {
        EventManager.myLevelStarted -= StartArowSpawning;
        EventManager.trueDirectionSwiped -= OpenTrueAnim;
        EventManager.wrongDirectionSwiped -= OpenWrongAnim;

    }
}
