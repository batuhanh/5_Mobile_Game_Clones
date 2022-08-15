using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*
 * This class is inherit from BustingTool. 
 */
public class MagnifiyngGlass : BustingTool
{
    [SerializeField] private EventManager eventManager;
    //this is an override for OnMouseDown method.As an extra this method is Calling event when player select Magnifiyn glass
    public override void OnMouseDown() 
    {
        base.OnMouseDown();
        eventManager.CallMagnifiyingGlassSelectedEvent();
    }
    //this is an override method for OnMosuyseUp
    public override void OnMouseUp()
    {
        base.OnMouseUp();
       
    }
}
