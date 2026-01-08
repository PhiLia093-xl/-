using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;//重新加载场景

public class PlayerLife : MonoBehaviour
{
    private Animator anim;
    private Rigidbody2D rb;
    void Start()
    {
        anim = GetComponent<Animator>();
        rb = GetComponent<Rigidbody2D>();
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Trap") 
        {
            Death();
        }
    }
    public Transform respawnPoint;  // 复活点

private void Death()
{
    rb.bodyType = RigidbodyType2D.Static;
    anim.SetTrigger("death");
    Invoke(nameof(Respawn), 1f);  // 等待动画结束
}

private void Respawn()
{
    rb.bodyType = RigidbodyType2D.Dynamic;

    // 移动到复活点
    transform.position = respawnPoint.position;

    // 重置速度
    rb.velocity = Vector2.zero;

    // 可以重置动画状态
    anim.ResetTrigger("death");
    anim.Play("Idle"); // 或者其它默认动画
}

}
