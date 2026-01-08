using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JellyfishClickAnimation : MonoBehaviour
{
    private Animator animator;

    void Start()
    {
        animator = GetComponent<Animator>();
    }

    void Update()
    {
        // 如果按下左键
        if (Input.GetMouseButtonDown(0))
        {
            Vector2 worldPoint =
                Camera.main.ScreenToWorldPoint(Input.mousePosition);

            // 发射 2D 射线检测是否点击到这个对象
            RaycastHit2D hit = Physics2D.Raycast(worldPoint, Vector2.zero);

            if (hit.collider != null &&
                hit.collider.gameObject == gameObject)
            {
                // 设置 Animator 参数触发播放被点击动画
                animator.SetTrigger("ClickedTrigger");
            }
        }
    }
}

