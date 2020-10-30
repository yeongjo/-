using UnityEngine;
using System.Collections;

public class LevelSaver : MonoBehaviour
{

    // Use this for initialization
    void Start()
    {
        Debug.Log("LevelSaverName" + name);
        PlayerPrefs.SetInt("level", Application.loadedLevel);
    }
}
