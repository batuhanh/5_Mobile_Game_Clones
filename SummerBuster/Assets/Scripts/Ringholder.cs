using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ringholder : MonoBehaviour
{
    [SerializeField] private List<GameObject> ringList;
    private int maxLength = 4;
    [SerializeField] private Transform myRingsParent;
    public void AddRingToList(GameObject newRing)
    {
        if (GetListLength() < maxLength)
        {
            ringList.Add(newRing);
            newRing.transform.SetParent(myRingsParent);
        }

    }
    public bool IsListAvailable()
    {
        return (GetListLength() < maxLength);
    }
    public void RemoveFromList(GameObject ring)
    {
        ringList.Remove(ring);
        ring.transform.SetParent(null);
    }
    public int GetListLength()
    {
        return ringList.Count;
    }
    private void UpdateRingsPoses()
    {
        for (int i = 0; i < ringList.Count; i++)
        {
            ringList[i].gameObject.GetComponent<Ring>().UpdateRingPos(gameObject,i);
        }
    }
    private void SetRingHolderOfRings()
    {
        foreach (GameObject ring in ringList)
        {
            ring.GetComponent<Ring>().SetRingHolder(this);
        }
    }
    private void CheckRingsForClickable()
    {
        foreach (GameObject ring in ringList)
        {
            ring.GetComponent<Ring>().canBeClicked = false;
        }
        if (ringList.Count>0)
        {
            ringList[ringList.Count - 1].GetComponent<Ring>().canBeClicked = true;
        }
        
    }
    public bool IsThisRingHolderOkey()
    {
        //Check if it is empty ringholder
        if (ringList.Count==0)
        {
            return true;
        }
        //checking for at least 3 ring
        if (ringList.Count < 3 )
        {
            return false;
        }

        //Checking for same Color

        RingColorType firstType = ringList[0].gameObject.GetComponent<Ring>().myColorType;
        foreach (GameObject ring in ringList)
        {
            if (ring.gameObject.GetComponent<Ring>().myColorType != firstType)
            {
                return false;
            }
        }

        return true;
    }
    void OnEnable()
    {
        EventManager.myLevelStarted += UpdateRingsPoses;
        EventManager.myLevelStarted += SetRingHolderOfRings;
        EventManager.ringsOrderChanged += CheckRingsForClickable;
        EventManager.myLevelStarted += CheckRingsForClickable;
    }
    void OnDisable()
    {
        EventManager.myLevelStarted -= UpdateRingsPoses;
        EventManager.myLevelStarted -= SetRingHolderOfRings;
        EventManager.ringsOrderChanged -= CheckRingsForClickable;
        EventManager.myLevelStarted -= CheckRingsForClickable;
    }
}
