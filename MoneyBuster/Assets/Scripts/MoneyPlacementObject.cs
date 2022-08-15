using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum MoneyPlacementType
{
    Shredder,
    Stacker
}
//this class is holding same variables for Stacker and Shredder and they are inheriting from this class
public class MoneyPlacementObject : MonoBehaviour
{
    public MoneyPlacementType myMoneyPlacementType;
    public Animator myAnim;
    public EventManager eventManager;

}
