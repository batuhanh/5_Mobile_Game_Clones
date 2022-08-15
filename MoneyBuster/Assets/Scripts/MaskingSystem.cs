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
    private void MaskMoneys() //Changin renderQueue of moneys so they can be affected from my Mask shader
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

    private void MakeActiveStackMoneys() //Changing renderQueue of moneys on the stack to 3002 so they can affect from my Mask Shader
    {
        Debug.Log("MakeActive");
        foreach (GameObject m in moneyStackMoneys)
        {
            m.gameObject.GetComponent<MeshRenderer>().material.renderQueue = 3002;
        }
    }
    private void MakeDeactiveStackMoneys()//Changing renderQueue of moneys on the stack to 2000 so they can not affect from my Mask Shader
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
