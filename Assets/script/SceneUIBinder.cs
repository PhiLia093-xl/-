using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;

public class SceneUIBinder : MonoBehaviour
{
    public TextMeshProUGUI starText;
    public TextMeshProUGUI fireSeedText;

    private void Start()
    {
        GameManager.Instance.BindUI(starText, fireSeedText);
    }
}
