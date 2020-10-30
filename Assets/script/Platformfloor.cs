using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Platformfloor : MonoBehaviour
{
    Vector3 firstVec;
    public Vector3 secondVec;
    public float speed = 1;
    bool bGoForward = true;
    List<Transform> upList = new List<Transform>();
	Vector3 deltaPos;
	Vector3 previousPos;

    void Start()
    {
        firstVec = transform.position;
		deltaPos = transform.position;
    }

    void Update()
    {
        if (Vector3.SqrMagnitude(secondVec - transform.position) < .1f)
        {
            bGoForward = false;
        }
        else if(Vector3.SqrMagnitude(firstVec - transform.position) < .1f)
        {
            bGoForward = true;
        }

        if(bGoForward)
        {
			transform.position = Vector3.Slerp(transform.position, secondVec, speed * Time.deltaTime);
            for(int i =0;i<upList.Count;i++)
            {
				upList[i].transform.position += deltaPos;
            }
        }
        else
        {
			transform.position = Vector3.Slerp(transform.position, firstVec, speed * Time.deltaTime);
            for (int i = 0; i < upList.Count; i++)
            {
				upList[i].transform.position += deltaPos;
            }

		}
		deltaPos = transform.position - previousPos;
		previousPos = transform.position;
	}

    void OnCollisionEnter2D(Collision2D coll)
    {
		//Debug.Log (coll.gameObject.layer);
		if (coll.gameObject.layer == 8)
			return;
		if (coll.contacts [0].point.y < transform.position.y)
			return;
        upList.Add(coll.transform);
    }

    void OnCollisionExit2D(Collision2D coll)
    {
		if (coll.collider.IsTouchingLayers (8))
			return;
        for(int i = 0; i< upList.Count; i++)
        {
            if (upList[i] == coll.transform)
            {
                upList.RemoveAt(i);
                break;
            }
        }
    }
}