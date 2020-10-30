using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class ScriptReader : MonoBehaviour
{

    public bool bIsAutoRead = false;
    [System.NonSerialized]
    public Text mUIText;
    [System.NonSerialized]
    public Image mImage;
    [System.NonSerialized]
    public int readingTextNumLine = 0;
    [System.NonSerialized]
    public bool readOneLineEnd = true;
    public string[] mScripts;
    [System.NonSerialized]
    public float readDelay = .07f;
    public Player player;
    [System.NonSerialized]
    public AudioSource mAudio;
    [System.NonSerialized]
    public AudioClip mVoiceAudioClip;

    // Use this for initialization
    public void Start()
    {
        mUIText = GameObject.Find("ScriptText").GetComponent<Text>();
        mAudio = GetComponent<AudioSource>();
        mVoiceAudioClip = (AudioClip)Resources.Load("voice");
        mImage = mUIText.transform.parent.GetComponent<Image>();
    }

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
                    this.enabled = false;
                    if (mImage)
                        mImage.enabled = false;
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
            if (Input.GetKeyDown(KeyCode.Z) || (Input.touchCount > 0 && Input.GetTouch(0).position.x > Screen.width * .5f && Input.GetTouch(0).phase == TouchPhase.Began)) //한줄 읽다가 다 읽어버리는놈
            {
                StopAllCoroutines();
                mUIText.text = mScripts[readingTextNumLine];
                readingTextNumLine++;
                readOneLineEnd = true;
            }
        }
    }

    public IEnumerator ReadText(string readtext)
    {
        if (mImage)
            mImage.enabled = true;
        readOneLineEnd = false;
        for (int i = 0; i < readtext.Length; i++)
        {
            mUIText.text = readtext.Substring(0, i + 1);
            mAudio.PlayOneShot(mVoiceAudioClip);
            yield return new WaitForSeconds(readDelay);
        }
        if (bIsAutoRead)
            yield return new WaitForSeconds(1.3f);
        readingTextNumLine++;
        readOneLineEnd = true;
    }
}
