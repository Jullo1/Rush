using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Meteor : MonoBehaviour
{
    SpriteRenderer _sr;
    AudioSource _audio;
    GameObject _game;
    GameManager _gameManager;
    GameObject _playerObject;
    Player _player;

    public AudioClip destroyAudio;

    public float speedX;
    public float speedY;
    float startPosY;
    float minSpeedX;
    float maxSpeedX;

    bool destroyed;

    void Awake()
    {
        _game = GameObject.FindGameObjectWithTag("GameController");
        _gameManager = _game.GetComponent<GameManager>();
        _playerObject = GameObject.FindGameObjectWithTag("Player");
        _player = _playerObject.GetComponent<Player>();
        _sr = GetComponent<SpriteRenderer>();
        _audio = GetComponent<AudioSource>();
    }

    void Start()
    {
        switch (_gameManager.stage)
        {
            case 1:
                minSpeedX = -8f;
                maxSpeedX = -10f;
                break;
            case 2:
                minSpeedX = -10f;
                maxSpeedX = -12f;
                break;
            case 3:
                minSpeedX = -12f;
                maxSpeedX = -14f;
                break;
            case 4:
                minSpeedX = -14f;
                maxSpeedX = -16f;
                break;
        }

        speedX = Random.Range(minSpeedX, maxSpeedX);
        speedY = 0;
        startPosY = Random.Range(-2f, 2f);
        transform.position = new Vector3(10f, startPosY, 0);
        destroyed = false;
    }

    void Update()
    {
        if (destroyed)
            transform.position -= new Vector3(Time.deltaTime * speedX, Time.deltaTime * speedY, 0);
        else
            transform.position += new Vector3(Time.deltaTime * speedX, Time.deltaTime * speedY, 0);

        if (transform.position.x < -14f || transform.position.x > 12f)
            Destroy(gameObject);

        transform.Rotate(Vector3.forward * 100f * Time.deltaTime); 

        if (PlayerPrefs.GetInt("Difficulty") == 3)
        {
            if (!destroyed)
                transform.localScale += Vector3.one * Time.deltaTime * 0.75f;
            else
                transform.localScale -= Vector3.one * Time.deltaTime * 6f;
        }
        else if (PlayerPrefs.GetInt("Difficulty") == 2)
        {
            if (destroyed)
                transform.localScale -= Vector3.one * Time.deltaTime * 4f;
        }

        if (transform.localScale.x < 0f)
            Destroy(gameObject);
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        destroyed = true;
        _sr.color = Color.red;
        _gameManager.PlusScore(5); //+5 for each collision

        if (transform.position.x <= 10f) //meteor on screen
        {
            _audio.clip = destroyAudio;
            _audio.Play();
        }

        //destroyed in intro instead of bounce, prevents bug
        if (collision.collider.GetComponent<Player>() && _player.introActive)
        {
            Destroy(gameObject.GetComponent<SpriteRenderer>());
            Destroy(gameObject.GetComponent<CircleCollider2D>());
        }
    }
}
