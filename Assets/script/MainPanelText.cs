using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class MainPanelText : ScriptReader
{

    public void Start()
    {
        mUIText = GetComponent<Text>();
        mAudio = GetComponent<AudioSource>();
        mVoiceAudioClip = (AudioClip)Resources.Load("voice");
        mImage = transform.parent.GetComponent<Image>();
        mImage.enabled = true;
        StartCoroutine(AlphaBlendoff(.8f));
    }

    IEnumerator AlphaBlendoff(float alpha)
    {
        float delta = (alpha - mImage.color.a) * .05f;
        for (int i = 0; i < 20; i++)
        {
            mImage.color += new Color(0, 0, 0, delta);
            yield return new WaitForSeconds(.05f);
        }
        mImage.color = new Color(0, 0, 0, alpha);
    }

    // Update is called once per frame
    public void Update()
    {
        if (readOneLineEnd)
        {
            if (Input.GetKeyDown(KeyCode.Z) || bIsAutoRead || readingTextNumLine == 0)
            {
                if (readingTextNumLine >= mScripts.Length) //대사읽음이 끝남.
                {
                    mUIText.text = "";
                    this.enabled = false;
                    if (mImage)
                        StartCoroutine(AlphaBlendoff(0));
                    readingTextNumLine = 0;
                    return;
                }
                StartCoroutine(ReadText(mScripts[readingTextNumLine])); //대사 한줄 읽음
            }
        }
        else
        {
            if (bIsAutoRead)
                return;
            if (Input.GetKeyDown(KeyCode.Z)) //한줄 읽다가 다 읽어버리는놈
            {
                StopAllCoroutines();
                mUIText.text = mScripts[readingTextNumLine];
                readingTextNumLine++;
                readOneLineEnd = true;
            }
        }
    }
}
