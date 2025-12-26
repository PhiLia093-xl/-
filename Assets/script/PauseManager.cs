using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PauseManager : MonoBehaviour
{
    public GameObject pausePanel;
    public Canvas targetCanvas;
    private bool isPaused = false;

    void Start()
    {
        isPaused = false;
        pausePanel.SetActive(false);
        Time.timeScale = 1f;
        if (targetCanvas == null)
            targetCanvas = GetComponent<Canvas>();
    }


    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            TogglePause();
            if (targetCanvas.sortingLayerName == "pause")
                targetCanvas.sortingLayerName = "background";
            else targetCanvas.sortingLayerName = "pause";
            
        }
    }

    public void TogglePause()
    {
        isPaused = !isPaused;

        pausePanel.SetActive(isPaused);

        Time.timeScale = isPaused ? 0f : 1f;
    }

    
    
}