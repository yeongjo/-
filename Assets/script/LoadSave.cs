using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class LoadSave : MonoBehaviour
{
    Text mtext;
    public Dropdown mDropdown;

    void Start()
    {
        mtext = GameObject.Find("MainText").GetComponent<Text>();
        int health = PlayerPrefs.GetInt("Health");
        if (health != 0)
            mDropdown.value = PlayerPrefs.GetInt("Health") - 1;
        else
        {
            mDropdown.value = 2;
            PlayerPrefs.SetInt("Health", 3);
        }
    }

    void Update()
    {
        int a;
        if((a = PlayerPrefs.GetInt("Health")) != 0)
        {
            mDropdown.value = a-1;
        }
        if(Application.isMobilePlatform)
        {
            if (mtext.text == "두손가락으로 누르면 처음부터 플레이가 가능합니다. (저장기록은 사라지지 않습니다.)" && Input.touchCount > 1)
            {
                PlayerPrefs.SetInt("Health", 3);
                PlayerPrefs.SetInt("level", 1);
                mtext.text = "이제 처음부터 플레이하실수 있습니다!";
            }
        }
        else
        {
            if (mtext.text == "'E'키를 누르면 처음부터 플레이가 가능합니다. (저장기록은 사라지지 않습니다.)" && Input.GetKeyDown(KeyCode.E))
            {
                PlayerPrefs.SetInt("Health", 3);
                PlayerPrefs.SetInt("level", 1);
                mtext.text = "이제 처음부터 플레이하실수 있습니다!";
            }
        }
    }

    void OnTriggerEnter2D(Collider2D coll)
    {
        if (Application.isMobilePlatform)
        {
            mtext.text = "두손가락으로 누르면 처음부터 플레이가 가능합니다. (저장기록은 사라지지 않습니다.)";
        }
        else
        {
			Debug.Log (coll.name);
            if (coll.name == "Player")
            {
                mtext.text = "'E'키를 누르면 처음부터 플레이가 가능합니다. (저장기록은 사라지지 않습니다.)";
            }
        }
    }

   public void Health(int value)
    {
        PlayerPrefs.SetInt("Health", value+1);
        Debug.Log(" : " + value);
    }
}
