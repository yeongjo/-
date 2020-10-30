using UnityEngine;
using System.Collections;

public class FirstScriptReader : ScriptReader
{
    public float time;
    public Animator mAnim;
    public GameObject mObj;
    // Use this for initialization
    void Start()
    {
        base.Start();
        readOneLineEnd = true;
        StartCoroutine(ReadText(mScripts[readingTextNumLine]));
    }

    // Update is called once per frame
    void Update()
    {
		// if press button Z over 1.5sec
        if(Input.GetKey(KeyCode.Z) || (Input.touchCount>0 && Input.GetTouch(0).position.x > Screen.width * .5f))
        {
            time += Time.deltaTime;
        }
        else if (Input.GetKeyUp(KeyCode.Z) || (Input.touchCount > 0 && Input.GetTouch(0).position.x > Screen.width * .5f && Input.GetTouch(0).phase == TouchPhase.Ended))
        {
            time = 0;
        }
        if(time > 1.5f)
        {
            mAnim.gameObject.SetActive(true);
            mObj.SetActive(false);
            mAnim.Play("simin_run", 0);
            StopAllCoroutines();
            mUIText.text = mScripts[readingTextNumLine];
            readingTextNumLine = mScripts.Length;
            readOneLineEnd = true;
        }
        if (readOneLineEnd || time>1.5f)
        {
            if (Input.GetKeyDown(KeyCode.Z) || bIsAutoRead || time > 1.5f || (Input.touchCount > 0 && Input.GetTouch(0).position.x > Screen.width * .5f && Input.GetTouch(0).phase == TouchPhase.Began))
            {
                if (readingTextNumLine >= mScripts.Length || time > 1.5f) //대사읽음이 끝남.
                {
                    mUIText.text = mScripts[mScripts.Length - 1];
                    this.enabled = false;
                    readingTextNumLine = 0;
                    if (player)
                        player.enabled = true;
                    return;
                }
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
}
