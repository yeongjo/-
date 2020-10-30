using UnityEngine;
using System.Collections;

public class LastCutScene : MonoBehaviour
{
	public Animator mAnimator;
	public int animationCount = 0;
	public ScriptReader[] mScriptReaders;
	public int readingScriptNum = 0;
	bool bIsReadingScript;
    Player mPlayer;
    public bool bCameraCanMove;
    AudioSource mMusic;
    public bool bDestoryMusic;
    public bool miniBossBefore;
    // Use this for initialization
    void Start()
    {
		//mAnimator = GetComponent<Animator> ();
		mAnimator.enabled = true;
        if (!bCameraCanMove)
            Camera.main.GetComponent<CameraContorller>().enabled = false;
        GameObject A;
        if ((A = GameObject.Find("Music")))
        {
            if (bDestoryMusic)
                Destroy(A);
            else
            {
                mMusic = A.GetComponent<AudioSource>();
                mMusic.volume = 0;
            }
        }
        else if ((A = GameObject.Find("NeMusic")) && !bDestoryMusic)
        {
            mMusic = A.GetComponent<AudioSource>();
            mMusic.volume = 0;
        }
        if (mPlayer = GameObject.Find("Player").GetComponent<Player>())
        {
            mPlayer.Enable();
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (!bIsReadingScript && mAnimator.GetCurrentAnimatorStateInfo(0).normalizedTime >= 1)
        {
            if (animationCount <= readingScriptNum)
            {
                Debug.Log("End");
                mPlayer.bCanDoubleJump = true;
                mPlayer.enabled = true;
                mAnimator.enabled = false;
                if (mMusic)
                    mMusic.volume = 1;
                if (mScriptReaders.Length > 2)
                    mScriptReaders[2].enabled = true;

                this.enabled = false;
                return;
            }
            mScriptReaders[readingScriptNum].enabled = true;
            bIsReadingScript = true;
        }
        if (!mScriptReaders[readingScriptNum].enabled && bIsReadingScript && (miniBossBefore || mAnimator.GetCurrentAnimatorStateInfo(0).normalizedTime >= 1))
        {
            bIsReadingScript = false;
            readingScriptNum++;
            mAnimator.SetTrigger("Next");
        }
    }
}
