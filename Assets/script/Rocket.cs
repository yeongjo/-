using UnityEngine;
using System.Collections;

public class Rocket : MonoBehaviour
{
	public float speed = 10;
	float delaytime01, delaytime02;
	AudioSource mPoopAudio;
	public AudioClip[] mClip = new AudioClip[2];

	void Start()
	{
		mPoopAudio = GetComponent<AudioSource> ();
		Destroy (this.gameObject, 5);
	}
	// Update is called once per frame
	void Update ()
	{
		transform.Translate (0, speed * Time.deltaTime, 0);
		delaytime01 += Time.deltaTime;
		delaytime02 += Time.deltaTime;
		if (delaytime01 > .1f)
		{
			mPoopAudio.PlayOneShot(mClip[0]);
			delaytime01 = 0;
		}
		if (delaytime02 > .4f)
		{
			mPoopAudio.PlayOneShot(mClip[1]);
			delaytime02 = 0;
		}
	}
}

