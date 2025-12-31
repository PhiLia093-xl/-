using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HuangJinYiAppear : MonoBehaviour
{
    private float WaitTime = 0;
    public float SetTime;
    private Transform playerTransform;
    public GameObject HuangJinYi;
    private bool Judge = false;

    // Start is called before the first frame update
    void Start()
    {
        playerTransform = GameObject.FindGameObjectWithTag("Player").GetComponent<Transform>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Judge)
        {
            WaitTime += Time.deltaTime;
        }
        if(WaitTime > SetTime)
        {
            HuangJinYi.SetActive(true);
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player")) // 只对玩家生效
        {
            Judge = true;
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Player")) // 玩家离开范围才设 false
        {
            Judge = false;

        }
    }
}
