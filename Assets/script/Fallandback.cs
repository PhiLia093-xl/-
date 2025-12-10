using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class Fallandback : MonoBehaviour
{
    public Transform PlayerNowPosition;
    public Transform Playerposition;
    
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

            Reback.instance.Spamwpoint.transform.position = Fallandback.instance.PlayerNowPosition.transform.position;

            Invoke("SetPlayerPosition", 1f);
        }
    }



    public void SetPlayerPosition()
    {
        Reback.instance.Pp.transform.position = new Vector3(Reback.instance.Spamwpoint.transform.position.x, Reback.instance.Spamwpoint.transform.position.y + 1f);

        Debug.Log("设置成功"+Reback.instance.Pp.transform.position + "目标位置为" + Fallandback.instance.PlayerNowPosition.transform.position);
    }























}
