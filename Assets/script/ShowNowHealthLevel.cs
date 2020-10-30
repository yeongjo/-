using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class ShowNowHealthLevel : MonoBehaviour
{
    public SpriteRenderer[] mSprite;
    public int myLevel;
    void Start()
    {
        mSprite = GetComponentsInChildren<SpriteRenderer>(false);
        if (PlayerPrefs.GetInt("EndClear1") != myLevel && PlayerPrefs.GetInt("EndClear2") != myLevel && PlayerPrefs.GetInt("EndClear3") != myLevel && PlayerPrefs.GetInt("EndClear4") != myLevel)
        {
            mSprite[1].color = Color.black;
            mSprite[2].color = Color.black;
            mSprite[3].color = Color.black;
        }
    }
}
