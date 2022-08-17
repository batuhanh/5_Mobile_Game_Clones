using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
public class Women : MonoBehaviour
{
    [SerializeField] private Animator myAnim;
    [SerializeField] private GameObject canvasObj;
    [SerializeField] private Animator mirrorFlashAnim;
    [SerializeField] GameObject[] photoPrefabs;
    [SerializeField] private RectTransform photosParent;
    private int currentPoseIndex;
    private List<GameObject> spawnedPhotos=new List<GameObject>();
    private void OpenNextPose()
    {
        myAnim.SetTrigger("Pose_" + currentPoseIndex.ToString());
        mirrorFlashAnim.SetTrigger("Flash");

        InstantiatePhoto();
        currentPoseIndex++;
        if (currentPoseIndex >= 4)
        {
            currentPoseIndex = 0;
        }
    }
    private void InstantiatePhoto()
    {
        Vector3 spawnPos = photosParent.transform.position + new Vector3(100, 0, 0);
        Quaternion randomRot = Quaternion.Euler(0,0,UnityEngine.Random.Range(-18f,18f));
        GameObject newPhoto = Instantiate(photoPrefabs[currentPoseIndex], spawnPos, randomRot, photosParent);

        Vector3 movePosition = photosParent.transform.position + new Vector3(UnityEngine.Random.Range(-30f,30f), UnityEngine.Random.Range(-30f, 30f), 0);
        newPhoto.transform.DOMove(movePosition, 0.5f).SetEase(Ease.OutCirc);
        spawnedPhotos.Add(newPhoto);
    }
    private void ShowAllPhotos()
    {
        Vector3 canvasPos = canvasObj.transform.position;
        foreach (GameObject photo in spawnedPhotos)
        {
            Vector3 targetPos = canvasPos+new Vector3(UnityEngine.Random.Range(-400,400), UnityEngine.Random.Range(-400, 400), 0);
            photo.gameObject.GetComponent<RectTransform>().DOMove(targetPos, 1f).SetEase(Ease.OutCirc);
        }
    }
    void OnEnable()
    {
        EventManager.trueDirectionSwiped += OpenNextPose;
        EventManager.myLevelCompleted += ShowAllPhotos;
    }
    void OnDisable()
    {
        EventManager.trueDirectionSwiped -= OpenNextPose;
        EventManager.myLevelCompleted -= ShowAllPhotos;
    }
}
