using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class BossClear : MonoBehaviour
{
    public AudioSource muteAudio;
    public AudioClip laserPowerUpSound;
    AudioSource mAudio;
    public GameObject[] clears;
    public Image fadeImage;
    public GameObject[] destoryObjs;
    public Player mPlayer;
    public AudioSource mNoAudio;
    public AudioClip nooooSound;
    public Esc mEsc;

    // Use this for initialization
    void Start()
    {
        mEsc.gameObject.SetActive(false);
        mPlayer.GetComponent<Collider2D>().enabled = false;
		mPlayer.GetComponent<Rigidbody2D> ().simulated = false;
        mPlayer.enabled = false;
        destoryObjs = GameObject.FindGameObjectsWithTag("BossBullet");
        for(int i = 0; i < destoryObjs.Length; i++)
        {
            Destroy(destoryObjs[i]);
        }
        mAudio = GetComponent<AudioSource>();
        StartCoroutine(LaserSound());
    }

    IEnumerator LaserSound()
    {
        yield return new WaitForSeconds(1);
        Camera.main.transform.position = new Vector3(9, 4, -10);
       StartCoroutine(CameraShakeUp());
        mAudio.PlayOneShot(laserPowerUpSound);
        clears[0].SetActive(true);
        yield return new WaitForSeconds(.8f);
        mAudio.PlayOneShot(laserPowerUpSound);
        clears[1].SetActive(true);
        clears[2].SetActive(true);
        yield return new WaitForSeconds(.8f);
        mAudio.PlayOneShot(laserPowerUpSound);
        clears[3].SetActive(true);
        yield return new WaitForSeconds(.7f);
        mAudio.PlayOneShot(laserPowerUpSound);
        clears[4].SetActive(true);
        yield return new WaitForSeconds(.6f);
        mNoAudio.PlayOneShot(nooooSound);
        mNoAudio.pitch = .4f;
        mAudio.PlayOneShot(laserPowerUpSound);
        clears[5].SetActive(true);
        yield return new WaitForSeconds(.5f);
        mAudio.PlayOneShot(laserPowerUpSound);
        clears[6].SetActive(true);
        yield return new WaitForSeconds(.4f);
        mAudio.PlayOneShot(laserPowerUpSound);
        clears[7].SetActive(true);
        clears[8].SetActive(true);
        yield return new WaitForSeconds(.3f);
        mAudio.PlayOneShot(laserPowerUpSound);
        yield return new WaitForSeconds(6);
        Application.LoadLevel("lastbossFightRoom");
    }

    IEnumerator CameraShakeUp()
    {
        mNoAudio.enabled = true;
        Transform cameraTrans = Camera.main.transform;
        GameObject.Find("Player").GetComponent<Player>().enabled = false;
        cameraTrans.GetComponent<CameraContorller>().enabled = false;
        Vector3 cameraPos = new Vector3(9,4,-10);
        float amount = .1f;
        int count = 0;
        while (count < 200)
        {
            muteAudio.volume = muteAudio.volume - .05f;
            mAudio.pitch = 1f + amount * .06f;
            cameraTrans.position = cameraPos + Random.insideUnitSphere * amount * 2f;
            amount += Time.deltaTime * .5f;
            fadeImage.color = new Color(1, 1, 1, amount * 1f);
            if (amount > 5f) amount = 5f;
            count++;
            yield return null;
            yield return null;
            yield return null;
        }

    }

    // Update is called once per frame
    void Update()
    {

    }
}
