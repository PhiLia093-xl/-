using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;
    //为外部提供接口

    // levelName -> collected keys
    private Dictionary<string, HashSet<string>> collectedByLevel = new Dictionary<string, HashSet<string>>();
    //对于Dictionary内部的两个变量
    //键（Key）类型：string → 表示关卡名称（如 "Level1"）
    //值（Value）类型：HashSet<string> → 表示该关卡中所有已收集物品的唯一 ID 集合

    private string CurrentLevel => SceneManager.GetActiveScene().name;//获取当前场景，使用=>进行动态字段实时更新

    // UI（旧 Text）
    private Text starText;
    private Text fireSeedText;
    //两个可拾取物的Text
    
    public int currentStarCount => GetStarCount(CurrentLevel);
    public int currentFireSeedCount => GetFireSeedCount(CurrentLevel);

    private void Awake()
    {
        if (Instance != null)
        {
            Destroy(gameObject);
            return;
        }
        //若Instance为空那就创建一个单例并将GameManager赋值给它
        Instance = this;
        DontDestroyOnLoad(gameObject);// 确保该对象在场景切换时不被销毁
        Load();//后面写的函数
    }

    /* ================= 收集 ================= */

    public void AddStar(string id) => Add("Star_" + id);
    public void AddFireSeed(string id) => Add("FireSeed_" + id);

    private void Add(string key)
    {
        if (!collectedByLevel.ContainsKey(CurrentLevel))
            collectedByLevel[CurrentLevel] = new HashSet<string>();

        if (!collectedByLevel[CurrentLevel].Add(key))//尝试对形参进行拾取，如果没有被Add的过，则进行Add
            return;

        Save();//对收集进行存档，后面有写
        UpdateUI();//更新UI
    }

    public bool IsCollected(string key)
    {
        return collectedByLevel.ContainsKey(CurrentLevel) && collectedByLevel[CurrentLevel].Contains(key);
    }
    //如果已经被拾取过，返回true
    /* ================= 统计 ================= */

    public int GetStarCount(string level)
    {
        if (!collectedByLevel.ContainsKey(level)) 
            return 0;
        int c = 0;
        foreach (var k in collectedByLevel[level])
            if (k.StartsWith("Star_")) 
                c++;
        return c;
    }

    public int GetFireSeedCount(string level)
    {
        if (!collectedByLevel.ContainsKey(level))
            return 0;
        int c = 0;
        foreach (var k in collectedByLevel[level])
            if (k.StartsWith("FireSeed_")) 
                c++;
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
        if (starText) 
            starText.text = currentStarCount.ToString();//ToString把数字转为字符串
        if (fireSeedText) 
            fireSeedText.text = currentFireSeedCount.ToString();
    }

    /* ================= 重置 ================= */

    

    public void ResetAll()
    {
        collectedByLevel.Clear();//清除一切存储的数据
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
    //我真的看不太懂

    private void Save()
    {
        var data = new SaveData(collectedByLevel);
        PlayerPrefs.SetString("COLLECT_DATA", JsonUtility.ToJson(data));
        PlayerPrefs.Save();
    }

    private void Load()
    {
        if (!PlayerPrefs.HasKey("COLLECT_DATA")) 
            return;
        var data = JsonUtility.FromJson<SaveData>(PlayerPrefs.GetString("COLLECT_DATA"));
        collectedByLevel = data.ToDictionary();
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
