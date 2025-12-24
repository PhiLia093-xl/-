using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Button : MonoBehaviour
{

    public void StartToChoose()
    {
        SceneManager.LoadScene("Choose");
    }

    public void StartToSet()
    {
        SceneManager.LoadScene("Set");
    }
    public void ToStart()
    {
        SceneManager.LoadScene("Start");
    }
    public void ToTeach()
    {
        SceneManager.LoadScene("Teach");
    }
    public void ExitGame()
    {
        Application.Quit();
    }
}