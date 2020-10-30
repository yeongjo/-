using UnityEngine;
using System.Collections;

public class CameraContorller : MonoBehaviour {

    public Transform playerTrans;
    public float cameraMaxX = 100;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
        transform.position = Vector3.Lerp(transform.position, playerTrans.position + Vector3.back*10 + Vector3.up * 1.5f, Time.deltaTime*5);
        if(transform.position.x < 0)
        {
            transform.position = new Vector3(0, transform.position.y, -10);
        }
        else if(transform.position.x > cameraMaxX)
        {
            transform.position = new Vector3(cameraMaxX, transform.position.y, -10);
        }
	}
}
