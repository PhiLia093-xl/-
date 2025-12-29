using UnityEngine;

public enum CollectibleType
{
    Star,
    FireSeed
}

public class Collectible : MonoBehaviour
{
    public CollectibleType type;
    public AudioClip collectSound;

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            if (collectSound != null)
                AudioSource.PlayClipAtPoint(collectSound, transform.position);

            switch (type)
            {
                case CollectibleType.Star:
                    GameManager.Instance.AddStar(1);
                    break;
                case CollectibleType.FireSeed:
                    GameManager.Instance.AddFireSeed(1);
                    break;
            }

            Destroy(gameObject);
        }
    }
}
