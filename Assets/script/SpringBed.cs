using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpringBed : MonoBehaviour
{
    public float jumpForce = 20f;
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.name == "Player")
        {
            collision.gameObject.GetComponent<Rigidbody2D>().AddForce(new Vector2(0,jumpForce),ForceMode2D.Impulse);


        }
    }
}
