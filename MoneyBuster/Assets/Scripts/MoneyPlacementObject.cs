using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum MoneyPlacementType
{
    Shredder,
    Stacker
}
public class MoneyPlacementObject : MonoBehaviour
{
    public MoneyPlacementType myMoneyPlacementType;
    public Animator myAnim;
    public EventManager eventManager;

}
