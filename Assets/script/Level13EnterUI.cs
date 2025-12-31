using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System.Collections;

public class Level13EnterUI : MonoBehaviour
{
    public Button level13Button; // 选关按钮
    public Text tipText;         // 提示文字，只用旧 Text
    public float tipFadeTime = 2f; // 提示文字淡出时间

    private void Start()
    {
        if (level13Button != null)
            level13Button.onClick.AddListener(OnClickLevel13);

        if (tipText != null)
            tipText.text = "";
    }

    private void OnClickLevel13()
    {
        // 检查全局火种总数是否达到12
        if (GameManager.Instance.currentFireSeedCount < 12)
        {
            // 提示文字
            tipText.text = "请集齐12火种，完成再创世";

            // 停止之前的协程，重新淡出
            StopAllCoroutines();
            StartCoroutine(FadeTip());
        }
        else
        {
            // 集齐火种，直接进入第13关
            SceneManager.LoadScene("Level13");
        }
    }

    private IEnumerator FadeTip()
    {
        float elapsed = 0f;
        Color c = tipText.color;
        c.a = 1f;
        tipText.color = c;

        while (elapsed < tipFadeTime)
        {
            elapsed += Time.deltaTime;
            c.a = Mathf.Lerp(1f, 0f, elapsed / tipFadeTime);
            tipText.color = c;
            yield return null;
        }

        tipText.text = "";
    }
}
