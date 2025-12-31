using UnityEngine;
using UnityEngine.UI;

public class FireSeedCornerUI : MonoBehaviour
{
    public Text starText;       // 右上角显示星琼数量
    public Text fireSeedText;   // 右上角显示火种数量

    private void Start()
    {
        if (GameManager.Instance != null)
        {
            GameManager.Instance.BindUI(starText, fireSeedText);
        }
    }

    private void Update()
    {
        // 持续更新UI，确保数字实时显示
        if (GameManager.Instance != null)
        {
            GameManager.Instance.UpdateUI(UnityEngine.SceneManagement.SceneManager.GetActiveScene().name);
        }
    }
}
