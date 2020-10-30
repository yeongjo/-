using UnityEngine;
using System.Collections;

public class bullet : MonoBehaviour
{
    public float speed = 10;
    public int damage = 1;
    public Sprite[] sprites;
    int drection;
    bool alreadyHit;

    void Start()
    {
        transform.localScale = transform.localScale * damage*.2f + transform.localScale*.5f;
        drection = (transform.localScale.x > 0) ? 1 : -1;
        switch (damage)
        {
            case 1:
            case 2:
                GetComponent<SpriteRenderer>().sprite = sprites[0];
                break;
            case 3:
            case 4:
                GetComponent<SpriteRenderer>().sprite = sprites[1];
                break;
            case 5:
            case 6:
                GetComponent<SpriteRenderer>().sprite = sprites[2];
                break;
            default:
                GetComponent<SpriteRenderer>().sprite = sprites[2];
                break;
        }
        GetComponent<SpriteRenderer>().sprite = sprites[0];
    }
    // Update is called once per frame
    void Update()
    {
        RaycastHit2D hit = Physics2D.Raycast(transform.position, transform.right, .6f, (-1)-(1<<9));
        if (!alreadyHit && hit)
        {
            if (hit.collider.name != "Player")
            {
                if (hit.collider.name == "보스")
                    hit.collider.GetComponent<Boss>().TakeDamage(damage);
                if(hit.collider.name == "미니보스")
                    hit.collider.GetComponent<SmallBoss>().TakeDamage(damage);
                DestroyObject(this.gameObject, .2f);
                alreadyHit = true;
            }
        }
        transform.Translate(drection*speed * Time.deltaTime, 0, 0);
    }
}
