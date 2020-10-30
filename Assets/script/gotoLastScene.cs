using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class gotoLastScene : ScriptReader
{

    public Image fadeoutImage;
    AudioSource mAudio;
    public AudioClip mSound;
    void Start()
    {
        GameObject A;
        if(A = GameObject.Find("Music"))
            mAudio = A.GetComponent<AudioSource>();
        else if (A = GameObject.Find("NeMusic"))
            mAudio = A.GetComponent<AudioSource>();
        StartCoroutine(Delay());
    }

    void Update()
    {

    }

    IEnumerator Delay()
    {
        AudioSource myAudio = GetComponent<AudioSource>();
        myAudio.pitch = 1;
        myAudio.PlayOneShot(mSound);
        Transform cameraTrans = Camera.main.transform;
        cameraTrans.GetComponent<CameraContorller>().enabled = false;
        Vector3 firstPos = cameraTrans.position;
        float value = .01f;
        while (fadeoutImage.color.a < .978f)
        {
            float a = fadeoutImage.color.r;
            fadeoutImage.color = new Color(a, a, a, Mathf.Lerp(fadeoutImage.color.a, 1, 1.4f*Time.deltaTime));
            cameraTrans.position = firstPos + Random.insideUnitSphere * value;
            value += Time.deltaTime;
            if(mAudio)
                mAudio.volume -= Time.deltaTime;
            if (value > 1) value = 1;
            yield return null;
            yield return null;
            yield return null;
        }
        fadeoutImage.color = Color.black;
        yield return new WaitForSeconds(1);
        Application.LoadLevel(Application.loadedLevel + 1);
    }
}
