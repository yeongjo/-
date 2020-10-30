using UnityEngine;
using System.Collections;

public class NPC_talk : MonoBehaviour
{
    public ScriptReader mScriptReader;
    public Player player;

    void Update()
    {
        if (Application.isMobilePlatform)
        {
            for (int j = 0; j < Input.touchCount; j++)
            {
                if (Input.GetTouch(j).position.x > Screen.width * .5f && Input.GetTouch(j).phase == TouchPhase.Began)
                {
                    if (Vector3.SqrMagnitude(transform.position - player.transform.position) < 4)
                    {
                        //Debug.Log("sjflds");
                        mScriptReader.enabled = true;
                        player.Enable();
                    }
                }
            }
        }
        else
        {
            if(Input.GetKeyDown(KeyCode.Z))
            {
                if (Vector3.SqrMagnitude(transform.position - player.transform.position) < 4)
                {
                    //Debug.Log("sjflds");
                    mScriptReader.enabled = true;
                    player.Enable();
                }
            }
        }
    }
}
