using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Platforms : MonoBehaviour
{
    [SerializeField] private GameObject[] points;
    [SerializeField] private float speed = 2f;

    private int pointNum = 1;
   
    void Update()
    {
        transform.position = Vector2.MoveTowards(transform.position, points[pointNum].transform.position,speed*Time.deltaTime);
    }
}
