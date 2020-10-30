using UnityEngine;
using System.Collections;

public class TitleChangeMyHealth : MonoBehaviour
{

    public Player mPlayer;
    public Boss mBoss;
    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.Alpha1))
        {
            PlayerPrefs.SetInt("Health", 1);
            if(mPlayer)
            mPlayer.Init();
            if (mBoss)
                mBoss.init();
        }
        else if (Input.GetKeyDown(KeyCode.Alpha2))
        {
            PlayerPrefs.SetInt("Health", 2);
            if (mPlayer)
                mPlayer.Init();
            if (mBoss)
                mBoss.init();
        }
        else if (Input.GetKeyDown(KeyCode.Alpha3))
        {
            PlayerPrefs.SetInt("Health", 3);
            if (mPlayer)
                mPlayer.Init();
            if (mBoss)
                mBoss.init();
        }
        else if (Input.GetKeyDown(KeyCode.Alpha4))
        {
            PlayerPrefs.SetInt("Health", 4);
            if (mPlayer)
                mPlayer.Init();
            if (mBoss)
                mBoss.init();
        }
    }
}
