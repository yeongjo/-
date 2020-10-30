using UnityEngine;
using System.Collections;

public class musicDelte : MonoBehaviour
{
    // Use this for initialization
    void Start()
    {
        Destroy(GameObject.Find("Music"));
    }
}
