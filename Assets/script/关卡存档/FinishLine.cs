using UnityEngine;
using UnityEngine.SceneManagement;

public class FinishLine : MonoBehaviour
{
    
    public int currentLevel;
    public string nextSceneName;

    public void CompleteLevel()//一个更新最新的最大可玩关卡数和切换场景的函数
    {
        

        int maxLevel = PlayerPrefs.GetInt("MaxLevel", 1);//读取Maxlevel的值，未读取到就代1

        if (currentLevel >= maxLevel)
        {
            PlayerPrefs.SetInt("MaxLevel", currentLevel + 1);//存储Maxlevel的值
            PlayerPrefs.Save();//防止游戏突然崩溃
        }

        if (!string.IsNullOrEmpty(nextSceneName))//判断string是不是NULL或者空，若不是则执行下面的操作
        {
            SceneManager.LoadScene(nextSceneName);
        }
        
    }

    private void OnTriggerEnter2D(Collider2D other)//碰到终点时使用函数
    {
        if (other.CompareTag("Player"))
        {
            
            CompleteLevel();
        }
    }
}
