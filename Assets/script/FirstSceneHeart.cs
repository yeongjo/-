using UnityEngine;
using System.Collections;

public class FirstSceneHeart : MonoBehaviour {

    public float delayTime = 8.5f;
    public float curruntTime = 0;
    public GameObject heart;
    public Transform mPlayer;
	
	// Update is called once per frame
	void Update () {
        curruntTime += Time.deltaTime;
        if (curruntTime >= delayTime)
        {
            Instantiate(heart, mPlayer.position + new Vector3(0.58f, 1, 0), Quaternion.identity);
            this.enabled = false;
        }
	}
}
