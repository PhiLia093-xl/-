using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;

    // ===== 数据 =====
    private Dictionary<string, List<string>> collectedIDsByLevel =
        new Dictionary<string, List<string>>();

    private string lockedLevelName;

    // ===== UI（旧 Text）=====
    private Text starText_UI;
    private Text fireSeedText_UI;

    // ===== 兼容旧接口 =====
    public int currentStarCount => GetStarCount(lockedLevelName);
    public int currentFireSeedCount => GetFireSeedCount(lockedLevelName);

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);

            LoadData();
            lockedLevelName = SceneManager.GetActiveScene().name;
        }
        else
        {
            Destroy(gameObject);
        }
    }

    // ================= 收集 =================

    public void AddStar(string id)
    {
        AddCollectible("Star_" + id);
    }

    public void AddFireSeed(string id)
    {
        AddCollectible("FireSeed_" + id);
    }

    private void AddCollectible(string key)
    {
        if (!collectedIDsByLevel.ContainsKey(lockedLevelName))
            collectedIDsByLevel[lockedLevelName] = new List<string>();

        if (collectedIDsByLevel[lockedLevelName].Contains(key))
            return;

        collectedIDsByLevel[lockedLevelName].Add(key);
        SaveData();
        UpdateUI(lockedLevelName);
    }

    public bool IsCollected(string key)
    {
        return collectedIDsByLevel.ContainsKey(lockedLevelName) &&
               collectedIDsByLevel[lockedLevelName].Contains(key);
    }

    // ================= 统计 =================

    public int GetStarCount(string levelName)
    {
        if (!collectedIDsByLevel.ContainsKey(levelName)) return 0;

        int count = 0;
        foreach (var id in collectedIDsByLevel[levelName])
            if (id.StartsWith("Star_")) count++;
        return count;
    }

    public int GetFireSeedCount(string levelName)
    {
        if (!collectedIDsByLevel.ContainsKey(levelName)) return 0;

        int count = 0;
        foreach (var id in collectedIDsByLevel[levelName])
            if (id.StartsWith("FireSeed_")) count++;
        return count;
    }

    // ================= UI =================

    public void BindUI(Text starUI, Text fireSeedUI)
    {
        starText_UI = starUI;
        fireSeedText_UI = fireSeedUI;
        UpdateUI(lockedLevelName);
    }

    public void UpdateUI(string levelName)
    {
        if (starText_UI != null)
            starText_UI.text = GetStarCount(levelName).ToString();

        if (fireSeedText_UI != null)
            fireSeedText_UI.text = GetFireSeedCount(levelName).ToString();
    }

    // ================= 重置（兼容） =================

    public void ResetCurrentLevel()
    {
        if (collectedIDsByLevel.ContainsKey(lockedLevelName))
            collectedIDsByLevel[lockedLevelName].Clear();

        SaveData();
        UpdateUI(lockedLevelName);
    }

    public void ResetAll()
    {
        collectedIDsByLevel.Clear();
        SaveData();
        UpdateUI(lockedLevelName);
    }

    // ================= 存档 =================

    private void SaveData()
    {
        string json = JsonUtility.ToJson(new SaveWrapper(collectedIDsByLevel));
        PlayerPrefs.SetString("CollectSave", json);
        PlayerPrefs.Save();
    }

    private void LoadData()
    {
        if (!PlayerPrefs.HasKey("CollectSave")) return;

        string json = PlayerPrefs.GetString("CollectSave");
        SaveWrapper wrapper = JsonUtility.FromJson<SaveWrapper>(json);

        collectedIDsByLevel = wrapper.data ??
                              new Dictionary<string, List<string>>();
    }

    [System.Serializable]
    private class SaveWrapper
    {
        public Dictionary<string, List<string>> data;
        public SaveWrapper(Dictionary<string, List<string>> d) { data = d; }
    }
}
