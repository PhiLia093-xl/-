using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Reback : MonoBehaviour
{
    
    public Transform Spamwpoint;
    public GameObject Pp;
    public static Reback instance { get; private set; }

    public void Awake()
    {
        if (instance = null)
        {
            instance = this;
        }
    }
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            
            Spamwpoint.transform.position = Fallandback.instance.PlayerNowPosition.transform.position;

            Invoke("SetPlayerPosition", 1f);
        }
    }
    public void SetPlayerPosition()
    {
        Pp.transform.position = new Vector3(Spamwpoint.transform.position.x, Spamwpoint.transform.position.y + 1f);

        
    }

}
