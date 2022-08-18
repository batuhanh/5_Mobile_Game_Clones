using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class WaterVacuum : CleaningTools
{
    [SerializeField] private GameObject removerSphere;
    [SerializeField] private EventManager eventManager;
    [SerializeField] private GameObject collisionBallsparent;
    private int totalBallCount = 0;
    private int hittedBallCount = 0;
    private bool canCollide = false;

    private void Start()
    {
        canMove = false;

    }
    public override void Update()
    {
        base.Update();
        //removerSphere.gameObject.GetComponent<Rigidbody>().MovePosition(transform.position) ;

            removerSphere.transform.position = transform.position + new Vector3(-0.4f, -0.3f, -0.4f);
    }
    private void OnTriggerEnter(Collider other)
    {
        if (canCollide && other.gameObject.CompareTag("CollisionBallsForWater"))
        {
            Destroy(other.gameObject);
            hittedBallCount++;
            if (hittedBallCount>=totalBallCount)
            {
                eventManager.CallLevelCompletedEvent();
            }
        }
    }
    private void GoToGamePos()
    {
        //moveObj.transform.position = moveObjStartPos;

        moveSpeed = 75f;
        lerpSpeed = 8f;
        canMove = true;
        collisionBallsparent.SetActive(true);
        totalBallCount = collisionBallsparent.transform.childCount;
        StartCoroutine(MakeItCollisable());

    }
    IEnumerator MakeItCollisable()
    {
        yield return new WaitForSeconds(1f);
        canCollide = true;
        removerSphere.SetActive(true);
    }
    void OnEnable()
    {
        EventManager.handVacuumStageDone += GoToGamePos;

    }
    void OnDisable()
    {
        EventManager.handVacuumStageDone -= GoToGamePos;

    }
}
