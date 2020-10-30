using UnityEngine;
using UnityEngine.UI;
//using System;
//using System.Diagnostics;
using System.Collections;
//using System.Runtime.InteropServices;


public class Boss : MonoBehaviour
{
    //[DllImport("user32.dll")]
    //public static extern int FindWindow(string lpClassName, string lpWindowName);
    //[DllImport("user32.dll")]
    //internal static extern bool MoveWindow(IntPtr hwnd, int X, int Y, int nWidth, int nHeight, bool bRepaint);

    public AudioClip AhSound;
    AudioSource mHurtAudio;
    public Transform playerTrans;
   public  int maxHealth = 100;
    int health = 100;

    Transform[] hand = new Transform[2];
    public RectTransform healthImageRect;
    RectTransform healthFakeRect, healthRealRect;
    Coroutine FakeHealthDownCor;
    Animator mAnimator;
    int goUpDownVaule = 0;
    AudioSource mAudio;
    public AudioClip shootSound, whooshSound, clashSound, impactSound;
    public GameObject mbullet;
    public ParticleSystem goUpPar, goDownPar;

    public BossClear bossClear;

    //int windowHandle;
    //int windowPosx, windowPosy;
    public void init()
    {
        playerHealth = PlayerPrefs.GetInt("Health");
        switch (playerHealth)
        {
            case 1:
                maxHealth = 500;
                break;
            case 2:
                maxHealth = 350;
                break;
            case 3:
                maxHealth = 200;
                break;
            case 4:
                maxHealth = 100;
                break;
        }
        health = maxHealth;
    }
    // Use this for initialization
    void Start()
    {
        //windowHandle = FindWindow(null, "계산기");
        //MoveWindow((IntPtr)windowHandle, 0, 0, 500, 500, true);
        init();
        hand[0] = transform.GetChild(0);
        hand[1] = transform.GetChild(1);
        mHurtAudio = transform.GetChild(2).GetComponent<AudioSource>();
        healthFakeRect = healthImageRect.GetChild(0).GetComponent<RectTransform>();
        healthRealRect = healthImageRect.GetChild(1).GetComponent<RectTransform>();
        mAnimator = GetComponent<Animator>();
        mAudio = GetComponent<AudioSource>();

        RandomState();
    }

    //// Update is called once per frame
    //void Update()
    //{
    //    if (Input.GetMouseButtonDown(0))
    //        TakeDamage(10);
    //}

    public void WhooshSound()
    {
        mAudio.PlayOneShot(whooshSound);
    }

    public void ClashSound()
    {
        mAudio.PlayOneShot(clashSound);
    }

    public void ImpactSound()
    {
        mAudio.PlayOneShot(impactSound);
    }

    public void TakeDamage(int damage)
    {
		ImpactSound ();
        mHurtAudio.PlayOneShot(AhSound);
        int prehealth = health;
        int mDamage;
        if (health > 50)
        {
            mDamage = (int)(.1f * damage * damage);
            if (mDamage > maxHealth*.4f) mDamage = (int)(maxHealth*.4f);
        }
        else
        {
            mDamage = (int)(.07f * damage * damage);
            if (mDamage > maxHealth*.3f) mDamage = (int)(maxHealth * .3f);
        }
        
        OnCameraShake(mDamage * .2f);
        health -= mDamage;
        if(health <= 0)
        {
            playerTrans.GetComponent<Rigidbody2D>().isKinematic = true;
            playerTrans.GetComponent<Collider2D>().enabled = false;
            playerTrans.GetComponent<Player>().enabled = false;
            bossClear.enabled = true;
            StopAllCoroutines();
            playerTrans.GetComponent<Player>().Enable();
            mAnimator.SetInteger("State", 0);
            mAnimator.enabled = false;
            this.enabled = false;
        }
        healthRealRect.anchorMax = new Vector2(health / (float)maxHealth, 1);
        if (FakeHealthDownCor != null)
            StopCoroutine(FakeHealthDownCor);
        FakeHealthDownCor = StartCoroutine(FakeHealthDown());
    }

    public void OnCameraShake(float value)
    {
        StartCoroutine(CameraShake(value));
    }

    IEnumerator CameraShake(float value)
    {
        Transform mCamera = Camera.main.transform;
        Vector3 cameraVec = mCamera.position;
        if (value > 3)
            value = 3;
        else if(value < .5f)
            value = .5f;
        for (int i = 0; i< 4; i++)
        {
            mCamera.position += (Vector3)Random.insideUnitCircle * value;
            yield return null;
            yield return null;
        }
    }

    IEnumerator FakeHealthDown()
    {
        yield return new WaitForSeconds(.05f);
        //float deltaValue = (healthRealRect.anchorMax.x- healthFakeRect.anchorMax.x) * .05f;
        for(int i=0;i<50;i++)
        {
            healthFakeRect.anchorMax = new Vector2(Mathf.Lerp(healthFakeRect.anchorMax.x, healthRealRect.anchorMax.x, .1f), 1);
            yield return new WaitForSeconds(.013f);
        }
    }

    int playerHealth;
    IEnumerator Idle()
    {
        float[] delta = new float[2];
        delta[0] = Mathf.Atan2(playerTrans.position.y - hand[0].position.y, playerTrans.position.x - hand[0].position.x) * Mathf.Rad2Deg + 90;
        delta[1] = Mathf.Atan2(playerTrans.position.y - hand[1].position.y, playerTrans.position.x - hand[1].position.x) * Mathf.Rad2Deg + 90;
        for (int i =0;i < 10-playerHealth; i++)
        {
            int ran = Random.Range(0, 2);
            GameObject bullet = (GameObject)Instantiate(mbullet, hand[ran].position, Quaternion.Euler(0, 0, delta[ran] + UnityEngine.Random.Range(25, 70)));
            bullet.GetComponent<BossBullet>().PlayerTrans = playerTrans;
            bullet = (GameObject)Instantiate(mbullet, hand[ran].position, Quaternion.Euler(0, 0, delta[ran]));
            bullet.GetComponent<BossBullet>().PlayerTrans = playerTrans;
            bullet = (GameObject)Instantiate(mbullet, hand[ran].position, Quaternion.Euler(0, 0, delta[ran] + UnityEngine.Random.Range(-25, -70)));
            bullet.GetComponent<BossBullet>().PlayerTrans = playerTrans;
            mAudio.PlayOneShot(shootSound);
			yield return new WaitForSeconds(11f * Time.deltaTime);
        }
		mAnimator.SetInteger("State", 0);
        RandomState();
    }

    public void UpSideDown()
    {
        playerTrans.localScale = new Vector3(playerTrans.localScale.x, -playerTrans.localScale.y, playerTrans.localScale.z);
        playerTrans.GetComponent<Rigidbody2D>().gravityScale *= -1;
    }

    public void goUpParShow()
    {
        goUpPar.Play();
    }
    public void goDownParShow()
    {
        goDownPar.Play();
    }

	float GetShootTime()
	{
		return 11f * Time.deltaTime * (10 - playerHealth);
	}

    IEnumerator Attack01()
    {
        mAnimator.SetInteger("State", 1);
        goUpDownVaule += 1;
		yield return new WaitForSeconds(3.5f - GetShootTime());
        StartCoroutine(Attack02());
    }
    IEnumerator Attack02()
    {
        mAnimator.SetInteger("State", 2);
        goUpDownVaule -= 1;
		yield return new WaitForSeconds(mAnimator.GetCurrentAnimatorStateInfo(0).length - GetShootTime());
        StartCoroutine(Idle());
    }
    IEnumerator Attack03()
    {
        mAnimator.SetInteger("State", 3);
		yield return new WaitForSeconds(mAnimator.GetCurrentAnimatorStateInfo(0).length - GetShootTime());
        StartCoroutine(Idle());
    }

    IEnumerator Attack04()
    {
        mAnimator.SetInteger("State", 4);
		yield return new WaitForSeconds(mAnimator.GetCurrentAnimatorStateInfo(0).length - GetShootTime());
        StartCoroutine(Idle());
    }

    IEnumerator Attack05()
    {
        mAnimator.SetInteger("State", 5);
		yield return new WaitForSeconds(mAnimator.GetCurrentAnimatorStateInfo(0).length - GetShootTime());
        StartCoroutine(Idle());
    }

    IEnumerator Attack06()
    {
        mAnimator.SetInteger("State", 6);
		yield return new WaitForSeconds(mAnimator.GetCurrentAnimatorStateInfo(0).length - GetShootTime());
        StartCoroutine(Idle());
    }

    void RandomState()
    {
        int randomNum = Random.Range(0, 6);
        if (Mathf.Abs(playerTrans.position.x- 9.73f) <= 7.7f)
        {
            switch (randomNum)
            {
                case 0:
                    StartCoroutine(Attack01());
                    break;
                case 1:
                    StartCoroutine(Attack03());
                    break;
                case 2:
                    StartCoroutine(Attack04());
                    break;
                case 3:
                    StartCoroutine(Attack05());
                    break;
                case 4:
                    StartCoroutine(Attack01());
                    break;
                case 5:
                    StartCoroutine(Attack06());
                    break;
            }
        }
        else
        {
            StartCoroutine(Attack06());
        }
        Debug.Log("Random Attack Num" + randomNum);
    }
}
