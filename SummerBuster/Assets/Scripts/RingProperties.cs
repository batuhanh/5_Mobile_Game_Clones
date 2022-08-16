using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum RingColorType
{
    Green,
    Orange,
    Blue,
    Pink
}
public class RingProperties : MonoBehaviour
{
    [Header("Ring Materials")]
    public Material greenMat;
    public Material orangeMat;
    public Material blueMat;
    public Material pinkMat;

    [Header("Ring Ghost Materials")]
    public Material greenGhostMat;
    public Material orangeGhostMat;
    public Material blueGhostMat;
    public Material pinkGhostMat;

    public void SetRingColor(RingColorType currentColorState, ref MeshRenderer ringMesh, ref Material ringGhostMaterial)
    {
        if (currentColorState==RingColorType.Green)
        {
            ringMesh.material = greenMat;
            ringGhostMaterial = greenGhostMat;
        }
        else if (currentColorState == RingColorType.Orange)
        {
            ringMesh.material = orangeMat;
            ringGhostMaterial = orangeGhostMat;
        }
        else if (currentColorState == RingColorType.Blue)
        {
            ringMesh.material = blueMat;
            ringGhostMaterial = blueGhostMat;
        }
        else if (currentColorState == RingColorType.Pink)
        {
            ringMesh.material = pinkMat;
            ringGhostMaterial = pinkGhostMat;
        }
    }
    private void CheckAllRingHolders()
    {

    }
    void OnEnable()
    {
        EventManager.ringsOrderChanged += CheckAllRingHolders;
    }
    void OnDisable()
    {
        EventManager.ringsOrderChanged -= CheckAllRingHolders;
    }
}

