using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tutorial : MonoBehaviour
{
    GameObject player;
    Player playerScript;

    public GameObject meteor;
    public GameObject comet;
    public GameObject health;
    public GameObject enemy;

    GameObject currentEnemy;
    bool currentKeyPressed;
    bool enemyActive;

    public List<GameObject> activeBackground = new List<GameObject>();

    float timer;
    int currentStep;

    void Awake()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        playerScript = player.GetComponent<Player>();
    }

    void Start()
    {
        timer = 0f;
        currentStep = 1;
        enemyActive = false;
        playerScript.tutorialLockPunch = true;
        playerScript.tutorialLockSpecial = true;
        playerScript.tutorialLockBlast = true;
        PlayerPrefs.SetInt("Difficulty", 1);
    }

    void Update()
    {
        timer += Time.deltaTime;

        switch (currentStep)
        {
            case 1:
                Step1();
                break;
            case 2:
                Step2();
                break;
            case 3:
                Step3();
                break;
            case 4:
                Step4();
                break;
        }

        if (enemyActive && currentStep == 1)
        {
            if (Vector3.Distance(player.transform.position, currentEnemy.transform.position) < 3.5f)
            {
                playerScript.tutorialLockPunch = false;

                if (!currentKeyPressed)
                {
                    Time.timeScale = 0f;
                }
                else
                {
                    Time.timeScale = 1f;
                    timer = 0f;
                    enemyActive = false;
                    currentStep++;
                }
            }
        }

        else if (enemyActive && currentStep == 2)
        {
            if (player.transform.position.y > currentEnemy.transform.position.y - 1f && player.transform.position.y < currentEnemy.transform.position.y + 1f && currentEnemy.transform.position.x < 7f && player.transform.position.x < currentEnemy.transform.position.x)
            {
                playerScript.tutorialLockBlast = false;

                if (!currentKeyPressed)
                {
                    Time.timeScale = 0f;
                }
                else
                {
                    Time.timeScale = 1f;
                    timer = 0f;
                    enemyActive = false;
                    currentStep++;
                }
            }
        }

        else if (enemyActive && currentStep == 3)
        {
            playerScript.tutorialLockBlast = true;

            if (Vector3.Distance(player.transform.position, currentEnemy.transform.position) < 3.5f)
            {
                playerScript.tutorialLockSpecial = false;

                if (!currentKeyPressed)
                {
                    Time.timeScale = 0f;
                }
                else
                {
                    Time.timeScale = 1f;
                    timer = 0f;
                    enemyActive = false;
                    currentStep++;
                }
            }
        }
    }

    void Step1()
    {
        if (timer >= 2f)
        {
            if (!enemyActive)
            {
                activeBackground[0].SetActive(true);
                currentEnemy = Instantiate(enemy);
                currentEnemy.transform.position = Vector3.right * 10f;
                enemyActive = true;
            }

            if (Input.GetKeyDown(KeyCode.LeftControl))
                currentKeyPressed = true;
            else
                currentKeyPressed = false;
        }
    }

    void Step2()
    {
        activeBackground[0].SetActive(false);

        if (timer >= 2f)
        {
            if (!enemyActive)
            {
                activeBackground[1].SetActive(true);
                currentEnemy = Instantiate(meteor);
                currentEnemy.transform.position = Vector3.right * 10f;
                enemyActive = true;
            }

            if (Input.GetKeyDown(KeyCode.E))
                currentKeyPressed = true;
            else
                currentKeyPressed = false;
        }

        if (currentEnemy.transform.position.x < -8.5f)
            currentEnemy.transform.position = Vector3.right * 10f;
    }

    void Step3()
    {
        activeBackground[1].SetActive(false);
        if (timer >= 2f)
        {
            if (!enemyActive)
            {
                activeBackground[2].SetActive(true);
                currentEnemy = Instantiate(enemy);
                currentEnemy.transform.position = Vector3.right * 10f;
                enemyActive = true;
            }

            if (Input.GetKeyDown(KeyCode.Q))
                currentKeyPressed = true;
            else
                currentKeyPressed = false;
        }
    }

    void Step4()
    {
        activeBackground[2].SetActive(false);
        playerScript.tutorialLockBlast = false;
        playerScript.tutorialLockSpecial = false;
    }
}
