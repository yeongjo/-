using UnityEngine;
using System.Collections;

public class RuningSimin : MonoBehaviour {

    public Transform mPlayerTrans;
    Animator mAnimator;

    void Start()
    {
        mAnimator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update () {
        if (Vector2.SqrMagnitude(transform.position - mPlayerTrans.position) < 50)
        {
            if (!mAnimator.applyRootMotion && mAnimator.GetCurrentAnimatorStateInfo(0).IsName("simin_run"))
                mAnimator.applyRootMotion = true;
        }
        if (mAnimator.applyRootMotion)
        {
            transform.localScale = new Vector3(1, 1, 1);
            transform.Translate(3f * Time.deltaTime, 0, 0);
        }
    }
}
