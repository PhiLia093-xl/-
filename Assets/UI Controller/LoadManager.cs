using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class LoadManager : MonoBehaviour
{
    public GameObject loadScene;
    public Slider loadSlider;
    public Text loadText;
    public string NextScene;

    public void LoadNextScene()
    {
        StartCoroutine(LoadScene());
    }

    IEnumerator LoadScene()
    {
        loadScene.SetActive(true);

        AsyncOperation operation = SceneManager.LoadSceneAsync(NextScene);

        operation.allowSceneActivation = false;  //用于在加载完成后不立即跳转场景

        while (!operation.isDone)
        {
            loadSlider.value = operation.progress;

            loadText.text = operation.progress * 100 + "%";

            if(operation.progress >= 0.9f)
            {
                loadSlider.value = 1;

                loadText.text = "按下任意按键以继续……";

                if (Input.anyKeyDown)
                {
                    operation.allowSceneActivation = true;
                }
            }
            yield return null;
        }
    }
}
