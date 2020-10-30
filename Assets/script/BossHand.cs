using UnityEngine;
using System.Collections;

public class BossHand : MonoBehaviour
{

    void OnTriggerEnter2D(Collider2D coll)
    {
        if(coll.name == "Player")
        {
            coll.GetComponent<Player>().TakeDamage();
        }
    }
}
