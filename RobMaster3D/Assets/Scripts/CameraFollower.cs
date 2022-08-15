using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollower : MonoBehaviour
{
    private Vector3 offset;
    [SerializeField] private GameObject targetObj;
    [SerializeField] private float followSpeed=5f;
    private bool canFollow = false;
    private Vector3 lerpedCameraPos;
    private void Start()
    {
        offset = transform.position - targetObj.transform.position;
        lerpedCameraPos = targetObj.transform.position + offset;
    }
    private void LateUpdate()
    {
        if (canFollow)
        {
            lerpedCameraPos = Vector3.Lerp(lerpedCameraPos, targetObj.transform.position + offset,Time.deltaTime* followSpeed);
            transform.position = lerpedCameraPos;
        }
    }
    private void StartCamera()
    {
        canFollow = true;
    }
    void OnEnable()
    {
        EventManager.myLevelStarted += StartCamera;
    }
    void OnDisable()
    {
        EventManager.myLevelStarted -= StartCamera;
    }
}
