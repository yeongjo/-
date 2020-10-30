using UnityEngine;
using System.Collections;

public class Oso : MonoBehaviour
{
    public LineRenderer[] mLine;
    public Material mMat;
    GameObject childObj;

    void Start()
    {
        mLine = new LineRenderer[10];
        for (int i = 0; i< 10; i++)
        {
            //Debug.Log("I : " + i);
            childObj = new GameObject();
            childObj.transform.SetParent(transform);
            mLine[i] = childObj.AddComponent<LineRenderer>();
            mLine[i].material = mMat;
            mLine[i].SetColors(new Color(66f / 255, 54f / 255, 44f / 255), Color.black);
            mLine[i].SetWidth(.1f, .1f);
            mLine[i].SetPosition(0, transform.position + new Vector3(.1f*i-.5f, 0, 0));
            mLine[i].SetPosition(1, transform.position + new Vector3(.1f * i - .5f, -40, 0));
        }
    }

    // Update is called once per frame
    void Update()
    {
        for (int i = 0; i < 10; i++)
        {
            RaycastHit2D hit = Physics2D.Raycast(transform.position + new Vector3(.1f * i - .5f, 0, 0), Vector2.down, 40);
            if (hit)
            {
                mLine[i].SetPosition(1, hit.point);
				if (hit.collider.name == "Player")
				{
					hit.collider.GetComponent<Player> ().Die ();
				}
            }
        }
    }
}
