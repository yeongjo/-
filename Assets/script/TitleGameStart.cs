using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class TitleGameStart : MonoBehaviour
{
    public Image fadeOutImage;
    public Dropdown mDrop;
    public ScriptReader mS;
    int loadedLevel;

    void OnTriggerEnter2D(Collider2D coll)
    {
        if(coll.name == "Player")
        {
            coll.gameObject.SetActive(false);
            StartCoroutine(GotoStart());
        }
    }

    IEnumerator GotoStart()
    {
        GameObject.Find("MainText").GetComponent<Text>().enabled = false;
        mDrop.gameObject.SetActive(false);
        float delta = 0f;
        while (delta < 1)
        {
            fadeOutImage.color = new Color(0, 0, 0, delta);
            delta += .02f;
            yield return new WaitForSeconds(.02f);
        }
        yield return new WaitForSeconds(1);
        mS.enabled = true;

        switch(PlayerPrefs.GetInt("Health"))
        {
            case 1:
                mS.mScripts[0] = "난이도  어떻게 깸";
                break;
            case 2:
                mS.mScripts[0] = "난이도  개어려움";
                break;
            case 3:
                mS.mScripts[0] = "난이도  보통";
                break;
            case 4:
                mS.mScripts[0] = "난이도  쉬움";
                break;
        }
        mS.mScripts[1] = "그렇게 지영의 험난한 여정이 시작되는데..";
        yield return new WaitForSeconds(5);
        loadedLevel = PlayerPrefs.GetInt("level");
        Application.LoadLevel((loadedLevel > 0)? loadedLevel : 1);
    }
}
