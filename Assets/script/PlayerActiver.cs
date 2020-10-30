using UnityEngine;
using System.Collections;

public class PlayerActiver : MonoBehaviour {

    public float delayTime = 12;
    public float curruntTime = 0;
    public Player player;
    Rigidbody2D playerrigid;

    void Start()
    {
        player.enabled = false;
        StartCoroutine(ASD());
        playerrigid = player.GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        curruntTime += Time.deltaTime;
        if (curruntTime >= delayTime)
        {
            player.enabled = true;
            this.enabled = false;
        }
    }

    IEnumerator ASD()
    {
        yield return new WaitForSeconds(11.5f);
        RaycastHit2D hit;
        hit = Physics2D.Raycast(player.transform.position, Vector2.down, .1f);
        if (hit)
        {
            playerrigid.AddForce(new Vector2(0, 450));
        }
        yield return new WaitForSeconds(.8f);
        hit = Physics2D.Raycast(player.transform.position, Vector2.down, .1f);
        if (hit)
        {
            playerrigid.AddForce(new Vector2(0, 450));
        }
    }
}
