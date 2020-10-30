using UnityEngine;
using System.Collections;

public class Esc : MonoBehaviour
{
    bool activeAll = false;
    public GameObject allObjs;

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.Escape))
        {
            activeAll = !activeAll;
        }
        if (activeAll)
        {
            allObjs.SetActive(true);
            if (Input.GetKeyDown(KeyCode.Q))
            {
                GotoMain();
            }
            if (Input.GetKeyDown(KeyCode.P))
            {
                End();
            }
            Time.timeScale = 0;
        }
        else
        {
            Time.timeScale = 1;
            allObjs.SetActive(false);
        }
    }

    public void GotoMain()
    {
        Time.timeScale = 1;
        PlayerPrefs.SetInt("level", Application.loadedLevel);
        Application.LoadLevel(0);
    }

    public void End()
    {
        Time.timeScale = 1;
        Application.Quit();
    }
}
