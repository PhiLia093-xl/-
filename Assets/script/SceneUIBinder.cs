using UnityEngine;
using UnityEngine.UI;

public class SceneUIBinder : MonoBehaviour
{
    public Text starText;
    public Text fireSeedText;

    private void Start()
    {
        GameManager.Instance.BindUI(starText, fireSeedText);
    }
}
