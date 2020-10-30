using UnityEngine;
using System.Collections;

public class TitleSetting : MonoBehaviour
{
    bool bDown;
    Transform cameraTrans;
    void Start()
    {
        //Screen.SetResolution(900, 562, false);
        cameraTrans = Camera.main.transform;
    }

    void OnTriggerEnter2D(Collider2D coll)
    {
        if(coll.name == "Player")
            bDown = true;
    }

    void Update()
    {
        if(bDown)
        {
            cameraTrans.position = Vector3.Lerp(cameraTrans.position, new Vector3(0, -9.53f, -10), Time.deltaTime * 5f);
        }
    }
}
