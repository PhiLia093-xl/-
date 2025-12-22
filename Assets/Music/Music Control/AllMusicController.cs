using UnityEditor;
using UnityEngine;

public class AllPlayerController : MonoBehaviour
{

    void Awake()
    {
        //确保BGMPlayer跨场景时存在
        if (FindObjectsOfType<AllPlayerController>().Length > 1)
        {
            Destroy(gameObject);
            return;
        }

        //确保游戏对象在加载新场景是不被销毁
        DontDestroyOnLoad(gameObject);
    }


}