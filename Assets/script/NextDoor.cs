using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System.Collections;

public class NextDoor : MonoBehaviour {

	AudioSource mAudio;
	Text mText;
	Camera mCamera;
	bool justOne;
	Image addtiveImage;
    float mtime;
    //AudioSource mMainAudiosource;

	void Start()
	{
		mCamera = Camera.main;
		mAudio = GetComponent<AudioSource> ();
		mText = GameObject.Find ("AudianText").GetComponent<Text> ();
		addtiveImage = mText.transform.parent.GetChild (3).GetComponent<Image> ();
        //GameObject Music;
        //if(Music = GameObject.Find("Music"))
        //    mMainAudiosource = Music.GetComponent<AudioSource>();
    }

    void Update()
	{
		if(mAudio.enabled)
		{
			Time.timeScale = .1f;
			mCamera.orthographicSize = Mathf.Lerp(mCamera.orthographicSize, 1, Time.deltaTime * 3);
			if(!justOne)
			{
				justOne = true;
				StartCoroutine (CameraShaker ());
			}
				
			// For PC
			#if UNITY_STANDALONE
			if (Input.GetKeyUp (KeyCode.Z)) 
			{
				Time.timeScale = 1;
                Application.LoadLevel (Application.loadedLevel + 1);
			}
			#endif

			// For Android
			#if UNITY_ANDROID
            for (int i = 0; i < Input.touchCount; i++)
            {
                if (Input.GetTouch(i).phase == TouchPhase.Stationary)
                {
                    Debug.Log("mtime : " + mtime);
                    mtime += Time.deltaTime;
                    if (mtime > .07f)
                    {
                        Time.timeScale = 1;
                        Application.LoadLevel(Application.loadedLevel + 1);
                    }
                }
                if (Input.GetTouch(i).phase == TouchPhase.Ended)
                {
                    Debug.Log("Ended : ");
                    mtime = 0;
                }
            }
			#endif
		}
	}

    void OnTriggerEnter2D(Collider2D other)
    {
        if(other.name == "Player")
        {
			mAudio.enabled = true;
			mText.text = "성공!!\n<size=19>다음 단계로 넘어가려면 꾹 누르세요.</size>";
        }
    }

	IEnumerator CameraShaker()
	{
		mCamera.GetComponent<CameraContorller> ().enabled = false;
		Vector3 first = mCamera.transform.position;
		CameraContorller cc = mCamera.GetComponent<CameraContorller> ();
		addtiveImage.enabled = true;
		while(true)
		{
            //if(mMainAudiosource)
            //    mMainAudiosource.volume -= .05f;
            addtiveImage.color = Color.Lerp (addtiveImage.color, new Color (.05f, .05f, .05f), Time.deltaTime * 20);
			first += (cc.playerTrans.position + new Vector3(0, 1, -10) - first) * Time.deltaTime * 10;
			mCamera.transform.position = first + (Vector3)Random.insideUnitCircle * .02f * mCamera.orthographicSize;
			yield return null;
			yield return null;
			yield return null;
			yield return null;
		}
	}
}
