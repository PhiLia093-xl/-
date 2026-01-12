using UnityEngine;

public class SmoothCameraZoom : MonoBehaviour
{
    public float zoomedSize = 3f;      // 放大时的尺寸（值越小，画面越放大）
    public float normalSize = 5f;      // 正常尺寸
    public float smoothTime = 0.5f;    // 平滑过渡时间（秒），值越小越快

    private Camera cam;
    private float currentVelocity = 0f;
    private bool isZoomed = false;

    void Start()
    {
        cam = Camera.main;
        
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            isZoomed = true;
        }
    }

    void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            isZoomed = false;
        }
    }

    void LateUpdate()//LateUpdate确保玩家移动完后再调整镜头
    {
        float targetSize = isZoomed ? zoomedSize : normalSize;
        cam.orthographicSize = Mathf.SmoothDamp(cam.orthographicSize, targetSize, ref currentVelocity, smoothTime);
        
    }
}