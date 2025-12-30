using UnityEngine;

public class ResetButton : MonoBehaviour
{
    public void OnResetClicked()
    {
        GameManager.Instance.ResetAll();
        UnityEngine.SceneManagement.SceneManager.LoadScene(
            UnityEngine.SceneManagement.SceneManager.GetActiveScene().name
        );
    }
}
