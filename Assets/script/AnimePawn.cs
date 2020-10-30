using UnityEngine;
using System.Collections;

public class AnimePawn : MonoBehaviour
{
    Animator mAnimator;
    // Use this for initialization
    void Start()
    {
        mAnimator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void Move(float deltaX)
    {
        if (deltaX != 0)
            mAnimator.SetBool("bRun", true);
        else
            mAnimator.SetBool("bRun", false);
        if (deltaX > 0)
            transform.localScale = Vector3.one;
        else if (deltaX < 0)
            transform.localScale = new Vector3(-1, 1, 1);
        transform.Translate(deltaX *Time.deltaTime * 3.6f, 0, 0);
    }
}
