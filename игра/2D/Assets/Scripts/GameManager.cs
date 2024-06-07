using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    [SerializeField] GameObject GameOverPanel;
    [SerializeField] GameObject GameEndPanel;
    int lvlName;
    public void GameOver()
    {
        lvlName = SceneManager.GetActiveScene().buildIndex;
        PlayerPrefs.SetInt("L",lvlName);
      
        BackToMainMenu();
    }
    public void ExitGame()
    {
        Application.Quit();
    }
    public void BackToMainMenu()
    {
        SceneManager.LoadScene(0);
    }
   public void LoadSavedLevel()
    {
       LoadLevel(PlayerPrefs.GetInt("L",1));
    }
    public void LoadLevel(int index)
    {
        Time.timeScale = 1;
        SceneManager.LoadScene(index);
    }

    internal void LoadNextLevel()
    {
        if(SceneManager.GetActiveScene().buildIndex + 1 > 3) SceneManager.LoadScene(0);
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex+1);
    }
    internal void LoadThisLevel()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

     
    public void EndGame()
    {
        PlayerPrefs.DeleteAll();
        GameEndPanel.SetActive(true);
       //Time.timeScale = 0;
    }
}
