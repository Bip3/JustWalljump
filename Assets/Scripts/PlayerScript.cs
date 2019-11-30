using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerScript : MonoBehaviour
{
    Rigidbody2D rb2d;
    Animator anim;
    Vector2 savedVelocity;
    SpawnerScript spawnerObj;
    ParticleSystem deathParticles;

    public GameObject testThing;

    float scaleX;
    float localScore;

    public bool canJump = false;
    public bool clinging = false;
    public bool notTouchingUI = true;

    public GameObject dustCloud;

    AudioSource myAudio;
    public AudioClip jumpSound;
    public AudioClip hitWall;
    public AudioClip hurt;

    void Start()
    {
        myAudio = GetComponent<AudioSource>();
        scaleX = transform.localScale.x;
        rb2d = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
        deathParticles = GetComponent<ParticleSystem>();
        spawnerObj = GameObject.Find("Spawner").GetComponent<SpawnerScript>();
    }
    public void musicPlayer(AudioClip clip)
    {
        myAudio.PlayOneShot(clip);
    }
    // Update is called once per frame
    void Update()
    {
        myAudio.volume = GameManager.soundEffectsVolume;

        anim.SetBool("GameActive", GameManager.gameActive);

        anim.SetBool("Clinging", clinging);
        anim.SetBool("CanJump", canJump);

        if (rb2d.velocity.y < -.1f)
        {
            rb2d.gravityScale = 2;
        }
        if (rb2d.velocity.y >= -.1f)
        {
            rb2d.gravityScale = 1;
        }

        if (Input.GetButtonDown("Fire1"))
        {
            if (notTouchingUI == true)
            {
                if (Input.mousePosition.x / Camera.main.pixelWidth > .5f)
                {
                    jumpRight();
                }
                else
                {
                    jumpLeft();
                }
            }
        }

        if (Input.GetKeyDown(KeyCode.RightArrow))
        {
                jumpRight();
        }
        if (Input.GetKeyDown(KeyCode.LeftArrow))
        {
                jumpLeft();
        }
    }
    void jumpLeft()
    {
        if (canJump && GameManager.gameActive)
        {
            if (clinging)
            {
                if (transform.localScale.x == scaleX)
                {
                    musicPlayer(jumpSound);
                    anim.SetTrigger("onJump");
                    rb2d.velocity = new Vector2(-4.5f, 4.5f);
                    transform.localScale = new Vector3(-scaleX, transform.localScale.y, transform.localScale.z);
                    canJump = false;
                }
            }
            else
            {
                musicPlayer(jumpSound);
                anim.SetTrigger("onJump");
                rb2d.velocity = new Vector2(-4.5f, 4.5f);
                transform.localScale = new Vector3(-scaleX, transform.localScale.y, transform.localScale.z);
                canJump = false;
            }
        }

    }
    void jumpRight()
    {
        if (canJump && GameManager.gameActive)
        {
            if (clinging)
            {
                if (transform.localScale.x == -scaleX)
                {
                    musicPlayer(jumpSound);
                    anim.SetTrigger("onJump");
                    rb2d.velocity = new Vector2(4.5f, 4.5f);
                    transform.localScale = new Vector3(scaleX, transform.localScale.y, transform.localScale.z * -1);
                    canJump = false;
                }
            }
            else
            {
                musicPlayer(jumpSound);
                anim.SetTrigger("onJump");
                rb2d.velocity = new Vector2(4.5f, 4.5f);
                transform.localScale = new Vector3(scaleX, transform.localScale.y, transform.localScale.z * -1);
                canJump = false;
            }
            
        }

    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Enemy"))
        {
            StartCoroutine(TimeToDie());
        }
    }

    IEnumerator TimeToDie()
    {
        GetComponent<BoxCollider2D>().enabled = false;
        deathParticles.Play();
        GetComponent<SpriteRenderer>().enabled = false;
        rb2d.constraints = RigidbodyConstraints2D.FreezeAll;
        GetComponent<TrailRenderer>().enabled = false;
        GameManager.gameActive = false;
        anim.ResetTrigger("onJump");
        GameManager.score += GameManager.scoreToBeAdded;
        GameManager.scoreToBeAdded = 0;
        GameObject.Find("UIManager").GetComponent<UIScript>().scoreText.text = "" + GameManager.score;
        musicPlayer(hurt);

        yield return new WaitForSeconds(1.0f);
        spawnerObj.endGame();
        
    }
    public void resetMe()
    {
        transform.position = new Vector3(0, .3f, -1f);
        GetComponent<BoxCollider2D>().enabled = true;
        clinging = false;
        canJump = false;
        GetComponent<SpriteRenderer>().enabled = true;
        anim.Play("IdleAnim", -1, 0f);
        GetComponent<TrailRenderer>().enabled = true;
        rb2d.constraints = RigidbodyConstraints2D.FreezeRotation;
    }
    public void spawnDustClouds()
    {
        int spawnAmount;
        spawnAmount = Random.Range(1, 3);
        for (int i = 0; i < spawnAmount; i++)
        {
            Instantiate(dustCloud, transform.position, Quaternion.identity);
        }
    }
}
