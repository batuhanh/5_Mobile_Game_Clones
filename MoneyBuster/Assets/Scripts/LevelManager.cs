using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelManager : MonoBehaviour
{
    [SerializeField] private EventManager eventManager;
    private void Start()
    {
        eventManager.CallLevelStartedEvent();
    }
}
