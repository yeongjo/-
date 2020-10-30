using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class EscShowNewLevel : MonoBehaviour
{
    public Text mtext;

    // Update is called once per frame
    void Update()
    {
        switch(PlayerPrefs.GetInt("Health"))
        {
            case 1:
                mtext.text = "난이도 어떻게 깸";
                break;
            case 2:
                mtext.text = "난이도 개어려움";
                break;
            case 3:
                mtext.text = "난이도 보통";
                break;
            case 4:
                mtext.text = "난이도 쉬움";
                break;
        }

    }
}
