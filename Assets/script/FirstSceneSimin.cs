using UnityEngine;
using System.Collections;

public class FirstSceneSimin : MonoBehaviour
{
    public GameObject too;
    public Transform mPlayerTrans;
    Animator mAnimator;

    void Update()
    {
        if (!mAnimator)
            mAnimator = GetComponent<Animator>();
        if(Vector2.SqrMagnitude(transform.position - mPlayerTrans.position) < 25)
        {
            if(!mAnimator.applyRootMotion && mAnimator.GetCurrentAnimatorStateInfo(0).IsName("simin_run"))
                mAnimator.applyRootMotion = true;
        }
        if(mAnimator.applyRootMotion)
        {
            transform.localScale = new Vector3(1, 1, 1);
            transform.Translate(4.2f * Time.deltaTime, 0, 0);
        }
    }

    void Delete()
    {
        too.SetActive(false);
    }
}
