using UnityEngine;

public enum CollectibleType
{
    Star,
    FireSeed
}

public class Collectible : MonoBehaviour
{
    public CollectibleType type;
    public string uniqueID;
    public AudioClip collectSound;

    private void Start()
    {
        string key = type.ToString() + "_" + uniqueID;

        if (GameManager.Instance.IsCollected(key))
        {
            Destroy(gameObject);
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (!other.CompareTag("Player")) return;

        if (collectSound != null)
            AudioSource.PlayClipAtPoint(collectSound, transform.position);

        if (type == CollectibleType.Star)
            GameManager.Instance.AddStar(uniqueID);
        else
            GameManager.Instance.AddFireSeed(uniqueID);

        Destroy(gameObject);
    }
}
