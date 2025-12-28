using System.Collections;
using System.Collections.Generic;
using Unity.Collections;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    [SerializeField] Transform groundDetector;
    [SerializeField] Vector2 groundDetectSize;
    private bool JumpJudge = false;
    public float ShakeTime;
    public float ShakeMagnitude;
    public CameraShakeManager cameraShake;
    private bool UpJudge = false;
    private float WaitTime = 0;

    // Update is called once per frame
    void Update()
    {
        Collider2D[] cols = Physics2D.OverlapBoxAll(groundDetector.position, groundDetectSize,0,LayerMask.GetMask("Ground"));
        if (Input.GetKeyDown(KeyCode.Space) || cols.Length <= 0)
        {
            JumpJudge = true;
        }

        if (JumpJudge && (Input.GetKeyUp(KeyCode.Space) || cols.Length <= 0))
        {
            UpJudge = true;
            WaitTime = ShakeTime - 0.1f;
        }

        if (UpJudge)
        {
            WaitTime += Time.deltaTime;
        }

        if (UpJudge && cols.Length > 0 && WaitTime > ShakeTime)
        {
            StartCoroutine(cameraShake.CameraShake(ShakeTime, ShakeMagnitude));
            JumpJudge = false;
            UpJudge = false;
            WaitTime = 0;
        }
    }
    private void OnDrawGizmos()
    {
        Gizmos.color = Color.yellow;
        Gizmos.DrawLine(groundDetector.position + new Vector3(groundDetectSize.x, groundDetectSize.y), groundDetector.position + new Vector3(-groundDetectSize.x, groundDetectSize.y));
        Gizmos.DrawLine(groundDetector.position + new Vector3(groundDetectSize.x, -groundDetectSize.y), groundDetector.position + new Vector3(-groundDetectSize.x, -groundDetectSize.y));
        Gizmos.DrawLine(groundDetector.position + new Vector3(groundDetectSize.x, groundDetectSize.y), groundDetector.position + new Vector3(groundDetectSize.x, -groundDetectSize.y));
        Gizmos.DrawLine(groundDetector.position + new Vector3(-groundDetectSize.x, groundDetectSize.y), groundDetector.position + new Vector3(-groundDetectSize.x, -groundDetectSize.y));
    }
}
