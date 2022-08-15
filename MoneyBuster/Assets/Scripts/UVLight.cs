using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UVLight : BustingTool
{
    [SerializeField] private Animator myAnim;
    [SerializeField] private EventManager eventManager;
    public override void OnMouseDown()
    {
        base.OnMouseDown();
        eventManager.CallUVLightSelectedEvent();
        myAnim.SetBool("isActive", true);
    }
    public override void OnMouseUp()
    {
        base.OnMouseUp();
        myAnim.SetBool("isActive", false);
    }
}
