using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Platforms : MonoBehaviour
{
    [SerializeField] private GameObject[] points;
    [SerializeField] private float speed = 2f;

    private int pointNum = 1;
    private float waitTime = 2.0f;

    void Update()
    {
        transform.position = Vector2.MoveTowards(transform.position, points[pointNum].transform.position, speed * Time.deltaTime);
        if (Vector2.Distance(transform.position, points[pointNum].transform.position) < 0.1f)
        {

            if (waitTime < 0)
            {
                if (pointNum == 0)
                {
                    pointNum = 1;
                }

                else
                {
                    pointNum = 0;
                }
                waitTime = 2.0f;
            }
            else
                waitTime-= Time.deltaTime;
        }
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if( collision.gameObject.name == "Player")
        {
            collision.gameObject.transform.SetParent(transform);
        }
    }
    private void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.gameObject.name == "Player")
        {
            collision.gameObject.transform.SetParent(null);
        }
    }
}
