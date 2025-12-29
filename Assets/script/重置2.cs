using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class 重置2: MonoBehaviour
{
    public void OnResetClicked()
    {
        GameManager.Instance.ResetAll();
        UnityEngine.SceneManagement.SceneManager.LoadScene(
            UnityEngine.SceneManagement.SceneManager.GetActiveScene().name
        );
    }
}
