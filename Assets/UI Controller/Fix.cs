using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Fix : MonoBehaviour
{

    public void SceneChange()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene("Start");
    }
}
