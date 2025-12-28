using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class 重置 : MonoBehaviour
{
    // Start is called before the first frame update
    public void ResetProgress()
    {
        PlayerPrefs.DeleteKey("MaxLevel");
        PlayerPrefs.Save();
        Debug.Log("进度已重置");
    }

}
