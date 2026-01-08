using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;

    // levelName -> collected keys
    private Dictionary<string, HashSet<string>> collectedByLevel =
        new Dictionary<string, HashSet<string>>();

    private string CurrentLevel => SceneManager.GetActiveScene().name;

    // UI（旧 Text）
    private Text starText;
    private Text fireSeedText;

    // ===== 兼容旧代码 =====
    public int currentStarCount => GetStarCount(CurrentLevel);
    public int currentFireSeedCount => GetFireSeedCount(CurrentLevel);

    private void Awake()
    {
        if (Instance != null)
        {
            Destroy(gameObject);
            return;
        }

        Instance = this;
        DontDestroyOnLoad(gameObject);
        Load();
    }

    /* ================= 收集 ================= */

    public void AddStar(string id) => Add("Star_" + id);
    public void AddFireSeed(string id) => Add("FireSeed_" + id);

    private void Add(string key)
    {
        if (!collectedByLevel.ContainsKey(CurrentLevel))
            collectedByLevel[CurrentLevel] = new HashSet<string>();

        if (!collectedByLevel[CurrentLevel].Add(key))
            return;

        Save();
        UpdateUI();
    }

    public bool IsCollected(string key)
    {
        return collectedByLevel.ContainsKey(CurrentLevel) &&
               collectedByLevel[CurrentLevel].Contains(key);
    }

    /* ================= 统计 ================= */

    public int GetStarCount(string level)
    {
        if (!collectedByLevel.ContainsKey(level)) return 0;
        int c = 0;
        foreach (var k in collectedByLevel[level])
            if (k.StartsWith("Star_")) c++;
        return c;
    }

    public int GetFireSeedCount(string level)
    {
        if (!collectedByLevel.ContainsKey(level)) return 0;
        int c = 0;
        foreach (var k in collectedByLevel[level])
            if (k.StartsWith("FireSeed_")) c++;
        return c;
    }

    /* ================= UI ================= */

    public void BindUI(Text star, Text fire)
    {
        starText = star;
        fireSeedText = fire;
        UpdateUI();
    }

    private void UpdateUI()
    {
        if (starText) starText.text = currentStarCount.ToString();
        if (fireSeedText) fireSeedText.text = currentFireSeedCount.ToString();
    }

    /* ================= 重置 ================= */

    public void ResetCurrentLevel()
    {
        collectedByLevel.Remove(CurrentLevel);
        Save();
        UpdateUI();
    }

    public void ResetAll()
    {
        collectedByLevel.Clear();
        Save();
        UpdateUI();
    }
    // ================== 总重置（火种 + 星琼 + 关卡） ==================
    public void TotalReset()
    {
        // 1. 清空所有收集数据
        ResetAll();

        // 2. 重置关卡解锁进度（只剩第一关）
        PlayerPrefs.SetInt("MaxLevel", 1);

        // 3. 保存
        PlayerPrefs.Save();
    }

    /* ================= 存档 ================= */

    private void Save()
    {
        var data = new SaveData(collectedByLevel);
        PlayerPrefs.SetString("COLLECT_DATA", JsonUtility.ToJson(data));
        PlayerPrefs.Save();
    }

    private void Load()
    {
        if (!PlayerPrefs.HasKey("COLLECT_DATA")) return;
        var data = JsonUtility.FromJson<SaveData>(PlayerPrefs.GetString("COLLECT_DATA"));
        collectedByLevel = data.ToDictionary();
    }
    // ================== 生命周期兜底保存 ==================
    private void OnApplicationQuit()
    {
        Save();
    }

    


    [System.Serializable]
    private class SaveData
    {
        public List<string> levels = new();
        public List<List<string>> values = new();

        public SaveData(Dictionary<string, HashSet<string>> dict)
        {
            foreach (var kv in dict)
            {
                levels.Add(kv.Key);
                values.Add(new List<string>(kv.Value));
            }
        }

        public Dictionary<string, HashSet<string>> ToDictionary()
        {
            var d = new Dictionary<string, HashSet<string>>();
            for (int i = 0; i < levels.Count; i++)
                d[levels[i]] = new HashSet<string>(values[i]);
            return d;
        }
        

    }
}
