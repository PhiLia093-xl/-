using System.Collections.Generic;
using UnityEngine;
using TMPro;

[System.Serializable]
public class SaveData
{
    public List<string> collectedIDs;
    public int starCount;
    public int fireSeedCount;
}

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;

    public int currentStarCount;
    public int currentFireSeedCount;

    private List<string> collectedIDs = new List<string>();

    private TextMeshProUGUI starText;
    private TextMeshProUGUI fireSeedText;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
            LoadData();
        }
        else Destroy(gameObject);
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
            SaveData();
            UpdateUI();
        }
    }

    public void AddFireSeed(string id)
    {
        if (!collectedIDs.Contains(id))
        {
            collectedIDs.Add(id);
            currentFireSeedCount++;
            SaveData();
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
            starText.text = currentStarCount + " / " + FindTotal("Star");
        if (fireSeedText != null)
            fireSeedText.text = currentFireSeedCount + " / " + FindTotal("FireSeed");
    }

    private int FindTotal(string tag)
    {
        return GameObject.FindGameObjectsWithTag(tag).Length + CollectedCountOfTag(tag);
    }

    private int CollectedCountOfTag(string tag)
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
        SaveData();
        UpdateUI();
    }

    public void SaveData()
    {
        SaveData data = new SaveData();
        data.collectedIDs = collectedIDs;
        data.starCount = currentStarCount;
        data.fireSeedCount = currentFireSeedCount;

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
        }
    }
}
