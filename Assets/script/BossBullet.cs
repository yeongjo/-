using UnityEngine;
using System.Collections;

public class BossBullet : bullet
{
    public bool bCanHit = true;
    public Transform PlayerTrans;
    float time;
    // Use this for initialization
    void Start()
    {
        transform.position += Vector3.back * 2;
        Destroy(this.gameObject, 10);
    }

    // Update is called once per frame
    void Update()
    {
        time += Time.deltaTime;
        if (time > .3f)
            GetComponent<Collider2D>().enabled = true;
        transform.Translate(0, -speed * Time.deltaTime, 0);
        if (time > 3) return;
        float deg = Mathf.LerpAngle(transform.eulerAngles.z, Mathf.Atan2(PlayerTrans.position.y - transform.position.y, PlayerTrans.position.x - transform.position.x) * Mathf.Rad2Deg + 90, Time.deltaTime * .9f);
        transform.eulerAngles = new Vector3(0, 0, deg);
    }

    void OnCollisionEnter2D(Collision2D coll)
    {
        if(coll.collider.name == "Player" && bCanHit)
        {

            coll.collider.GetComponent<Player>().TakeDamage();
        }
           DestroyObject(this.gameObject);
    }
}
