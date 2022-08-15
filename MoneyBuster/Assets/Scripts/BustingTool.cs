using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*
 * This class is inherit from Moveable. As an extra it has OnMouseDown method for UVligt and MagnifiyingGlass and the classes that are inherits from this
 */
public class BustingTool : Moveable
{
    //This is an virtual method to detect mouse click on a object attached or inherited from this class
    public virtual void OnMouseUp()
    {
        targetPos = startPos;
        canMoveBack = true;
    }
}
