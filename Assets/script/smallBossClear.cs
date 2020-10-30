using UnityEngine;
using System.Collections;

public class smallBossClear : MonoBehaviour
{
    public Animator mAnimator;
    LastCutScene mLastCut;
    public Player mPlayer;
    // Use this for initialization
    void Start()
    {
        StartCoroutine(DelayAnim());
        mLastCut=GetComponent<LastCutScene>();
        mPlayer.transform.position -= new Vector3(0, mPlayer.transform.position.y + 1.887f, 0);
		mPlayer.GetComponent<Rigidbody2D>().simulated = false;
        mPlayer.GetComponent<Collider2D>().enabled = false;
        mPlayer.transform.localScale = new Vector3(1, 1, 1);
        mPlayer.enabled = false;
    }

    IEnumerator DelayAnim()
    {
        yield return new WaitForSeconds(1.5f);
        mAnimator.SetTrigger("End");
        yield return new WaitForSeconds(.1f);
        mAnimator.SetTrigger("Next");
        mLastCut.enabled = true;
    }
}
