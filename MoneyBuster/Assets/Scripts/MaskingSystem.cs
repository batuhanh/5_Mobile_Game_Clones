using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MaskingSystem : MonoBehaviour
{
    public GameObject maskObj;

    private void Start()
    {
        MaskIt();
    }
    public void MaskIt()
    {
        maskObj.gameObject.GetComponent<MeshRenderer>().material.renderQueue = 3002;
    }

}
