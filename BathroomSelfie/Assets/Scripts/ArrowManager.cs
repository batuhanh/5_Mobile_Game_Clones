using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum ArrowDirection
{
    Up,
    Down,
    Right,
    Left
}

public class ArrowManager : MonoBehaviour
{
    [SerializeField] private GameObject arrowPanel;
    private void OpenArrowPanel()
    {
        arrowPanel.SetActive(true);
    }
    void OnEnable()
    {
        EventManager.myLevelStarted += OpenArrowPanel;

    }
    void OnDisable()
    {
        EventManager.myLevelStarted -= OpenArrowPanel;

    }
}
