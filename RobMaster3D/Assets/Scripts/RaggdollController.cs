using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DitzelGames.FastIK;

public class RaggdollController : MonoBehaviour
{
    [SerializeField] private GameObject[] bones;
    [SerializeField] private FastIKFabric[] ikFabrics;

    private void OpenRagdoll()
    {
        foreach (GameObject bone in bones)
        {
            bone.gameObject.GetComponent<Collider>().enabled = true;
            bone.gameObject.GetComponent<Rigidbody>().isKinematic = false;
            bone.gameObject.GetComponent<Rigidbody>().useGravity = true;
        }
    }
    private void OpenIKFabrics()
    {
        foreach (FastIKFabric ik in ikFabrics)
        {
            ik.enabled = true;
        }
    }
    private void CloseRagdoll()
    {
        foreach (GameObject bone in bones)
        {
            bone.gameObject.GetComponent<Collider>().enabled = false;
            bone.gameObject.GetComponent<Rigidbody>().isKinematic = true;
            bone.gameObject.GetComponent<Rigidbody>().useGravity = false;
        }
    }
    private void CloseIKFabrics()
    {
        foreach (FastIKFabric ik in ikFabrics)
        {
            ik.enabled = false;
        }
    }
    private void AddForceToHips()
    {
        bones[0].gameObject.GetComponent<Rigidbody>().AddForce(Vector3.Normalize(bones[0].transform.position + new Vector3(0, 5, 0)) * 8000);
    }
    void OnEnable()
    {
        EventManager.myLevelStarted += CloseRagdoll;
        EventManager.myLevelStarted += OpenIKFabrics;
        EventManager.myLevelFailed += CloseIKFabrics;
        EventManager.myLevelFailed += OpenRagdoll;
        EventManager.myLevelFailed += AddForceToHips;
    }
    void OnDisable()
    {
        EventManager.myLevelStarted -= CloseRagdoll;
        EventManager.myLevelStarted -= OpenIKFabrics;
        EventManager.myLevelFailed -= CloseIKFabrics;
        EventManager.myLevelFailed -= OpenRagdoll;
        EventManager.myLevelFailed -= AddForceToHips;
    }
}
