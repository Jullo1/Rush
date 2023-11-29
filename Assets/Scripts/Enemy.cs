using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy: MonoBehaviour
{
    AudioSource _audio;
    GameObject _game;
    GameManager _gameManager;
    Animator _anim;

    public AudioClip destroyAudio;

    public float speed;
    float startPosY;
    float timer;
    float minSpeed;
    float maxSpeed;

    bool destroyed;


    void Awake()
    {
        _game = GameObject.FindGameObjectWithTag("GameController");
        _gameManager = _game.GetComponent<GameManager>();
        _audio = GetComponent<AudioSource>();
        _anim = GetComponent<Animator>();
    }

    void Start()
    {
        switch (_gameManager.stage)
        {
            case 1:
                minSpeed = 8f;
                maxSpeed = 12f;
                break;
            case 2:
                minSpeed = 10f;
                maxSpeed = 14f;
                break;
            case 3:
                minSpeed = 12f;
                maxSpeed = 16f;
                break;
            case 4:
                minSpeed = 16f;
                maxSpeed = 18f;
                break;
        }

        speed = Random.Range(minSpeed, maxSpeed);
        startPosY = Random.Range(-4f, 4f);
        transform.position = new Vector3(10f, startPosY, 0);
        destroyed = false;
    }

    void Update()
    {
        timer += Time.deltaTime;

        if (!destroyed)
            transform.position = Vector3.MoveTowards(transform.position, GameObject.FindGameObjectWithTag("Player").transform.position, speed * Time.deltaTime);
        else
            transform.position += Vector3.down * 0.1f;

        if (transform.position.x < -14f || transform.position.x > 12f || timer > 2f)
            Destroy(gameObject);
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        destroyed = true;
        _anim.SetBool("Destroyed", true);
        Destroy(gameObject.GetComponent<CircleCollider2D>());
        _gameManager.PlusScore(10);
        _audio.clip = destroyAudio;
        _audio.Play();
    }
}