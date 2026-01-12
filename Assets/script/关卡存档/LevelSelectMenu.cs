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
        int maxLevel = PlayerPrefs.GetInt("MaxLevel", 1);//MaxLevel属于PlayerPref里面的字段，会自动生成
        

        for (int i = 0; i < levelButtons.Length; i++)//对选关界面的按钮进行颜色和可否点击的改变
        {
            int levelIndex = i + 1;
            bool unlocked = levelIndex <= maxLevel;

            levelButtons[i].interactable = unlocked;
            levelButtons[i].image.color = unlocked ? unlockedColor : lockedColor;
        }
    }
}
