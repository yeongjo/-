using UnityEngine;
using System.Collections;

public class SmallBoss : MonoBehaviour
{
    public AudioClip ahSound;
    AudioSource mHurtAudio;
    public AudioClip shootSound;
    public GameObject mBullet;
    Coroutine FakeHealthDownCor;
    public RectTransform healthImageRect;
    RectTransform healthFakeRect, healthRealRect;
    public Transform playerTrans;
    Transform headTrans;
    int health = 100;
    Animator mAnimator;
    LineRenderer mLineRen;
    public smallBossClear mClear;

    Coroutine FakeHealthCor;
    // Use this for initialization
    void Start()
    {
        mHurtAudio = GetComponent<AudioSource>();
        mAnimator = GetComponent<Animator>();
        mLineRen = GetComponent<LineRenderer>();
        healthFakeRect = healthImageRect.GetChild(0).GetComponent<RectTransform>();
        healthRealRect = healthImageRect.GetChild(1).GetComponent<RectTransform>();
        headTrans = transform.GetChild(0);

        StartCoroutine(Idle());
    }

    // Update is called once per frame
    void Update()
    {
        NeckController();
    }

    void NeckController()
    {
		mLineRen.SetPosition(0, transform.position + Vector3.up * .6f + Vector3.forward);
		mLineRen.SetPosition(1, headTrans.position + Vector3.forward);
    }

    public void OnShoot()
    {
        StartCoroutine(Shoot());
    }

    IEnumerator Shoot()
    {
        float delta = Mathf.Atan2(playerTrans.position.y - headTrans.position.y, playerTrans.position.x - headTrans.position.x) * Mathf.Rad2Deg + 90;
        for (int i = 0; i < 3; i++)
        {
            GameObject bullet = (GameObject)Instantiate(mBullet, headTrans.position, Quaternion.Euler(0, 0, delta + UnityEngine.Random.Range(30, 90)));
            bullet.GetComponent<BossBullet>().PlayerTrans = playerTrans;
            bullet = (GameObject)Instantiate(mBullet, headTrans.position, Quaternion.Euler(0, 0, delta));
            bullet.GetComponent<BossBullet>().PlayerTrans = playerTrans;
            bullet = (GameObject)Instantiate(mBullet, headTrans.position, Quaternion.Euler(0, 0,  + UnityEngine.Random.Range(-30, -90)));
            bullet.GetComponent<BossBullet>().PlayerTrans = playerTrans;
            mHurtAudio.PlayOneShot(shootSound);
            yield return new WaitForSeconds(.3f);
        }
    }

    public void TakeDamage(int damage)
    {
        mHurtAudio.PlayOneShot(ahSound);
        int mDamage = (int)(.1f * damage * damage)+1;
        if (mDamage > 50) mDamage = 50;
        OnCameraShake(mDamage * .2f);
        health -= mDamage;
        Debug.Log("mDamage : " + mDamage + " " + health + "  " + (health / 100f));
        if (health <= 0)
        {
            mClear.enabled = true;
            StopAllCoroutines();
            playerTrans.GetComponent<Player>().Enable();
            playerTrans.GetComponent<Rigidbody2D>().isKinematic = true;
            playerTrans.GetComponent<Collider2D>().enabled = false;
            mAnimator.SetInteger("State", 0);
            mAnimator.enabled = false;
        }
        healthRealRect.anchorMax = new Vector2(health / 100f, 1);
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
        for (int i = 0; i < 4; i++)
        {
            mCamera.position += Random.insideUnitSphere * value;
            yield return null;
            yield return null;
        }
    }

    IEnumerator FakeHealthDown()
    {
        yield return new WaitForSeconds(.05f);
        //float deltaValue = (healthRealRect.anchorMax.x- healthFakeRect.anchorMax.x) * .05f;
        for (int i = 0; i < 50; i++)
        {
            healthFakeRect.anchorMax = new Vector2(Mathf.Lerp(healthFakeRect.anchorMax.x, healthRealRect.anchorMax.x, .1f), 1);
            yield return new WaitForSeconds(.013f);
        }
    }

    IEnumerator Idle()
    {
        mAnimator.SetInteger("State", 0);
        yield return new WaitForSeconds(2);
        int ran = Random.Range(0, 2);
        switch(ran)
        {
            case 0:
                StartCoroutine(Attack01());
                break;
            case 1:
                StartCoroutine(Attack02());
                break;
        }
    }

    IEnumerator Attack01()
    {
        mAnimator.SetInteger("State", 1);
        yield return new WaitForSeconds(mAnimator.GetCurrentAnimatorStateInfo(0).length);
        StartCoroutine(Idle());
    }

    IEnumerator Attack02()
    {
        mAnimator.SetInteger("State", 2);
        yield return new WaitForSeconds(mAnimator.GetCurrentAnimatorStateInfo(0).length);
        StartCoroutine(Idle());
    }
}
