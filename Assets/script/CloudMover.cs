using UnityEngine;
using System.Collections;

public class CloudMover : MonoBehaviour
{
    public float speed = 1f;
    public float distance = .1f;
    Transform cameraTrans;
    Vector3 previousCameraPos;

    void Start()
    {
        cameraTrans = Camera.main.transform;
        transform.position = new Vector3(transform.position.x, transform.position.y, 1);
        previousCameraPos = cameraTrans.position;
    }
    // Update is called once per frame
    void Update()
    {
        transform.position += new Vector3(speed * Time.deltaTime, 0, 0) + (cameraTrans.position - previousCameraPos) * distance;
        previousCameraPos = cameraTrans.position;
    }
}
