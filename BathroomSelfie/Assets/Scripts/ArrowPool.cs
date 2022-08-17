using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArrowPool : MonoBehaviour
{
    [SerializeField] private GameObject arrowPrefab;
    [SerializeField] private GameObject arrowParent;
    [SerializeField] private List<GameObject> arrowPoolList;
    [SerializeField] private int arrowCount;

    //Creating pool system to avoid garbage collection
    //Making pool script singleton pattern and static to easy acces
    private static ArrowPool _instance; 
    public static ArrowPool Instance
    {
        get
        {
            if (_instance==null)
            {
                Debug.Log("no instance");
            }
            return _instance;
        }
    }
    private void Awake()
    {
        _instance = this;
    }
    private void Start()
    {
        arrowPoolList = GenerateArrows(arrowCount);
    }
    private List<GameObject> GenerateArrows(int count)
    {
        for (int i = 0; i < count; i++)
        {
            GameObject arrow = Instantiate(arrowPrefab);
            arrow.transform.SetParent(arrowParent.transform);
            arrow.SetActive(false);
            arrowPoolList.Add(arrow);
        }
        return arrowPoolList;
    }
    public GameObject RequestArrow()
    {
        foreach (var arrow in arrowPoolList)
        {
            if (!arrow.activeInHierarchy)
            {
                arrow.SetActive(true);
                return arrow;
            }
        }
        GameObject newArrow = Instantiate(arrowPrefab);
        newArrow.transform.SetParent(arrowParent.transform);
        arrowPoolList.Add(newArrow);
        return newArrow;
    }
}
