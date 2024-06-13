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
    //Вызывается при смерти игрока (если нет сердец) и возврат в гл меню
    public void GameOver()
    {
        lvlName = SceneManager.GetActiveScene().buildIndex;
        PlayerPrefs.SetInt("L",lvlName);
      
        BackToMainMenu();
    }
    // для выхода из игры 
    public void ExitGame()
    {
        Application.Quit();
    }
    // для входа в гл меню
    public void BackToMainMenu()
    {
        SceneManager.LoadScene(0);
    }
    // для загрузки сох уровня
    public void LoadSavedLevel()
    {
       LoadLevel(PlayerPrefs.GetInt("L",1));
    }
    // загрузка уровня по его индексу
    public void LoadLevel(int index)
    {
        Time.timeScale = 1;
        SceneManager.LoadScene(index);
    }
    // загрузка сл уровня
    internal void LoadNextLevel()
    {
        if(SceneManager.GetActiveScene().buildIndex + 1 > 3) SceneManager.LoadScene(0);
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex+1);
    }
    // загрузка текущего уровня 
    internal void LoadThisLevel()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    // для конца игры (прохождения)
    public void EndGame()
    {
        PlayerPrefs.DeleteAll();
        GameEndPanel.SetActive(true);
       //Time.timeScale = 0;
    }
}
