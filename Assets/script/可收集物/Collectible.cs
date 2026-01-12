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
    //当检测到此物体已经收集时，销毁此物体
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (!other.CompareTag("Player")) 
            return;

        if (collectSound)
            AudioSource.PlayClipAtPoint(collectSound, transform.position);
        //在收集处播放音乐
        if (type == CollectibleType.Star)
            GameManager.Instance.AddStar(uniqueID);
        else
            GameManager.Instance.AddFireSeed(uniqueID);
        //对星琼和火种数量进行累加
        Destroy(gameObject);
    }
}
