using UnityEngine;
using System.Collections;

public class MusicDestoryer : MonoBehaviour
{

    // Use this for initialization
    void Start()
    {
        GameObject A;
        if (A = GameObject.Find("Music"))
        {
            Destroy(A);
        }
    }
}
