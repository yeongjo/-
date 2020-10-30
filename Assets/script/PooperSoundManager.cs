using UnityEngine;
using System.Collections;

public class PooperSoundManager : MonoBehaviour
{

    float delaytime01, delaytime02;
    AudioSource mPoopAudio;
    public AudioClip[] mClip = new AudioClip[2];

    // Use this for initialization
    void Start()
    {
        mPoopAudio = GetComponent<AudioSource>();
        if( GetComponent<Animator>()) GetComponent<Animator>().enabled = true;
    }

    // Update is called once per frame
    void Update()
    {
        if (!mPoopAudio.enabled) return;
        delaytime01 += Time.deltaTime;
        delaytime02 += Time.deltaTime;
        if (delaytime01 > .4f)
        {
            mPoopAudio.PlayOneShot(mClip[0]);
            delaytime01 = 0;
        }
        if (delaytime02 > .1f)
        {
            mPoopAudio.PlayOneShot(mClip[1]);
            delaytime02 = 0;
        }
    }
}
