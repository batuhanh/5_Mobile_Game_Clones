using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class HandVacuum : CleaningTools
{
    [SerializeField] private GameObject vacuumModel;
    [SerializeField] private EventManager eventManager;
    public override void Update()
    {
        base.Update();
        vacuumModel.transform.LookAt(moveObj.transform);
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("CheesePuff"))
        {
            Transform oldParent = other.transform.parent;
            other.transform.SetParent(vacuumModel.transform);
            other.transform.DOLocalJump(new Vector3(0, -0.25f, 1.82f), 0.4f, 1, 0.2f).OnComplete(() =>
            {
                Destroy(other.gameObject);
                if (oldParent.childCount==0)
                {
                    eventManager.CallHandVacuumStageDoneEvent();
                }
            });
        }
    }
}
