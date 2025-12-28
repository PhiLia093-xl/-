using UnityEngine;

public class JellyfishSwitch : MonoBehaviour
{
    public bool startRed = false;  // ✅ 新增：设置初始状态

    private Animator animator;
    private Collider2D col;

    private bool isRed;

    void Awake()
    {
        animator = GetComponent<Animator>();
        col = GetComponent<Collider2D>();

        // 根据 startRed 决定初始状态
        if (startRed)
            SetRed();
        else
            SetPurple();
    }

    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            Vector2 pos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            RaycastHit2D hit = Physics2D.Raycast(pos, Vector2.zero);

            if (hit.collider != null && hit.collider.gameObject == gameObject)
            {
                Toggle();
            }
        }
    }

    void Toggle()
    {
        isRed = !isRed;
        animator.SetBool("isRed", isRed);
        col.isTrigger = !isRed;  // 红色：非触发 = 有碰撞，紫色：触发 = 无碰撞
    }

    void SetPurple()
    {
        isRed = false;
        animator.SetBool("isRed", false);
        col.isTrigger = true;
    }

    void SetRed()
    {
        isRed = true;
        animator.SetBool("isRed", true);
        col.isTrigger = false;
    }
}
