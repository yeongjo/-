using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class Player : MonoBehaviour {

    Animator mAnimator;
    [System.NonSerialized]
    public Rigidbody2D rigid;
    public float speed = 3.6f;
    public float jumpForce = 10;

	AudioSource audioSource;
	public AudioClip jumpAudio;
	public AudioClip dieAudio;
    public bool bCanDoubleJump;
    bool bIsDoubleJumping;
    bool bUsedJump;
    int havingTrashCount;
    public GameObject playerBullet;
    GameObject insideParticle;
    ParticleSystem outParticle;
    AudioSource boomSound;
    AudioSource MainMusicAudio;

    public float maxHealth = 3;
    float health;
    float RegenDelay = 1.5f;
    public AudioClip hitSound;
    public Image bloodImage;
    public AudioSource mBreathAudio;
    SpriteRenderer mSprite;

    bool bHardCore;

    // Use this for initialization
    void Start () {
        Time.timeScale = 1;
        Init();
        mSprite = GetComponent<SpriteRenderer>();
        mAnimator = GetComponent<Animator>();
		audioSource = GetComponent<AudioSource> ();
        rigid = GetComponent<Rigidbody2D>();
        insideParticle = transform.GetChild(0).gameObject;
        insideParticle.SetActive(false);
        outParticle = transform.GetChild(1).GetComponent<ParticleSystem>();
        boomSound = outParticle.GetComponent<AudioSource>();

        GameObject Music;
        if (Music = GameObject.Find("Music"))
            MainMusicAudio = Music.GetComponent<AudioSource>();
	}

    public void Init()
    {
        maxHealth = PlayerPrefs.GetInt("Health");
        if (maxHealth == 2)
        {
            bHardCore = true;
            maxHealth = 3;
        }
        else if (maxHealth == 1)
        {
            bHardCore = true;
            maxHealth = 2;
        }
        health = maxHealth;
    }
	
	// Update is called once per frame
	void Update () {
        Move();
        Jump();
		Restart ();
        Shoot();
        if(bloodImage)
            Regen();
        //if (Input.GetMouseButtonDown(0))
        //    TakeDamage();
    }

    void Regen()
    {
        RegenDelay += Time.deltaTime;
        bloodImage.color = Color.Lerp(bloodImage.color, new Color(1, 0, 0, (maxHealth - health) * .3f), .1f);
        mBreathAudio.volume = (maxHealth - health) * .5f;
        if (MainMusicAudio)
            MainMusicAudio.volume = health * 1/maxHealth - .25f;
        if (RegenDelay < 1.5f)
            return;
        health += Time.deltaTime;
        if (health > maxHealth)
            health = maxHealth;
    }

	void Restart()
	{
		if (Input.GetKeyDown (KeyCode.R)) 
		{
			Application.LoadLevel(Application.loadedLevel);
		}
        if(Input.touchCount > 2)
        {
            Application.LoadLevel(Application.loadedLevel);
        }
	}

    public void Enable()
    {
        if(mBreathAudio)
            mBreathAudio.enabled = false;
        mAnimator.SetBool("bJump", false);
        mAnimator.SetBool("bRun", false);
        if(bloodImage)
            bloodImage.color = Color.clear;
        this.enabled = false;
    }

    bool bCantTakeDamage;
    IEnumerator DamageDelay()
    {
        mSprite.color = Color.clear;
        yield return new WaitForSeconds(.04f);
        bCantTakeDamage = false;
        yield return new WaitForSeconds(.06f);
        mSprite.color = Color.white;
        yield return new WaitForSeconds(.1f);
        mSprite.color = Color.clear;
        yield return new WaitForSeconds(.1f);
        mSprite.color = Color.white;
        yield return new WaitForSeconds(.1f);
    }
    Coroutine Dama;
    public void TakeDamage()
    {
        if (!bHardCore)
        {
            if (bCantTakeDamage) return;
            bCantTakeDamage = true;
            if (Dama != null)
                StopCoroutine(Dama);
            Dama = StartCoroutine(DamageDelay());
        }
        OnCameraShake( .2f);
        audioSource.PlayOneShot(hitSound);
        health--;
        RegenDelay = 0;

        if (health <= 0)
        {
            health = 0;
            bloodImage.color = new Color(1, 0, 0, .9f);
           Die();
        }
    }

    public void Die()
    {
		Debug.Log ("die");
        mAnimator.SetTrigger("bDie");
        if(MainMusicAudio)
            MainMusicAudio.enabled = false;
        if(mBreathAudio)
        mBreathAudio.volume = 0;
        GetComponent<Collider2D>().enabled = false;
		Camera.main.GetComponent<CameraContorller> ().enabled = false;
		audioSource.PlayOneShot (dieAudio);
        rigid.velocity = new Vector2(0, 10);
        StartCoroutine(DieDelay((MainMusicAudio)? MainMusicAudio.gameObject:null));
    }

    IEnumerator DieDelay(GameObject music)
    {
		this.enabled = false;
		GameObject a = GameObject.Find ("Canvas");
		Transform deadScreenTrans = a.transform.GetChild (1);
		deadScreenTrans.gameObject.SetActive (true);
		Image b = deadScreenTrans.GetComponent<Image> ();
		for(int i=0;i<10;i++)
		{
			//Debug.Log ("sdf : "+b.name);
			b.color = Color.Lerp(b.color, Color.black, 8*Time.deltaTime);
			yield return new WaitForSeconds (.1f);
		}
		while(Input.touchCount < 3 && !Input.GetKey(KeyCode.R))
		{
			yield return new WaitForSeconds (Time.deltaTime);
		}
        if(music)
            music.GetComponent<AudioSource>().enabled = true;
        Application.LoadLevel(Application.loadedLevel);
    }

    void Jump()
    {
        RaycastHit2D hit;
        hit = Physics2D.Linecast(transform.position + new Vector3(-0.15f, -0.055f*transform.localScale.y, 0), transform.position + new Vector3(0.15f, -0.055f * transform.localScale.y, 0), (-1)-(1<<4));
        if (rigid.velocity.y != 0 && !hit)
        {
            mAnimator.SetBool("bJump", true);
            bUsedJump = true;
        }
        else
        {
            mAnimator.SetBool("bJump", false);
            bUsedJump = false;
            bIsDoubleJumping = false;
        }
        if (!Application.isMobilePlatform)
        {
            if (Input.GetKeyDown(KeyCode.UpArrow))
            {
                if (bCanDoubleJump && bUsedJump && !bIsDoubleJumping)
                {
                    audioSource.PlayOneShot(jumpAudio);
                    rigid.velocity = new Vector2(0, jumpForce * transform.localScale.y);
                    bIsDoubleJumping = true;
                }
                else {
                    if (hit)
                        if (hit && Mathf.Abs(hit.transform.rotation.z) < .7f)
                        {
                            audioSource.PlayOneShot(jumpAudio);
                            rigid.velocity = new Vector2(0, jumpForce * transform.localScale.y);
                        }
                }
            }
        }
        else
        {
            for (int i = 0; i < Input.touchCount; i++)
            {
                if (Input.GetTouch(i).phase == TouchPhase.Began && Input.GetTouch(i).position.x < Screen.width * .5f)
                {
                    if (bCanDoubleJump && bUsedJump && !bIsDoubleJumping)
                    {
                        audioSource.PlayOneShot(jumpAudio);
                        rigid.velocity = new Vector2(0, jumpForce * transform.localScale.y);
                        bIsDoubleJumping = true;
                    }
                    else {
                        if (hit)
                            if (hit && Mathf.Abs(hit.transform.rotation.z) < .7f)
                            {
                                audioSource.PlayOneShot(jumpAudio);
                                rigid.velocity = new Vector2(0, jumpForce * transform.localScale.y);
                            }
                    }
                }
            }
        }
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
            mCamera.position += (Vector3)Random.insideUnitCircle * value;
            yield return null;
            yield return null;
        }
    }

    float shootDelay = 1;
    public void Shoot()
    {
        shootDelay += Time.deltaTime;
        if (!bCanDoubleJump || shootDelay < 1)
            return;
        //insideParticle.GetComponent<AudioSource>().volume = .1f * havingTrashCount + .2f;
        insideParticle.GetComponent<AudioSource>().pitch = .1f * havingTrashCount + .2f;
        switch (havingTrashCount)
        {
            case 0:
            case 1:
                boomSound.pitch = 1f;
                break;
            case 2:
            case 3:
                boomSound.pitch = 1.5f;
                break;
            case 4:
            case 5:
                boomSound.pitch = 2;
                break;
            default:
                boomSound.pitch = 3;
                break;
        }
        if (!Application.isMobilePlatform)
        {
            if (Input.GetKey(KeyCode.Z))
            {
                OnCameraShake(((havingTrashCount + 1 > 5) ? 5 : (havingTrashCount + 1)) * .01f);
                insideParticle.SetActive(true);
                Collider2D[] hit = Physics2D.OverlapCircleAll(transform.position + new Vector3(0, .7f * transform.localScale.y, 0), 3, 1 << 9);
                if (hit.Length > 0)
                {
                    for (int i = 0; i < hit.Length; i++)
                    {
                        if ((hit[i].transform.position.x - transform.position.x) * transform.localScale.x > 0)
                        {
                            hit[i].GetComponent<BossBullet>().bCanHit = false;
                            if (Vector2.SqrMagnitude(hit[i].transform.position - (transform.position + Vector3.up * .7f * transform.localScale.y)) > .4f)
                                hit[i].transform.position += (transform.position + Vector3.up * .7f * transform.localScale.y - hit[i].transform.position) * Time.deltaTime * 17;
                            else
                            {
                                DestroyObject(hit[i].gameObject);
                                havingTrashCount++; ;
                            }
                        }
                    }
                }
            }
            else if (Input.GetKeyUp(KeyCode.Z))
            {
                shootDelay = 0;
                insideParticle.SetActive(false);
                outParticle.Play();
                boomSound.Play();
                GameObject bullet = (GameObject)Instantiate(playerBullet, transform.position + Vector3.up * .7f * transform.localScale.y, Quaternion.identity);
                bullet.transform.localScale = transform.localScale;
                bullet.GetComponent<bullet>().damage = havingTrashCount;
                havingTrashCount = 0;
            }
        }
        else {
            for (int j = 0; j < Input.touchCount; j++)
            {
                if (Input.GetTouch(j).position.x > Screen.width * .5f && Input.GetTouch(j).phase == TouchPhase.Stationary)
                {
                    OnCameraShake(((havingTrashCount + 1 > 5) ? 5 : (havingTrashCount + 1)) * .01f);
                    insideParticle.SetActive(true);
                    Collider2D[] hit = Physics2D.OverlapCircleAll(transform.position + new Vector3(0, .7f * transform.localScale.y, 0), 3, 1 << 9);
                    if (hit.Length > 0)
                    {
                        for (int i = 0; i < hit.Length; i++)
                        {
                            if ((hit[i].transform.position.x - transform.position.x) * transform.localScale.x > 0)
                            {
                                hit[i].GetComponent<BossBullet>().bCanHit = false;
                                if (Vector2.SqrMagnitude(hit[i].transform.position - (transform.position + Vector3.up * .7f * transform.localScale.y)) > .4f)
                                    hit[i].transform.position += (transform.position + Vector3.up * .7f * transform.localScale.y - hit[i].transform.position) * Time.deltaTime * 17;
                                else
                                {
                                    DestroyObject(hit[i].gameObject);
                                    havingTrashCount++; ;
                                }
                            }
                        }
                    }
                }
                else if (Input.GetTouch(j).phase == TouchPhase.Ended && Input.GetTouch(j).position.x > Screen.width * .5f)
                {
                    shootDelay = 0;
                    insideParticle.SetActive(false);
                    outParticle.Play();
                    boomSound.Play();
                    GameObject bullet = (GameObject)Instantiate(playerBullet, transform.position + Vector3.up * .7f * transform.localScale.y, Quaternion.identity);
                    bullet.transform.localScale = transform.localScale;
                    bullet.GetComponent<bullet>().damage = havingTrashCount;
                    havingTrashCount = 0;
                }
            }
        }
    }

    void Move()
    {
        Vector2 InputVecter = new Vector2(Input.GetAxis("Horizontal"), 0);
        if(Application.isMobilePlatform)
            InputVecter = Input.acceleration;
        //Debug.Log("InputVecter : " + InputVecter);

        if (Application.isMobilePlatform)
        {
            InputVecter.x *= 3;
            InputVecter.x = (InputVecter.x > 1) ? 1 : InputVecter.x;
            InputVecter.x = (InputVecter.x < -1) ? -1 : InputVecter.x;
            if (Mathf.Abs(InputVecter.x) > .3f)
                mAnimator.SetBool("bRun", true);
            else
                mAnimator.SetBool("bRun", false);
            if (InputVecter.x > .3f)
                transform.localScale = new Vector3(1, transform.localScale.y, transform.localScale.z);
            else if (InputVecter.x < -.3f)
                transform.localScale = new Vector3(-1, transform.localScale.y, transform.localScale.z);
            if (Mathf.Abs(InputVecter.x) > .3f)
            {
                transform.Translate(InputVecter.x * speed * Time.deltaTime, 0, 0);
            }
        }
        else
        {
            if (Mathf.Abs(InputVecter.x) > 0)
                mAnimator.SetBool("bRun", true);
            else
                mAnimator.SetBool("bRun", false);
            if (InputVecter.x > 0)
                transform.localScale = new Vector3(1, transform.localScale.y, transform.localScale.z);
            else if (InputVecter.x < 0)
                transform.localScale = new Vector3(-1, transform.localScale.y, transform.localScale.z);
                transform.Translate(InputVecter.x * speed * Time.deltaTime, 0, 0);
        }
    }
}
