using UnityEngine;
using System.Collections;

public class TrashGenerator : MonoBehaviour
{
    public Sprite[] sprites;
    public GameObject mTrashPrefab;
    public int limitCount = 10;
    int genCount = 0;
    Transform cameraTrans;
    float countTime = 0;

    // Use this for initialization
    void Start()
    {
        cameraTrans = Camera.main.transform;
    }

    // Update is called once per frame
    void Update()
    {
        countTime += Time.deltaTime;
        if (countTime > 1)
        {
            if (genCount < limitCount)
            {
                ++genCount;
                GameObject mGameObj = (GameObject)Instantiate(mTrashPrefab, cameraTrans.position + new Vector3(Random.Range(-12, 12), cameraTrans.position.y + 20, 10), Quaternion.identity);
                Trash mTrash = mGameObj.GetComponent<Trash>();
                mTrash.cameraTrans = cameraTrans;
                mTrash.sprite.sprite = sprites[Random.Range(0, sprites.Length)];
                countTime = 0;
            }
            else if (countTime > 15)
            {
                GameObject mGameObj = (GameObject)Instantiate(mTrashPrefab, cameraTrans.position + new Vector3(Random.Range(-12, 12), cameraTrans.position.y + 20, 10), Quaternion.identity);
                Trash mTrash = mGameObj.GetComponent<Trash>();
                mTrash.cameraTrans = cameraTrans;
                mTrash.sprite.sprite = sprites[Random.Range(0, sprites.Length)];
                countTime = 0;
            }
        }
    }
}
