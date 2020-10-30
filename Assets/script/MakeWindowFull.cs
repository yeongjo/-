using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class MakeWindowFull : MonoBehaviour
{
    Text mtext;
    void Start()
    {
        mtext = GameObject.Find("MainText").GetComponent<Text>();
    }
    void OnTriggerEnter2D(Collider2D coll)
    {
        if (!Application.isMobilePlatform)
        {
            if (coll.name == "Player")
            {
                if (Screen.currentResolution.width * 10 / 16 < Screen.currentResolution.height)
                    Screen.SetResolution(Screen.currentResolution.width, Screen.currentResolution.width * 10 / 16, true);
                else
                    Screen.SetResolution(Screen.currentResolution.height * 16 / 10, Screen.currentResolution.height, true);
                mtext.text = "전체화면으로 변경되었습니다.";
            }
        }
        else
        {
            mtext.text = "모바일에선 지원하지않습니다.";
        }
    }
}
