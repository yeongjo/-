using UnityEngine;
using System.Collections;

public class NPC_ScriptReaderMagic : ScriptReader
{
    public GameObject magicObject;
    public int appearCount = -1;
	public NPC_talk npc_Talk;
	public bool disappear;
    // Update is called once per frame
    public void Update()
    {
        if (readOneLineEnd)
        {
            if (Input.GetKeyDown(KeyCode.Z) || bIsAutoRead || readingTextNumLine == 0 || (Input.touchCount > 0 && Input.GetTouch(0).position.x > Screen.width * .5f && Input.GetTouch(0).phase == TouchPhase.Began))
            {
                if (readingTextNumLine >= mScripts.Length) //대사읽음이 끝남.
                {
                    mUIText.text = "";
                    readingTextNumLine = 0;
                    if (player) 
						player.enabled = true;
                    magicObject.SetActive(true);
                    this.enabled = false;
					mImage.enabled = false;
					if(npc_Talk)
						npc_Talk.enabled = false;
					if(disappear)
						gameObject.SetActive (false);
					return;
                }
                if (readingTextNumLine == appearCount)
                    magicObject.SetActive(true);
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
