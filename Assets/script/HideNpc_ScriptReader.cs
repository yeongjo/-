using UnityEngine;
using System.Collections;

public class HideNpc_ScriptReader : ScriptReader
{
	public NPC_talk npc_Talk;
	Camera mCamera;

	public void Start()
	{
		base.Start ();
		mCamera = Camera.main;
	}


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
                    GetComponent<Animator>().Play("hide");
                    readingTextNumLine = 0;
					mImage.enabled = false;
					StartCoroutine (ZoomInZoomOut ());
					npc_Talk.enabled = false;
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
            if (Input.GetKeyDown(KeyCode.Z))
            {
                StopAllCoroutines();
                mUIText.text = mScripts[readingTextNumLine];
                readingTextNumLine++;
                readOneLineEnd = true;
            }
        }
    }

	IEnumerator ZoomInZoomOut()
	{
		Vector3 mpos = transform.position + Vector3.back*10;
		for (int i = 0; i < 50; i++) {
			mCamera.orthographicSize = Mathf.Lerp (mCamera.orthographicSize, 1.5f, Time.deltaTime);
			mCamera.transform.position = mpos;
			mCamera.GetComponent<CameraContorller> ().enabled = false;
			yield return new WaitForSeconds (.02f);
		}
		yield return new WaitForSeconds (3);
		mCamera.GetComponent<CameraContorller> ().enabled = true;
		for (int i = 0; i < 50; i++) {
			mCamera.orthographicSize = Mathf.Lerp (mCamera.orthographicSize, 5f, Time.deltaTime);
			yield return new WaitForSeconds (.005f);
		}
	}
}
