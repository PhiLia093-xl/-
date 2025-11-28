using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fallandback : MonoBehaviour
{
    public Transform PlayerNowPosition;
    public Transform Playerposition;
    public Transform Spamwpoint;
    public GameObject Pp;
    public static Fallandback instance { get; private set; }
    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
    }
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            Debug.Log("赋值");

            Spamwpoint.transform.position = Fallandback.instance.PlayerNowPosition.transform.position;

            Invoke("SetPlayerPosition", 1f);
        }
    }



public void SetPlayerPosition()
    {
        Pp.transform.position = new Vector3(Spamwpoint.transform.position.x, Spamwpoint.transform.position.y + 1f);

Debug.Log("设置成功"+Pp.transform.position + "目标位置为" + Fallandback.instance.PlayerNowPosition.transform.position);
}























}
