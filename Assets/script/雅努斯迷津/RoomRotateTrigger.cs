using UnityEngine;

public class RoomRotateTrigger : MonoBehaviour
{
    private RoomAutoReturnRotator_RoomOnly rotator;

    void Awake()
    {
        rotator = GetComponentInParent<RoomAutoReturnRotator_RoomOnly>();
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            rotator.SetCanRotate(true);
        }
    }

    void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            rotator.SetCanRotate(false);
        }
    }
}
