using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class PlayerMove : MonoBehaviour
{
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
    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        spriteRenderer = GetComponent<SpriteRenderer>();
    }
    void Update()
    {
        float horizontal = Input.GetAxis("Horizontal");
        rb.velocity = new Vector2(speed * horizontal, rb.velocity.y);
        animator.SetFloat("Speed", Mathf.Abs(rb.velocity.x));
        if (rb.velocity.x > 0)
        {
            spriteRenderer.flipX = false;
        }
        else if (rb.velocity.x < 0)
        {
            spriteRenderer.flipX = true;
        }
        float h = Input.GetAxis("Horizontal");
        if (isDashing)
        {
            rb.velocity = new Vector2(h * dashSpeed, rb.velocity.y);
            dashTimer += Time.deltaTime;
            if (dashTimer >= maxDashTime)
            {
                isDashing = false;
                dashTimer = 0f;
            }
        }
        else
        {
            rb.velocity = new Vector2(h * speed, rb.velocity.y);
        }
        if (Input.GetKeyDown(KeyCode.LeftShift) && !isDashing && h != 0)
            isDashing = true;
        Collider2D[] cols = Physics2D.OverlapBoxAll(groundDetector.position, groundDetectSize, 0, LayerMask.GetMask("Ground"));
        if (Input.GetKeyDown(KeyCode.Space) && cols.Length > 0)
        {
            isKeyDown = true;
        }

        if (isKeyDown)
        {
            jumpTimer += Time.deltaTime;
            if (Input.GetKeyUp(KeyCode.Space))
            {

                float jumpForce = baseJumpForce + jumpTimer * 3;
                jumpForce = jumpForce > maxJumpForce ? maxJumpForce : jumpForce;
                rb.AddForce(new Vector2(0, jumpForce * 100));
                jumpTimer = 0f;
                isKeyDown = false;
            }
        }
        if (rb.velocity.y < 0)
            rb.gravityScale = 2;
        else
            rb.gravityScale = 1;
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

