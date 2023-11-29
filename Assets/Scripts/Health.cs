using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Health : MonoBehaviour
{
    AudioSource _audio;
    public AudioClip destroyAudio;

    public float speedY;
    float speedX;
    float startPosX;

    void Awake()
    {
        _audio = GetComponent<AudioSource>();
    }

    void Start()
    {
        speedY = Random.Range(-6f, -8f);
        speedX = Random.Range(-3f, -5f);
        startPosX = Random.Range(6f, 8f);
        transform.position = new Vector3(startPosX, 6f, 0);
    }

    void Update()
    {
        transform.position += new Vector3(Time.deltaTime * speedX, Time.deltaTime * speedY, 0);

        if (transform.position.y < -7f || transform.position.y > 7f)
        {
            Destroy(gameObject);
        }

        transform.Rotate(Vector3.forward * 100f * Time.deltaTime);
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        Destroy(gameObject.GetComponent<SpriteRenderer>());
        Destroy(gameObject.GetComponent<CircleCollider2D>());
        Destroy(gameObject.GetComponent<Rigidbody2D>());
        _audio.clip = destroyAudio;
        _audio.Play();
    }
}