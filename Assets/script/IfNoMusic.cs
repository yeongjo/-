using UnityEngine;
using System.Collections;

public class IfNoMusic : MonoBehaviour
{
    AudioSource mAudioSource;
    // Use this for initialization
    void Start()
    {
        GameObject A = GameObject.Find("Music");
        if(A == null)
        {
            mAudioSource = GetComponent<AudioSource>();
            mAudioSource.enabled = true;
            DontDestroyOnLoad(mAudioSource);
            mAudioSource.gameObject.name = "Music";
        }
    }
}
