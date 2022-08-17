using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaterVacuum : CleaningTools
{
    private void Start()
    {
        canMove = false;
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
