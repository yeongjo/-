using UnityEngine;
using System.Collections;

public class PooperScriptReader : ScriptReader
{
    public GameObject poopParticle;
    public float speed = 2;
    public AudioSource mPoopAudio;
    public AudioClip[] mClip;
    public int appearCount = -1;
    bool bEnd = false;
    float delaytime01 = .05f, delaytime02 = .2f;
    public Transform mTrans;
    public AudioSource gMusic;

    public void Start()
    {
        base.Start();
        mTrans = transform;
        mTrans.position += Vector3.back * 5;
        GameObject music;
        if(music = GameObject.Find("Music"))
            gMusic = music.GetComponent<AudioSource>();
    }

    // Update is called once per frame
    public void Update()
    {
        if(poopParticle.activeSelf)
        {
            delaytime01 += Time.deltaTime;
            delaytime02 += Time.deltaTime;
            mTrans.position += new Vector3(0, speed * Time.deltaTime, 0);
            speed += .04f;
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
            if (transform.position.y > 15 && mUIText.text == "")
                gameObject.SetActive(false);
        }
        if (bEnd) return;
        if (readOneLineEnd)
        {
            if (Input.GetKeyDown(KeyCode.Z) || bIsAutoRead || readingTextNumLine == 0 || (Input.touchCount > 0 && Input.GetTouch(0).position.x > Screen.width * .5f && Input.GetTouch(0).phase == TouchPhase.Began))
            {
                if (readingTextNumLine >= mScripts.Length) //대사읽음이 끝남.
                {
                    mUIText.text = "";
                    readingTextNumLine = 0;
					mImage.enabled = false;
                    if (player) player.enabled = true;
                    StartCoroutine(ChangeVoulme(1));
                    poopParticle.SetActive(true);
                    bEnd = true;
                    return;
                }
                if(readingTextNumLine == 1)
                {
                    mPoopAudio.Play();
                    StartCoroutine(ChangeVoulme(0));
                    readDelay = .15f;
                    bIsAutoRead = true;
                }
                if(readingTextNumLine == 4)
                {
                    if (!mPoopAudio.isPlaying)
                    {
                        readDelay = .07f;
                        bIsAutoRead = true;
                        poopParticle.SetActive(true);
                    }
                    else
                    {
                        return;
                    }
                }
                //if (readingTextNumLine == appearCount)
                //{
                //    poopParticle.SetActive(true);
                //}
                StartCoroutine(ReadText(mScripts[readingTextNumLine])); //대사 한줄 읽음
            }
        }
        else
        {
            if (bIsAutoRead)
                return;
            if (Input.GetKeyDown(KeyCode.Z) || (Input.touchCount > 0 && Input.GetTouch(0).position.x > Screen.width * .5f && Input.GetTouch(0).phase == TouchPhase.Began))
            {
                StopAllCoroutines();
                mUIText.text = mScripts[readingTextNumLine];
                readingTextNumLine++;
                readOneLineEnd = true;
            }
        }
    }

    IEnumerator ChangeVoulme(float gotoVoulme)
    {
        if (!gMusic)
        {
            yield return null;
            yield break;
        }
        float deltaVol = (gotoVoulme - gMusic.volume) * .1f;
        for (int i=0;i<10;i++)
        {
            gMusic.volume += deltaVol;
            yield return new WaitForSeconds(.07f);
        }
    }
}
