using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class HandVacuum : CleaningTools
{
    [SerializeField] private GameObject vacuumModel;
    [SerializeField] private EventManager eventManager;
    private Vector3 startPos;
    private void Start()
    {
        startPos = transform.position;
        moveObjStartPos = moveObj.transform.position;

    }
    public override void Update()
    {
        base.Update();
        vacuumModel.transform.LookAt(moveObj.transform);
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("CheesePuff"))
        {
            other.gameObject.tag = "Untagged";
            GameObject oldParent = other.transform.parent.gameObject;
            other.transform.SetParent(vacuumModel.transform);
            other.transform.DOLocalJump(new Vector3(0, -0.25f, 1.82f), 0.4f, 1, 0.2f).OnComplete(() =>
            {
                Destroy(other.gameObject);
                
            });
            Debug.Log(oldParent.name);
            if (oldParent.transform.childCount == 0)
            {
                
                transform.DOMove(startPos, 1f).OnComplete(() =>
                {
                    Destroy(gameObject);
                    eventManager.CallHandVacuumStageDoneEvent();
                });
                Debug.Log("HandVacuumStageDone");
            }
        }
    }

}
