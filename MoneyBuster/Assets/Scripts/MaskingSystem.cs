using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MaskingSystem : MonoBehaviour
{
 
    private GameObject[] moneys;
    public GameObject[] moneyStackMoneys;
    private void DetectMoneys()
    {
        moneys = GameObject.FindGameObjectsWithTag("MoneyWillMask");
        moneyStackMoneys = GameObject.FindGameObjectsWithTag("MoneyStackWillMask");
    }
    private void MaskMoneys()
    {
        foreach (GameObject m in moneys)
        {
            m.gameObject.GetComponent<MeshRenderer>().material.renderQueue = 3002;
        }
        foreach (GameObject m in moneyStackMoneys)
        {
            m.gameObject.GetComponent<MeshRenderer>().material.renderQueue = 3002;
        }
    }

    private void MakeActiveStackMoneys()
    {
        Debug.Log("MakeActive");
        foreach (GameObject m in moneyStackMoneys)
        {
            m.gameObject.GetComponent<MeshRenderer>().material.renderQueue = 3002;
        }
    }
    private void MakeDeactiveStackMoneys()
    {
        Debug.Log("MakeDeactive");
        foreach (GameObject m in moneyStackMoneys)
        {
            m.gameObject.GetComponent<MeshRenderer>().material.renderQueue = 2000;
        }
    }

    void OnEnable()
    {
        EventManager.myLevelStarted += DetectMoneys;
        EventManager.myLevelStarted += MaskMoneys;
        EventManager.magnifiyingGlassSelected += MakeDeactiveStackMoneys;
        EventManager.uvLightSelected += MakeActiveStackMoneys;
    }
    void OnDisable()
    {
        EventManager.myLevelStarted -= DetectMoneys;
        EventManager.myLevelStarted -= MaskMoneys;
        EventManager.magnifiyingGlassSelected -= MakeDeactiveStackMoneys;
        EventManager.uvLightSelected -= MakeActiveStackMoneys;
    }

}
