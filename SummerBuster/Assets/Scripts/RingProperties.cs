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
    [SerializeField] LevelManager levelManager;
    [SerializeField] EventManager eventManager;
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

    private Ringholder[] ringHolders;
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
    private void LoadAllRingHolders()
    {
        ringHolders = levelManager.GetCurrentLevelObject().gameObject.GetComponentsInChildren<Ringholder>();
    }
    private void CheckAllRingHolders()
    {
        bool isAllRingOkey = true;
        if (ringHolders==null)
        {
            LoadAllRingHolders();
            Debug.Log("LoadAllRingHolders");
        }
        foreach (Ringholder rh in ringHolders)
        {
            if (!rh.IsThisRingHolderOkey())
            {
                isAllRingOkey = false;
            }
        }
        if (isAllRingOkey)
        {
            //Win
            Debug.Log("Win");
            eventManager.CallLevelCompletedEvent();
        }
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

