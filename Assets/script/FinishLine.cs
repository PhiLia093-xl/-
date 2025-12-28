using UnityEngine;
using UnityEngine.SceneManagement;

public class FinishLine : MonoBehaviour
{
    [Header("当前关卡编号")]
    public int currentLevel;

    [Header("下一个场景名（在 Inspector 中填写）")]
    public string nextSceneName;

    public void CompleteLevel()
    {
        Debug.Log("完成关卡：" + currentLevel);

        int maxLevel = PlayerPrefs.GetInt("MaxLevel", 1);

        if (currentLevel >= maxLevel)
        {
            PlayerPrefs.SetInt("MaxLevel", currentLevel + 1);
            PlayerPrefs.Save();
        }

        if (!string.IsNullOrEmpty(nextSceneName))
        {
            SceneManager.LoadScene(nextSceneName);
        }
        else
        {
            Debug.LogError("❌ 未设置 nextSceneName！");
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            Debug.Log("玩家进入终点");
            CompleteLevel();
        }
    }
}
