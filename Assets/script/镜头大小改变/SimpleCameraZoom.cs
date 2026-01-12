using UnityEngine;

public class SimpleCameraZoom : MonoBehaviour
{
    public float zoomInSize = 3f;   // 放大时的摄像机尺寸（越小越放大）
    public float defaultSize = 5f;  // 默认摄像机尺寸

    private Camera cam;

    void Start()
    {
        cam = Camera.main;
        cam.orthographicSize = defaultSize;
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            cam.orthographicSize = zoomInSize;
        }
    }

    void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            cam.orthographicSize = defaultSize;
        }
    }
}