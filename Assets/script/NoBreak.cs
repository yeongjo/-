using UnityEngine;
using System.Collections;

public class NoBreak : MonoBehaviour {

	// Use this for initialization
	void Start () {
        DontDestroyOnLoad(transform.gameObject);
    }
}
