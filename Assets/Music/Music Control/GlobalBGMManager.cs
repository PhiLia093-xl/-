// GlobalBGMManager.cs
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.SceneManagement;

[RequireComponent(typeof(AudioSource))]
public class GlobalBGMManager : MonoBehaviour
{
    public static GlobalBGMManager Instance { get; private set; }

    [Header("Audio Settings")]
    public AudioMixer audioMixer;
    public string volumeParamName = "BGMVolume"; // 必须与 AudioMixer 中暴露的参数名一致

    [Header("Scene to BGM Mapping")]
    public AudioClip mainMenuBGM;      // Esc 按下时播放的音乐（如主菜单）
    public AudioClip defaultBGM;       // 默认 BGM（用于未配置的场景）

    [System.Serializable]
    public class SceneBGM
    {
        public string sceneName;
        public AudioClip bgm;
    }
    public SceneBGM[] sceneBGMs;

    private AudioSource audioSource;
    private float currentVolumeDB = -10f; // 默认音量（dB）
    private bool isPaused = false;

    void Awake()
    {
        if (Instance != null)
        {
            Destroy(gameObject);
            return;
        }
        Instance = this;
        DontDestroyOnLoad(gameObject);

        audioSource = gameObject.AddComponent<AudioSource>();
        audioSource.playOnAwake = false;
        audioSource.loop = true;

        // 初始化音量
        SetVolume(currentVolumeDB);
    }

    void OnEnable()
    {
        SceneManager.sceneLoaded += OnSceneLoaded;
    }

    void OnDisable()
    {
        SceneManager.sceneLoaded -= OnSceneLoaded;
    }

    void Update()
    {
        // 按下 Esc 切换到主菜单 BGM（模拟暂停菜单）
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            TogglePauseBGM();
        }
    }

    void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        PlayBGMForScene(scene.name);
    }

    void PlayBGMForScene(string sceneName)
    {
        isPaused = false; // 自动退出暂停状态

        var mapping = System.Array.Find(sceneBGMs, x => x.sceneName == sceneName);
        AudioClip clipToPlay = mapping?.bgm ?? defaultBGM;

        if (clipToPlay != null && audioSource.clip != clipToPlay)
        {
            audioSource.clip = clipToPlay;
            audioSource.Play();
        }
    }

    void TogglePauseBGM()
    {
        isPaused = !isPaused;

        if (isPaused)
        {
            // 暂停当前 BGM，播放主菜单音乐
            if (mainMenuBGM != null)
            {
                audioSource.Pause(); // 先暂停当前
                audioSource.clip = mainMenuBGM;
                audioSource.Play();
            }
        }
        else
        {
            // 恢复原场景 BGM
            Scene currentScene = SceneManager.GetActiveScene();
            PlayBGMForScene(currentScene.name);
        }
    }

    public void SetVolume(float volumeInDB)
    {
        currentVolumeDB = Mathf.Clamp(volumeInDB, -80f, 0f);
        audioMixer.SetFloat(volumeParamName, currentVolumeDB);
    }

    public void SetMute(bool muted)
    {
        float vol = muted ? -80f : currentVolumeDB;
        audioMixer.SetFloat(volumeParamName, vol);
    }
}
