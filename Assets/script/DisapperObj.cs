using UnityEngine;
using System.Collections;

public class DisapperObj : MonoBehaviour
{

	public float disapperTime = 1.4f;
	float mTime = 0;
	public GameObject obj;
	// Update is called once per frame
	void Update ()
	{
		mTime += Time.deltaTime;
		if (mTime > disapperTime) 
		{
			this.gameObject.SetActive (false);

		}
	}
}

