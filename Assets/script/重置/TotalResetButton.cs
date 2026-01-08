using UnityEngine;
using UnityEngine.SceneManagement;

public class TotalResetButton : MonoBehaviour
{
    [Header("重置后返回的关卡（一般是第一关）")]
    public string firstLevelSceneName = "Level1";

    public void OnTotalReset()
    {
        // 执行总重置
        GameManager.Instance.TotalReset();

        
    }
}
