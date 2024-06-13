using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenuManager : MonoBehaviour
{
    [SerializeField] GameObject settingsMenue;

    // при запуске скраваем панель настроек
    private void Start()
    {
        settingsMenue.SetActive(false);
    }
    // при нажатти на кнопу откываем панель настроек
    public void EnterSettings()
    {
        settingsMenue.SetActive(true);
    }

}
