using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class Player : MonoBehaviour
{
    Scene _scene;
    Brain _keyListener;
    Rigidbody2D _rb;
    Animator _anim;
    SpriteRenderer _sr;
    AudioSource _audio;
    public GameManager game;

    public GameObject blastPrefab;
    public Image healthBar;
    public Image specialCooldown;

    public AudioClip punch;
    public AudioClip special;
    public AudioClip blast;
    public AudioClip takeDamage;
    public AudioClip intro;
    public AudioClip charge;

    public GameObject[] blastChargesImages = new GameObject[5];

    public float speed = 7f;
    public int health;
    public int blastCharges;
    bool punching;
    float punchCD = 0;
    float specialCD = 0;
    float blastCD = 0;
    float blastChargesCD;
    bool invincible;

    float currentTimeDamage;
    public float maxTimeDamage = 0.8f;
    public bool isTakingDamage;
    public bool willTakeDamage;
    bool canMove;

    public bool introActive;
    public bool introActive2;
    public float introTimer;

    public bool tutorialLockPunch;
    public bool tutorialLockSpecial;
    public bool tutorialLockBlast;

    void Awake()
    {
        _rb = GetComponent<Rigidbody2D>();
        _anim = GetComponent<Animator>();
        _audio = GetComponent<AudioSource>();
        _sr = GetComponent<SpriteRenderer>();
        _scene = SceneManager.GetActiveScene();
    }

    void Start()
    {
        _keyListener = new Brain(this);
        health = 5;
        blastCharges = 0;
        introActive2 = false;
        invincible = false;
        canMove = true;

        if (_scene.name == "1")
        {
            transform.position = new Vector3(-10f, 0, 0);
            introActive = true;
        }
        else
        {
            introActive = false;
        }
    }

    void Update()
    {
        if (canMove)
            _keyListener.ListenerKeys();

        if (introActive2)
            Intro2();

        Intro();
        Cooldowns();
        Interface();
        Death();

        if (isTakingDamage)
        {
            currentTimeDamage += Time.deltaTime;
            if (currentTimeDamage >= maxTimeDamage)
            {
                isTakingDamage = false;
                currentTimeDamage = 0f;
                _sr.color = Color.white;
            }
            else
            {
                _sr.color = Color.red;
            }
        }

        if (punchCD < 0.35f && !isTakingDamage) //is punching, velocity = 0, and destroys enemy
        {
            _rb.velocity = Vector3.zero;

            if (punchCD < 0.2f) //more precise for destroying enemy
            {
                punching = true; //destroys in collisionEnter2D
            }
            else
            {
                punching = false;
            }
        }
    }

    public void Move(float DirX, float DirY)
    {
        if (!isTakingDamage && !introActive && punchCD > 0.25f)
        {
            _rb.velocity = new Vector3(DirX * speed, DirY * speed, 0);
            _anim.SetFloat("DirX", DirX);
            _anim.SetFloat("DirY", DirY);

            if (DirX != 0 || DirY != 0)
                _anim.SetBool("Moving", true);
            else
                _anim.SetBool("Moving", false);
            }
        }

    public void Punch()
    {
        if (!introActive && !introActive2 && !game.pauseStatus && !tutorialLockPunch)
            if (!isTakingDamage && punchCD >= 0.35f && blastCD >= 0.15f)
            {
                punchCD = 0;
                _anim.SetTrigger("Punch");
                _audio.clip = punch;
                _audio.Play();
            }
    }

    public void Special()
    {
        if (!introActive && !introActive2 && !game.pauseStatus && !tutorialLockSpecial)
            if (specialCD >= 8f && !isTakingDamage)
            {
                speed = 4f; //slower movement speed in special
                specialCD = 0f;
                _anim.SetTrigger("Special");
                _audio.clip = special;
                _audio.Play();
            }
    }

    public void Blast()
    {
        if (!introActive && !introActive2 && !game.pauseStatus && !tutorialLockBlast)
            if (!isTakingDamage && blastCD >= 0.15f && punchCD >= 0.25f && blastCharges > 0)
            {
                blastCD = 0f;
                _anim.SetTrigger("Blast");
                _audio.clip = blast;
                _audio.Play();

                GameObject Blast = Instantiate(blastPrefab);
                blastCharges--;
            }
    }

    public void Damage(int quantity)
    {
        if (!introActive)
        {
            health -= quantity;
            isTakingDamage = true;
            _anim.SetTrigger("Damage");
            _audio.clip = takeDamage;
            _audio.Play();
            _rb.velocity = new Vector3(-10f, Random.Range(-4f, 4f), 0f);
        }
    }

    void Intro()
    {
        if (introActive)
        {
            introTimer += Time.deltaTime;

            if (introTimer > 2f && introTimer < 2.7f)
            {
                _audio.clip = intro;
                _audio.Play();
            }
            else if (introTimer > 2.7f && introTimer < 3.3f)
            {
                _anim.SetBool("Intro", true);
                transform.position += Vector3.right * 25f * Time.deltaTime;
            }
            else if (introTimer > 6f && introTimer < 8f)
            {
                transform.position -= Vector3.right * 5f * Time.deltaTime;
            }
            else if (introTimer > 8f)
            {
                introActive = false;
                introTimer = 0;
                _anim.SetBool("Intro", false);
            }
        }
    }

    public void Intro2()
    {
        introTimer += Time.deltaTime;
        invincible = true;

        if (introTimer < 0.1f)
        {
            _audio.clip = charge;
            _audio.Play();
        }
        else if (introTimer > 0.1f && introTimer < 3f)
        {
            _anim.SetBool("Intro", true);
        }
        else if (introTimer > 3f)
        {
            _anim.SetBool("Intro", false);
            invincible = false;
            introActive2 = false;
            introTimer = 0;
        }
    }

    void Cooldowns()
    {
        punchCD += Time.deltaTime;
        specialCD += Time.deltaTime;
        blastCD += Time.deltaTime;

        if (blastCharges < 5)
        {
            blastChargesCD += Time.deltaTime;
        }

        if (blastChargesCD > 2f)
        {
            blastCharges++;
            blastChargesCD = 0;
        }

        if (specialCD >= 1f) //out of special, normal speed
            speed = 7f;
    }

    void Interface()
    {
        //status
        healthBar.fillAmount = health / 5f;
        specialCooldown.fillAmount = specialCD / 8f;
        //blast charges
        switch (blastCharges)
        {
            case 0:
                for (int i = 0; i < 5; i++)
                    blastChargesImages[i].SetActive(false);
                break;

            case 1:
                blastChargesImages[0].SetActive(true);
                for (int i = 1; i < 5; i++)
                {
                    blastChargesImages[i].SetActive(false);
                }
                break;

            case 2:
                blastChargesImages[0].SetActive(true);
                blastChargesImages[1].SetActive(true);
                for (int i = 2; i < 5; i++)
                {
                    blastChargesImages[i].SetActive(false);
                }
                break;

            case 3:
                for (int i = 0; i < 3; i++)
                {
                    blastChargesImages[i].SetActive(true);
                }
                blastChargesImages[3].SetActive(false);
                blastChargesImages[4].SetActive(false);
                break;

            case 4:
                for (int i = 0; i < 4; i++)
                {
                    blastChargesImages[i].SetActive(true);
                }
                blastChargesImages[4].SetActive(false);
                break;

            case 5:
                for (int i = 0; i < 5; i++)
                {
                    blastChargesImages[i].SetActive(true);
                }
                break;
        }

        //player out of camera
        if (!introActive)
        {
            if (transform.position.x >= 8.4f)
                transform.position += Vector3.left * 0.2f;
            if (transform.position.x <= -8.4f)
                transform.position += Vector3.right * 0.2f;
            if (transform.position.y >= 4.1f)
                transform.position += Vector3.down * 0.2f;
            if (transform.position.y <= -4.1f)
                transform.position += Vector3.up * 0.2f;
        }
    }

    void Death()
    {
        if (health <= 0)
        {
            SceneManager.LoadScene("Menu");
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (specialCD < 1) //in special
        {
            Destroy(collision.collider);
        }

        else
        {
            if (punching)
            {
                if (collision.gameObject.GetComponent<Comet>() || collision.gameObject.GetComponent<Enemy>())
                {
                    Destroy(collision.collider);
                }
            }
            else //not punching, this cancels healing if punching. Also notice that if punching = true, meteor won't do damage
            {
                if (collision.gameObject.GetComponent<Comet>() || collision.gameObject.GetComponent<Enemy>() || collision.gameObject.GetComponent<Meteor>())
                {
                    if (!isTakingDamage && !invincible)
                        Damage(1);
                }

                else if (collision.gameObject.GetComponent<Health>())
                {
                    game.PlusScore(5);
                    if (health < 5)
                        health++;
                }
            }
        }
    }
}