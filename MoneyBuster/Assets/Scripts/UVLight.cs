using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UVLight : BustingTool
{
    [SerializeField] private Animator myAnim;
    [SerializeField] private EventManager eventManager;
    //this is an override for OnMouseDown method.As an extra this method is Calling event when player select Uv Light and calling anim of it
    public override void OnMouseDown()
    {
        base.OnMouseDown();
        eventManager.CallUVLightSelectedEvent();
        myAnim.SetBool("isActive", true);
    }
    //this is an override for OnMouseUp method. Setting aniamton bool to false to animaton stop
    public override void OnMouseUp()
    {
        base.OnMouseUp();
        myAnim.SetBool("isActive", false);
    }
}
