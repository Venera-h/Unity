using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PausePanelController : MonoBehaviour
{
    [SerializeField] GameObject pausePanel;

    bool isPaused = false;


    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape) && !isPaused)
        {
            isPaused = true;
            pausePanel.SetActive(true);
            Time.timeScale = 0f;
        }
        else if(Input.GetKeyDown(KeyCode.Escape) && isPaused )
        {
            isPaused = false;
            pausePanel.SetActive(false);
            Time.timeScale = 1.0f;
        }
    }
}
