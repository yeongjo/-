using UnityEngine;
using System.Collections;

public class ThisIsSpartaReader : ScriptReader
{
	public Animator mSpartaAnimator;
    AudioSource mMainAudioSource;
    Coroutine A;
    bool keyInputIgnore;
    public void Start()
    {
        base.Start();
        GameObject A;
        if(A = GameObject.Find("Music"))
        {
            mMainAudioSource = A.GetComponent<AudioSource>();
            mMainAudioSource.volume = 0;
        }
    }

	public void Update () {
        if (keyInputIgnore) return;
		if(readOneLineEnd)
		{
			if (Input.GetKeyDown(KeyCode.Z) || bIsAutoRead || readingTextNumLine == 0 || (Input.touchCount > 0 && Input.GetTouch(0).position.x > Screen.width * .5f && Input.GetTouch(0).phase == TouchPhase.Began))
			{
				if (readingTextNumLine >= mScripts.Length) //대사읽음이 끝남.
				{
					mUIText.text = "";
                    keyInputIgnore = true;
                    this.enabled = false;
                    this.GetComponent<NPC_talk>().enabled = false;
                    this.GetComponent<SpriteRenderer>().enabled = false;
                    StartCoroutine(CheckAnimEnd(player));
                    readingTextNumLine = 0;
					mImage.enabled = false;
					return;
				}
                A = StartCoroutine(ReadText(mScripts[readingTextNumLine])); //대사 한줄 읽음
			}
		}
		else
		{
			if (bIsAutoRead)
				return;
			if(Input.GetKeyDown(KeyCode.Z) || (Input.touchCount > 0 && Input.GetTouch(0).position.x > Screen.width * .5f && Input.GetTouch(0).phase == TouchPhase.Began))
            {
                StopCoroutine(A);
				mUIText.text = mScripts[readingTextNumLine];
				readingTextNumLine++;
				readOneLineEnd = true;
			}
		}
	}

    IEnumerator CheckAnimEnd(Player mplayer)
    {
        Debug.Log("CheckAnimEnd");
        mSpartaAnimator.enabled = true;
        //yield return new WaitForSeconds(1);
        while (true)
        {
            //Debug.Log(" : " + mSpartaAnimator.GetCurrentAnimatorStateInfo(0).normalizedTime);
            if (mSpartaAnimator.GetCurrentAnimatorStateInfo(0).normalizedTime > 1f)
                break;
            yield return null;
        }
        mplayer.enabled = true;

        Debug.Log("end");
        mSpartaAnimator.enabled = false;
        StartCoroutine(SlowVolumeUp());
    }

    IEnumerator SlowVolumeUp()
    {
        for(int i = 0; i < 10; i++)
        {
            if(mMainAudioSource)
            mMainAudioSource.volume += .1f;
            yield return new WaitForSeconds(.2f);
        }
    }
}

