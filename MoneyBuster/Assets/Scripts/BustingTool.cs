using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BustingTool : Moveable
{
    public virtual void OnMouseUp()
    {
        targetPos = startPos;
        canMoveBack = true;
    }
}
