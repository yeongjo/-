using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class ResSetting : MonoBehaviour
{
    public int wi = 800, he = 500;
    Text mtext;
    void Start()
    {
        mtext = GameObject.Find("MainText").GetComponent<Text>();
    }
    void OnTriggerEnter2D(Collider2D coll)
    {
        if(coll.name == "Player")
        {
            Screen.SetResolution(wi, he, false);
            Debug.Log(wi + " : " + he);
            mtext.text = wi + " : " + he+"  해상도로 변경";
        }
    }
}
