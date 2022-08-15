using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

//Basic LevelManager CLass that have some levelstart levelwin levelfailed type methods 
public class LevelManager : MonoBehaviour
{
    [SerializeField] private EventManager eventManager;
    [SerializeField] private GameObject[] levels;
    [SerializeField] private int currentLevel;
    [SerializeField] private Text levelText;
    [SerializeField] private GameObject winMenu;
    [SerializeField] private GameObject failMenu;
    private void Start()
    {
        currentLevel = PlayerPrefs.GetInt("level", 0);
        LoadCurrentlevel();
        eventManager.CallLevelStartedEvent();
        
    }
    private void LoadCurrentlevel() 
    {
        foreach (GameObject l in levels)
        {
            l.SetActive(false);
        }
        levels[currentLevel % levels.Length].SetActive(true);
        levelText.text = "Lv. "+currentLevel.ToString();
    }
    private void LevelCompleted()
    {
        currentLevel++;
        PlayerPrefs.SetInt("level", currentLevel);
        winMenu.SetActive(true);
    }
    public void RestartScene()
    {
        SceneManager.LoadScene(0);
    }
    private void LevelFailed()
    {
        failMenu.SetActive(true);
    }
    void OnEnable()
    {
        EventManager.myLevelCompleted += LevelCompleted;
        EventManager.myLevelFailed += LevelFailed;

    }
    void OnDisable()
    {
        EventManager.myLevelCompleted -= LevelCompleted;
        EventManager.myLevelFailed -= LevelFailed;

    }
}
