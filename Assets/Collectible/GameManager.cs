using UnityEngine;
using TMPro;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;

    public int currentStarCount = 0;
    public int totalStarCount = 0;

    public int currentFireSeedCount = 0;
    public int totalFireSeedCount = 0;

    public TextMeshProUGUI starText;
    public TextMeshProUGUI fireSeedText;

    private void Awake()
    {
        if (Instance == null) Instance = this;
        else Destroy(gameObject);
    }

    private void Start()
    {
        // 统计场景中所有星穹和火种数量
        totalStarCount = GameObject.FindGameObjectsWithTag("Star").Length;
        totalFireSeedCount = GameObject.FindGameObjectsWithTag("FireSeed").Length;

        UpdateUI();
    }

    public void AddStar(int amount)
    {
        currentStarCount += amount;
        UpdateUI();
    }

    public void AddFireSeed(int amount)
    {
        currentFireSeedCount += amount;
        UpdateUI();
    }

    private void UpdateUI()
    {
        if (starText != null)
        {
            starText.text = currentStarCount + " / " + totalStarCount;
        }
        if (fireSeedText != null)
        {
            fireSeedText.text = currentFireSeedCount + " / " + totalFireSeedCount;
        }
    }
}
