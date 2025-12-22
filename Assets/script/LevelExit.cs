using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement; // 引入场景管理命名空间

public class LevelExit : MonoBehaviour
{
    [SerializeField] private string levelToLoad; // 指定要加载的下一个场景名称
    private bool levelCompleted = false; // 防止重复触发

    private void OnTriggerEnter2D(Collider2D collision)
    {
        // 1. 检查碰撞对象是否是玩家
        if (collision.CompareTag("Player") && !levelCompleted)
        {
            levelCompleted = true; // 标记关卡已完成

            // 2. 播放效果（可选）
            // 例如：播放音效、触发动画（旗帜升起）
            // GetComponent<AudioSource>().Play();
            // GetComponent<Animator>().SetTrigger("Victory");

            // 3. 调用结束关卡协程
            StartCoroutine(CompleteLevelAfterDelay(1.5f)); // 延迟1.5秒后完成关卡
        }
    }

    private System.Collections.IEnumerator CompleteLevelAfterDelay(float delay)
    {
        // 等待指定秒数，让玩家看到效果
        yield return new WaitForSeconds(delay);

        // 4. 加载下一个场景或显示结束界面
        SceneManager.LoadScene(levelToLoad);
    }
}