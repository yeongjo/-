using UnityEngine;
using System.Collections;

public class CloudCreator : MonoBehaviour {

    GameObject[] cloudObjects;
    public int distance = 40;
    int cloudCount = 0;
    public Sprite[] cloudSprite;
    float cloudSpeed = -0.25f;
    float distanceByBackgound = 0.5f;

	// Use this for initialization
	void Start () {
        cloudCount = (int)(distance * .3f);
        cloudObjects = new GameObject[cloudCount];
        for (int i = 0; i < cloudCount; i++)
        {
            cloudObjects[i] = new GameObject();
            cloudObjects[i].name = "구름_" + i;
            SpriteRenderer b =  cloudObjects[i].AddComponent<SpriteRenderer>();
            b.sprite = cloudSprite[Random.Range(0, cloudSprite.Length)];
            b.sortingOrder = -10;
            CloudMover a = cloudObjects[i].AddComponent<CloudMover>();
            a.speed = -.25f;
            a.distance = .5f;
            cloudObjects[i].transform.position = new Vector3(distance / cloudCount * i - 7 + Random.Range(0, 7f), Random.Range(0, 8f));
        }
	}
	
	// Update is called once per frame
	void Update () {
        if (cloudObjects[0].transform.position.x < -8.5f)
        {
            cloudObjects[0].transform.position = new Vector3(distance, 0, 0);
        }
    }
}
