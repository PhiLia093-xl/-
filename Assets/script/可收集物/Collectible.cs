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

    private string Key => type + "_" + uniqueID;

    private void Start()
    {
        if (GameManager.Instance.IsCollected(Key))
            Destroy(gameObject);
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (!other.CompareTag("Player")) return;

        if (collectSound)
            AudioSource.PlayClipAtPoint(collectSound, transform.position);

        if (type == CollectibleType.Star)
            GameManager.Instance.AddStar(uniqueID);
        else
            GameManager.Instance.AddFireSeed(uniqueID);

        Destroy(gameObject);
    }
}
