using System.Collections.Generic;
using UnityEngine;
using TMPro;

[System.Serializable]
public class SaveData
{
    public List<string> collectedIDs;
    public int starCount;
    public int fireSeedCount;

    public int totalStarCount;
    public int totalFireSeedCount;
}

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;

    public int currentStarCount;
    public int currentFireSeedCount;

    private List<string> collectedIDs = new List<string>();

    private TextMeshProUGUI starText;
    private TextMeshProUGUI fireSeedText;

    private int totalStarCount = -1;
    private int totalFireSeedCount = -1;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
            LoadData(); // 读取存档
        }
        else
        {
            Destroy(gameObject);
        }
    }

    public void BindUI(TextMeshProUGUI starUI, TextMeshProUGUI fireSeedUI)
    {
        starText = starUI;
        fireSeedText = fireSeedUI;
        UpdateUI();
    }

    public void AddStar(string id)
    {
        if (!collectedIDs.Contains(id))
        {
            collectedIDs.Add(id);
            currentStarCount++;
            SaveDataToPrefs();
            UpdateUI();
        }
    }

    public void AddFireSeed(string id)
    {
        if (!collectedIDs.Contains(id))
        {
            collectedIDs.Add(id);
            currentFireSeedCount++;
            SaveDataToPrefs();
            UpdateUI();
        }
    }

    public bool IsCollected(string id)
    {
        return collectedIDs.Contains(id);
    }

    private void UpdateUI()
    {
        if (starText != null)
            starText.text = currentStarCount + " / " + GetTotalStars();
        if (fireSeedText != null)
            fireSeedText.text = currentFireSeedCount + " / " + GetTotalFireSeeds();
    }

    public int GetTotalStars()
    {
        // 如果还没记录过总数，则统计一次，并保存在内存和存档里
        if (totalStarCount < 0)
        {
            totalStarCount = GameObject.FindGameObjectsWithTag("Star").Length + CountCollectedWithTag("Star");
        }
        return totalStarCount;
    }

    public int GetTotalFireSeeds()
    {
        if (totalFireSeedCount < 0)
        {
            totalFireSeedCount = GameObject.FindGameObjectsWithTag("FireSeed").Length + CountCollectedWithTag("FireSeed");
        }
        return totalFireSeedCount;
    }

    private int CountCollectedWithTag(string tag)
    {
        int count = 0;
        foreach (string id in collectedIDs)
            if (id.StartsWith(tag)) count++;
        return count;
    }

    public void ResetAll()
    {
        collectedIDs.Clear();
        currentStarCount = 0;
        currentFireSeedCount = 0;
        totalStarCount = -1;       // 重置总数
        totalFireSeedCount = -1;
        SaveDataToPrefs();
        UpdateUI();
    }

    public void SaveDataToPrefs()
    {
        SaveData data = new SaveData();
        data.collectedIDs = collectedIDs;
        data.starCount = currentStarCount;
        data.fireSeedCount = currentFireSeedCount;
        data.totalStarCount = totalStarCount;
        data.totalFireSeedCount = totalFireSeedCount;

        string json = JsonUtility.ToJson(data);
        PlayerPrefs.SetString("CollectSave", json);
        PlayerPrefs.Save();
    }

    public void LoadData()
    {
        if (PlayerPrefs.HasKey("CollectSave"))
        {
            string json = PlayerPrefs.GetString("CollectSave");
            SaveData data = JsonUtility.FromJson<SaveData>(json);

            collectedIDs = data.collectedIDs != null ? data.collectedIDs : new List<string>();
            currentStarCount = data.starCount;
            currentFireSeedCount = data.fireSeedCount;

            totalStarCount = data.totalStarCount;
            totalFireSeedCount = data.totalFireSeedCount;
        }
    }
}
