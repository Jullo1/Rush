using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SocialPlatforms.Impl;
using UnityEngine.UI;

public class FinalScore : MonoBehaviour
{
    Text _score;
    public Text scoreText;

    void Awake()
    {
        _score = GetComponent<Text>();
    }

    void Start()
    {
        if (PlayerPrefs.GetInt("FinalScore") != 0)
        {
            switch (PlayerPrefs.GetInt("Difficulty"))
            {
                case 1:
                    PlayerPrefs.SetInt("FinalScore", PlayerPrefs.GetInt("FinalScore") * 1);
                    break;
                case 2:
                    PlayerPrefs.SetInt("FinalScore", PlayerPrefs.GetInt("FinalScore") * 2);
                    break;
                case 3:
                    PlayerPrefs.SetInt("FinalScore", PlayerPrefs.GetInt("FinalScore") * 3);
                    break;
            }
            scoreText.gameObject.SetActive(true);
            _score.text = PlayerPrefs.GetInt("FinalScore").ToString();
            StartCoroutine(SendScore(PlayerPrefs.GetInt("FinalScore")));
        }
        else scoreText.gameObject.SetActive(false);
    }

    IEnumerator SendScore(int value)
    {
        Debug.Log(value);
        WWWForm form = new WWWForm();
        form.AddField("game", "Rush");
        form.AddField("score", value);

        WWW www = new WWW("https://julianlerej.com/app/views/sendScore.php", form);
        yield return www;
    }
}
