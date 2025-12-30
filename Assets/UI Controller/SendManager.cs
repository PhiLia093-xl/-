using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SendManager : MonoBehaviour
{
    public Transform backDoor;

    private bool isDoor;
    private Transform playerTransform;

    void Start()
    {
        playerTransform = GameObject.FindGameObjectWithTag("Player").GetComponent<Transform>();
    }

    void Update()
    {
        if (isDoor && Input.GetKeyDown(KeyCode.S))
        {
            playerTransform.position = backDoor.position;
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player")) // 只对玩家生效
        {
            Debug.Log("Player进入门的范围");
            isDoor = true;
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Player")) // 玩家离开范围才设 false
        {
            Debug.Log("Player离开门的范围");
            isDoor = false;
        }
    }
}
