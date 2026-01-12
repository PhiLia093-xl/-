using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;
public class PlayerMove : MonoBehaviour
{
    //定义变量
    [SerializeField] private float speed = 1f;
    [SerializeField] private float dashSpeed = 5;
    [SerializeField] private float baseJumpForce = 2f;
    [SerializeField] private float maxJumpForce = 10f;
    [SerializeField] private float maxDashTime = 1;
    [SerializeField] private Rigidbody2D rb;
    [SerializeField] Transform groundDetector;
    [SerializeField] Vector2 groundDetectSize;
    private bool isKeyDown = false;
    private float jumpTimer = 0f;
    private bool isDashing = false;
    private float dashTimer = 0f;
    private Animator animator;
    private SpriteRenderer spriteRenderer;
    public AudioMixer WalkSound;
    private float volume;
    private void Start()
    {
        //获取组件
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        spriteRenderer = GetComponent<SpriteRenderer>();
    }
    void Update()
    {
        float h = Input.GetAxis("Horizontal");//获取a，d键并返回-1到1的值，控制方向
        rb.velocity = new Vector2(speed * h, rb.velocity.y);//；利用二维向量控制水平速度
        animator.SetFloat("Speed", Mathf.Abs(rb.velocity.x));//通过对水平速度取绝对值来实现用animator组件对静止，跑步动画进行控制的目的
     //修改spriteRenderer组件的flipx来实现动画转向
        if (rb.velocity.x > 0)
        {
            spriteRenderer.flipX = false;
        }
        else if (rb.velocity.x < 0)
        {
            spriteRenderer.flipX = true;
        }
       
        //判断冲刺的开始与结束
        if (isDashing)
        {
            rb.velocity = new Vector2(h * dashSpeed, rb.velocity.y);
            dashTimer += Time.deltaTime;
            if (dashTimer >= maxDashTime)
            {
                isDashing = false;
                animator.SetBool("IsDashing", false);
            }
        }
        //冲刺结束恢复原来的速度
        else
        {
            rb.velocity = new Vector2(h * speed, rb.velocity.y);
        }
        //冲刺的触发条件
        if (Input.GetKeyDown(KeyCode.LeftShift) && !isDashing && h != 0)
        {
            isDashing = true;
            dashTimer = 0f;
            animator.SetBool("IsDashing", true);

        }
       

        //地面检测
        Collider2D[] cols = Physics2D.OverlapBoxAll(groundDetector.position, groundDetectSize, 0, LayerMask.GetMask("Ground"));
        // 检测按下跳跃键（Space 或 W）
        if ((Input.GetKeyDown(KeyCode.Space) || Input.GetKeyDown(KeyCode.W)) && cols.Length > 0)
        {
            isKeyDown = true;
        }
        //蓄力跳（没有启用）
        if (isKeyDown)
        {
            jumpTimer += Time.deltaTime;
            if (Input.GetKeyUp(KeyCode.Space) || Input.GetKeyUp(KeyCode.W))
            {
                float jumpForce = baseJumpForce + jumpTimer * 3;
                jumpForce = jumpForce > maxJumpForce ? maxJumpForce : jumpForce;
                rb.AddForce(new Vector2(0, jumpForce * 100));
                jumpTimer = 0f;
                isKeyDown = false;
            }
        }
        //落下比跳起更容易
        if (rb.velocity.y < 0)
            rb.gravityScale = 2;
        else
            rb.gravityScale = 1;
        //走路音效
        WalkingAudio(h, volume, cols);
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.yellow;
        // 注意：size 参数是“总尺寸”，不是半长
        Gizmos.DrawWireCube(groundDetector.position, new Vector3(groundDetectSize.x * 2, groundDetectSize.y * 2, 0));
    }

    //走路音效
    public void WalkingAudio(float horizontal,float volume, Collider2D[] cols)
    {
        horizontal = Input.GetAxis("Horizontal");
        cols = Physics2D.OverlapBoxAll(groundDetector.position, groundDetectSize, 0, LayerMask.GetMask("Ground"));
        if (cols.Length > 0 && (horizontal > 0 || horizontal < 0))
        {
            WalkSound.SetFloat("WalkingSound", 0);
        }
        else
        {
            WalkSound.SetFloat("WalkingSound", -80);
        }
    }
    



}

