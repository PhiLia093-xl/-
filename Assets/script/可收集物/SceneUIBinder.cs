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
//只是提供一个可以自由修改两个Text的地方，实际是BindUI函数起到作用
