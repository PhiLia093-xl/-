using UnityEngine;

public class RoomAutoReturnRotator_RoomOnly : MonoBehaviour
{
    [Header("Rotation")]
    public float rotateSpeed = 60f;     // 按住E旋转速度
    public float returnSpeed = 80f;     // 松手回正速度
    public float snapThreshold = 0.5f;  // 回正吸附阈值

    private float currentAngle = 0f;    // 自己维护的角度
    private bool canRotate = false;     // ⭐是否允许旋转（核心）

    void Update()
    {
        if (!canRotate)
            return;

        if (Input.GetKey(KeyCode.E))
        {
            // 按住E：逆时针旋转
            currentAngle += rotateSpeed * Time.deltaTime;
        }
        else
        {
            // 松手：自动回正
            if (Mathf.Abs(currentAngle) > snapThreshold)
            {
                currentAngle = Mathf.MoveTowards(currentAngle,0f, returnSpeed * Time.deltaTime);


            }
            else
            {
                currentAngle = 0f;
            }
        }

        transform.rotation = Quaternion.Euler(0f, 0f, currentAngle);
    }

    // 给 Trigger 调用
    public void SetCanRotate(bool value)
    {
        canRotate = value;

        // 离开房间时，强制开始回正
        if (!canRotate && Mathf.Abs(currentAngle) > 0.01f)
        {
            currentAngle = Mathf.MoveTowards(currentAngle, 0f, returnSpeed * Time.deltaTime);
            // 不清角度，让它自然回正
        }
    }
}
