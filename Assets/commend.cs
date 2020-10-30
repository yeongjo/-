using UnityEngine;
using System.Collections;

public class commend : MonoBehaviour {

    public void Yes()
    {
        Application.OpenURL("http://www.ziksir.com/ziksir/board/view/72");
        this.gameObject.SetActive(false);
    }

    public void No()
    {
        this.gameObject.SetActive(false);
    }
}
