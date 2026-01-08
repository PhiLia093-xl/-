using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class LevelSelectMenu : MonoBehaviour
{
    public Button[] levelButtons;
    public string[] levelSceneNames;
    public Color lockedColor = Color.gray;
    public Color unlockedColor = Color.white;

    void Start()
    {
        int maxLevel = PlayerPrefs.GetInt("MaxLevel", 1);
        Debug.Log("当前最大解锁关卡：" + maxLevel);

        for (int i = 0; i < levelButtons.Length; i++)
        {
            int levelIndex = i + 1;
            bool unlocked = levelIndex <= maxLevel;

            levelButtons[i].interactable = unlocked;
            levelButtons[i].image.color = unlocked ? unlockedColor : lockedColor;
        }
    }
}
