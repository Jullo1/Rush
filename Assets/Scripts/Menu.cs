using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SocialPlatforms.Impl;
using UnityEngine.UI;

public class Menu : MonoBehaviour
{
    public GameObject[] controlsItems = new GameObject[4];
    public GameObject[] enemiesItems = new GameObject[4];
    public Sprite selected;
    public Sprite unselected;

    public Image controlsImg;
    public Image enemiesImg;
    public GameManager game;

    public void ShowControls()
    {
        for (int i = 0; i < 4; i++)
        {
            controlsItems[i].gameObject.SetActive(true);
        }
        controlsImg.sprite = selected;
        game.GetComponent<AudioSource>().Play();
    }
    public void HideControls()
    {
        for (int i = 0; i < 4; i++)
        {
            controlsItems[i].gameObject.SetActive(false);
        }
        controlsImg.sprite = unselected;
    }

    public void ShowEnemies()
    {
        for (int i = 0; i < 4; i++)
        {
            enemiesItems[i].gameObject.SetActive(true);
        }
        enemiesImg.sprite = selected;
        game.GetComponent<AudioSource>().Play();
    }
    public void HideEnemies()
    {
        for (int i = 0; i < 4; i++)
        {
            enemiesItems[i].gameObject.SetActive(false);
        }
        enemiesImg.sprite = unselected;
    }
}
