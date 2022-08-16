using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ringholder : MonoBehaviour
{
    [SerializeField] private List<GameObject> ringList;
    [SerializeField] int maxLength;
    public void AddRingToList(GameObject newRing)
    {
        if (GetListLength()<maxLength)
        {
            ringList.Add(newRing);
        }
     
    }
    public int GetListLength()
    {
        return ringList.Count;
    }
}
