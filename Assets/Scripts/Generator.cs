using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Generator : MonoBehaviour
{
    public GameObject meteor;
    public GameObject comet;
    public GameObject health;
    public GameObject enemy;

    GameManager game;

    public bool generatorOn;
    public float timer;
    float _spawnTime;
    int _type;
    public float minSpawnTime;
    public float maxSpawnTime;

    int difficulty;

    void Awake()
    {
        game = GameObject.FindGameObjectWithTag("GameController").GetComponent<GameManager>();
    }

    void Start()
    {
        _spawnTime = 8f;
        minSpawnTime = 1f;
        maxSpawnTime = 1.5f;
        generatorOn = true;
        difficulty = PlayerPrefs.GetInt("Difficulty");
    }

    void Update()
    {
        timer += Time.deltaTime;
        if (timer >= _spawnTime && generatorOn)
        {
            timer = 0f;

            switch (game.stage)
            {
                default:
                    _type = Random.Range(1, 6);
                    break;
                case 1:
                    _type = Random.Range(3, 7);
                    break;
                case 2:
                    _type = Random.Range(2, 7);
                    break;
            }

            switch (_type)
            {
                default:
                    Instantiate(meteor);
                    _spawnTime = Random.Range(minSpawnTime, maxSpawnTime);
                    break;

                case 1:
                    Instantiate(enemy);
                    _spawnTime = Random.Range(minSpawnTime, maxSpawnTime);
                    break;
                case 2:
                    if (difficulty != 1)
                        Instantiate(comet);
                    _spawnTime = Random.Range(minSpawnTime, maxSpawnTime);
                    break;
                case 3:
                    if (difficulty == 1)
                    {
                        Instantiate(health);
                    }
                    else if (difficulty == 2)
                    {
                        float randomNumber = Random.Range(0f, 1f);
                        if (randomNumber > 0.4f)
                            Instantiate(health);
                        else
                            Instantiate(enemy);
                    }
                    else if (difficulty == 3)
                    {
                        float randomNumber = Random.Range(0f, 1f);
                        if (randomNumber > 0.6f)
                            Instantiate(health);
                        else
                            Instantiate(enemy);
                    }

                    _spawnTime = Random.Range(minSpawnTime, maxSpawnTime);
                    break;
            }
        }
    }
}
