using UnityEngine;
using System.Collections;

public class EndingCredit : MonoBehaviour
{
    public float speed  = 10;
    RectTransform mRect;
    // Use this for initialization
    void Start()
    {
        mRect = GetComponent<RectTransform>();
    }

    // Update is called once per frame
    void Update()
    {
        mRect.position += Vector3.up * speed * Screen.height / 500 * Time.deltaTime;
    }
}
