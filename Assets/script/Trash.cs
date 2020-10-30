using UnityEngine;
using System.Collections;

public class Trash : MonoBehaviour {

    public SpriteRenderer sprite;
    public float speed = -6;
    [System.NonSerialized]
    public Transform cameraTrans;

    void Update()
    {
        transform.position += new Vector3(0, speed * Time.deltaTime, 0);
        if (transform.position.y < -10)
            transform.position = new Vector3(cameraTrans.position.x + Random.Range(-12, 12), cameraTrans.position.y + Random.Range(8,11), 0);
    }

    void OnTriggerEnter2D(Collider2D coll)
    {
        if(coll.name == "Player")
        {
            coll.GetComponent<Player>().Die();
            cameraTrans.GetComponent<CameraContorller>().enabled = false;
        }
        transform.position = new Vector3(cameraTrans.position.x + Random.Range(-12, 12), cameraTrans.position.y + Random.Range(8, 11), 0);
    }
}
