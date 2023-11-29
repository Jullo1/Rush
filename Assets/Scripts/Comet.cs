using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Comet : MonoBehaviour
{
    AudioSource _audio;

    GameObject _game;
    GameManager _gameManager;
    SpriteRenderer _sr;

    public AudioClip destroyAudio;

    public float speedX;
    public float speedY;
    float startPosY;
    float minSpeedX;
    float maxSpeedX;

    void Awake()
    {
        _game = GameObject.FindGameObjectWithTag("GameController");
        _gameManager = _game.GetComponent<GameManager>();
        _audio = GetComponent<AudioSource>();
        _sr = GetComponent<SpriteRenderer>();
    }

    void Start()
    {
        switch (_gameManager.stage)
        {
            case 1:
                minSpeedX = -12f;
                maxSpeedX = -16f;
                break;
            case 2:
                minSpeedX = -16f;
                maxSpeedX = -20f;
                break;
            case 3:
                minSpeedX = -20f;
                maxSpeedX = -24f;
                break;
            case 4:
                minSpeedX = -22f;
                maxSpeedX = -24f;
                break;
        }

        speedX = Random.Range(minSpeedX, maxSpeedX);
        speedY = Random.Range(-3f, 3f);
        startPosY = Random.Range(-2f, 2f);
        transform.position = new Vector3(10f, startPosY, 0);
    }

    void Update()
    {
        transform.position += new Vector3(Time.deltaTime * speedX, Time.deltaTime * speedY, 0);

        if (transform.position.x < -20f || transform.position.x > 12f)
            Destroy(gameObject);

        transform.Rotate(Vector3.forward * 200f * Time.deltaTime);
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        _sr.color = Color.red;
        speedX -= 8f;
        Destroy(gameObject.GetComponent<CircleCollider2D>());
        _gameManager.PlusScore(5);
        _audio.clip = destroyAudio;
        _audio.Play();
    }
}
