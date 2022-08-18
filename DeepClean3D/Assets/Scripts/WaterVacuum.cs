using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class WaterVacuum : CleaningTools
{
    [SerializeField] private GameObject removerSphere;
    private void Start()
    {
        canMove = false;
        
    }
    public override void Update()
    {
        base.Update();
        removerSphere.gameObject.GetComponent<Rigidbody>().MovePosition(transform.position) ;
    }
    private void GoToGamePos()
    {
        moveSpeed = 125f;
        lerpSpeed = 8f;
        canMove = true;

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
