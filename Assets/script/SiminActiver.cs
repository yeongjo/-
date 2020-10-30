using UnityEngine;
using System.Collections;

public class SiminActiver : MonoBehaviour
{
    public float delayTime = 10;
    public float curruntTime = 0;
    public GameObject simin;

    // Update is called once per frame
    void Update()
    {
        curruntTime += Time.deltaTime;
        if (curruntTime >= delayTime)
        {
            simin.SetActive(true);
            this.enabled = false;
        }
    }
}
