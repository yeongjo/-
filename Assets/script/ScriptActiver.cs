using UnityEngine;
using System.Collections;

public class ScriptActiver : MonoBehaviour
{
	public MonoBehaviour mScript;
	bool justOne;

	void OnTriggerEnter2D(Collider2D coll)
	{
		if (justOne)
			return;
		if(coll.gameObject.name == "Player")
		{
			mScript.enabled = true;
			justOne = true;
		}
	}
}

