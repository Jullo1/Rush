using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    Scene _scene;
    Generator _generator;
    Player _player;
    AudioSource audioS;
    public GameObject pauseCanvas;
    public AudioClip start;
    public Text scoreText;
    public Text stageText;
    public Image stageImage;
    public GameObject stageUpText;

    public int stage; 
    float _stageDuration;
    int _previousStageDuration;
    public int score;
    public bool pauseStatus;

    public void GameStart()
    {
        SceneManager.LoadScene("Difficulty");
        audioS.clip = start;
        audioS.Play();

        if (pauseStatus) //checks if the game is starting on pause
            Pause();
    }

    public void TutorialStart()
    {
        SceneManager.LoadScene("Tutorial");
        audioS.clip = start;
        audioS.Play();

        if (pauseStatus) //checks if the game is starting on pause
            Pause();
    }

    public void Pause()
    {
        if (_scene.name != "Tutorial")
            if (!pauseStatus)
            {
                Time.timeScale = 0;
                pauseStatus = true;
                pauseCanvas.SetActive(true);
            }
            else
            {
                Time.timeScale = 1;
                pauseStatus = false;
                pauseCanvas.SetActive(false);
            }
    }

    public void PlusScore(int plusScore)
    {
        score += plusScore;
    }

    void Awake()
    {
        audioS = GetComponent<AudioSource>();
        _generator = GetComponent<Generator>();
        _player = GameObject.FindObjectOfType<Player>();
        _scene = SceneManager.GetActiveScene();
    }

    void Start()
    {
        score = 0;
        stage = 1;
        _stageDuration = 100;
        _previousStageDuration = 0;
        PlayerPrefs.SetInt("FinalScore", 0);
    }

    void Update()
    {
        RefreshUI();

        if (Input.GetKeyDown(KeyCode.Escape) && _scene.name != "Tutorial" && _scene.name != "Menu" && _scene.name != "Difficulty")
            Pause();

        if ((score >= 100 && stage == 1))
            StageUp1();
        else if (score >= 250 && stage == 2)
            StageUp2();
        else if (score >= 700 && stage == 3)
            StageUp3();


        if (Input.GetKeyDown(KeyCode.F1) && !_player.introActive && !_player.introActive2 && !pauseStatus && _scene.name == "1")
        {
            switch (stage)
            {
                case 1:
                    score = 100;
                    break;
                case 2:
                    score = 250;
                    break;
                case 3:
                    score = 700;
                    break;
            }
        }
    }

    public void ExitTutorial()
    {
        SceneManager.LoadScene("0");
    }

    void RefreshUI()
    {
        if (_scene.name != "Tutorial")
        {
            scoreText.text = score.ToString();
            stageText.text = stage.ToString();
            PlayerPrefs.SetInt("FinalScore", score); //setting hiscore for next scene
            if (stage < 4)
                stageImage.fillAmount = (score - _previousStageDuration) / _stageDuration;
            else
                stageImage.transform.localScale = Vector3.zero;
        }
    }

    void StageUp1()
    {
        if (!_player.isTakingDamage)
            _player.introActive2 = true;

        stage++;
        _stageDuration = 2500;
        _previousStageDuration = 1000;
        _generator.minSpawnTime = 0.75f;
        _generator.maxSpawnTime = 1f;

        StartCoroutine(StageUpText());
    }

    void StageUp2()
    {
        if (!_player.isTakingDamage)
            _player.introActive2 = true;

        stage++;
        _stageDuration = 7000;
        _previousStageDuration = 2500;
        _generator.minSpawnTime = 0.5f;
        _generator.maxSpawnTime = 0.75f;

        StartCoroutine(StageUpText());
    }
    void StageUp3()
    {
        if (!_player.isTakingDamage)
            _player.introActive2 = true;

        stage++;
        _previousStageDuration = 7000;
        _generator.minSpawnTime = 0.4f;
        _generator.maxSpawnTime = 0.6f;

        StartCoroutine(StageUpText());
    }

    IEnumerator StageUpText()
    {
        stageUpText.SetActive(true);
        yield return new WaitForSeconds(0.05f);
        stageUpText.SetActive(false);
        yield return new WaitForSeconds(0.05f);
        stageUpText.SetActive(true);
        yield return new WaitForSeconds(0.05f);
        stageUpText.SetActive(false);
        yield return new WaitForSeconds(0.05f);
        stageUpText.SetActive(true);
        yield return new WaitForSeconds(2f);
        stageUpText.SetActive(false);
    }

    public void QuitGame()
    {
        Application.Quit();
    }
}
