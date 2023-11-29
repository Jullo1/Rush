using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class DifficultyMenu : MonoBehaviour
{
    AudioSource audioSource;
    public AudioClip gameStart;
    public Image selectionImage;
    public Sprite punchSprite;

    void Awake()
    {
        audioSource = GetComponent<AudioSource>();
    }

    //hovers
    public void NormalHover()
    {
        audioSource.Play();
        selectionImage.rectTransform.anchoredPosition = new Vector2(20f, 150f);
    }

    public void HardHover()
    {
        audioSource.Play();
        selectionImage.rectTransform.anchoredPosition = new Vector2(40f, 20f);
    }

    public void ExtremeHover()
    {
        audioSource.Play();
        selectionImage.rectTransform.anchoredPosition = new Vector2(10f, -125f);
    }

    //gamestarts
    public void NormalStart()
    {
        PlayerPrefs.SetInt("Difficulty", 1);
        StartCoroutine(StartGame());
    }

    public void HardStart()
    {
        PlayerPrefs.SetInt("Difficulty", 2);
        StartCoroutine(StartGame());
    }

    public void ExtremeStart()
    {
        PlayerPrefs.SetInt("Difficulty", 3);
        StartCoroutine(StartGame());
    }

    IEnumerator StartGame()
    {
        audioSource.clip = gameStart;
        audioSource.Play();
        selectionImage.sprite = punchSprite;
        yield return new WaitForSeconds(0.25f);
        SceneManager.LoadScene("1");
    }
}
