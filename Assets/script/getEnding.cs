using UnityEngine;
using System.Collections;

public class getEnding : MonoBehaviour
{
    void Start()
    {
        int a = PlayerPrefs.GetInt("Health");
        switch (a)
        {
            case 1:
                PlayerPrefs.SetInt("EndClear1", 1);
                break;
            case 2:
                PlayerPrefs.SetInt("EndClear2", 2);
                break;
            case 3:
                PlayerPrefs.SetInt("EndClear3", 3);
                break;
            case 4:
                PlayerPrefs.SetInt("EndClear4", 4);
                break;
        }
    }
}
