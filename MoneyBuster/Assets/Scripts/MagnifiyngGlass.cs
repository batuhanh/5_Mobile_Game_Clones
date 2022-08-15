using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MagnifiyngGlass : BustingTool
{
    [SerializeField] private EventManager eventManager;
    public override void OnMouseDown()
    {
        base.OnMouseDown();
        eventManager.CallMagnifiyingGlassSelectedEvent();
    }
    public override void OnMouseUp()
    {
        base.OnMouseUp();
       
    }
}
