using UnityEngine;

public class RoomAutoReturnRotator_RoomOnly : MonoBehaviour
{
    
    public float rotateSpeed = 60f;     // 按住E旋转速度
    public float returnSpeed = 80f;     // 松手回正速度
    public float snapThreshold = 0.5f;  // 回正吸附阈值

    private float currentAngle = 0f;    // 自己维护的角度
    private bool canRotate = false;     // ⭐是否允许旋转（核心）
    private bool isReturning = false; // 新增：是否正在自动回正

    void Update()
    {
        if (!canRotate && !isReturning)
            return;
        // 如果当前不允许旋转（例如玩家不在房间内），直接跳过所有逻辑

        if (Input.GetKey(KeyCode.E))
        {
            // 按住E：逆时针旋转
            currentAngle += rotateSpeed * Time.deltaTime;
            isReturning = false; // 手动旋转时停止回正
        
    }
        else
        {
            // 松手：自动回正
            if (Mathf.Abs(currentAngle) > snapThreshold)
            {
                currentAngle = Mathf.MoveTowards(currentAngle,0f, returnSpeed * Time.deltaTime);
                isReturning = true; // 标记正在回正
                //利用MoveTowards平滑回正

            }
            else
            {
                currentAngle = 0f;//吸附
                isReturning = false; // 回正完成
            }
        }

        transform.rotation = Quaternion.Euler(0f, 0f, currentAngle);//表示绕z轴旋转多少度
    }

    // 给 Trigger 调用
    public void SetCanRotate(bool value)
    {
        canRotate = value;

        // 离开房间时，强制开始回正
        if (!canRotate)
        {
            isReturning = true;
        }
    }
}
