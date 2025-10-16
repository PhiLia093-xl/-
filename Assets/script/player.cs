using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class PlayerMove : MonoBehaviour
{
    [SerializeField] private float speed = 5f;
    [SerializeField] private float dashSpeed = 20;
    [SerializeField] private float baseJumpForce = 5f;
    [SerializeField] private float maxJumpForce = 20f;
    [SerializeField] private float maxDashTime = 1;
    [SerializeField] private Rigidbody2D rb;
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

        if (Input.GetKeyDown(KeyCode.Space))
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
}