using UnityEngine;
using System.Collections;

public class Title_getEnding : MonoBehaviour
{
    TextMesh mText;
    public int clearLevel = 0;
    // Use this for initialization
    void Start()
    {
        mText = GetComponent<TextMesh>();
        clearLevel = PlayerPrefs.GetInt("EndClear");
    }

    // Update is called once per frame
    void Update()
    {
        switch(clearLevel)
        {
            case 1:
                mText.text = "난이도 어떻게 깸을 어떻게 클리어 하셨습니다..! ㄷㄷ";
                break;
            case 2:
                mText.text = "난이도 아주 어려움을 클리어 하셨습니다!  축하드려요!";
                break;
            case 3:
                mText.text = "난이도 보통을 클리어 하셨습니다!  축하드려요!";
                break;
            case 4:
                mText.text = "난이도 쉬움을 클리어 하셨습니다!  축하드려요!";
                break;
        }
    }
}
