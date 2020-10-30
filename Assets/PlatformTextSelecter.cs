using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlatformTextSelecter : MonoBehaviour {

	[TextArea]
	public string for_android_;
	[TextArea]
	public string for_pc_;

	// Use this for initialization
	void Start () {
		TextMesh this_text = GetComponent<TextMesh>();
		if (Application.isMobilePlatform)
			this_text.text = for_android_;
		else
			this_text.text = for_pc_;
	}
}
