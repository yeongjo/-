using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class DeadScriptReader : ScriptReader
{	
    void Start()
    {
        mUIText = GetComponent<Text>();
        mAudio = GetComponent<AudioSource>();
        mVoiceAudioClip = (AudioClip)Resources.Load("voice");
        mImage = mUIText.transform.parent.GetComponent<Image>();
    }
	public void Update () {
		if(readOneLineEnd)
		{
			if (Input.GetKeyDown(KeyCode.Z) || bIsAutoRead || readingTextNumLine == 0)
			{
				if (readingTextNumLine >= mScripts.Length) //대사읽음이 끝남.
				{
					this.enabled = false;
					readingTextNumLine = 0;
					if (player) player.enabled = true;
					return;
				}
				StartCoroutine(ReadText(mScripts[readingTextNumLine])); //대사 한줄 읽음
			}
		}
		else
		{
			if (bIsAutoRead)
				return;
			if(Input.GetKeyDown(KeyCode.Z))
			{
				StopAllCoroutines();
				mUIText.text = mScripts[readingTextNumLine];
				readingTextNumLine++;
				readOneLineEnd = true;
			}
		}
	}
}

