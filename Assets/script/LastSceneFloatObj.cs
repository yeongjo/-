using UnityEngine;
using System.Collections;

public class LastSceneFloatObj : MonoBehaviour
{
    public float amout = 2;
    // Update is called once per frame
    void Update()
    {
        transform.position += new Vector3(0, Mathf.Sin(Time.time*3) * amout, 0);
    }
}
