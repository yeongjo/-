﻿using UnityEngine;
using System.Collections;

public class NextScene : MonoBehaviour
{

    // Use this for initialization
    void Start()
    {
        Application.LoadLevel(Application.loadedLevel + 1);
    }
}
