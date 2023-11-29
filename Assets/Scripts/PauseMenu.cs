using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PauseMenu : MonoBehaviour
{
    public RectTransform bardockImage;

    AudioSource _audio;
    public AudioClip hoverAudio;

    private void Awake()
    {
        _audio = gameObject.GetComponent<AudioSource>();
    }

    public void OnResumeHover()
    {
        bardockImage.anchoredPosition = new Vector3(0, 0, 0);
        _audio.clip = hoverAudio;
        _audio.Play();
    }

    public void OnRestartHover()
    {
        bardockImage.anchoredPosition = new Vector3(-15f, -100f, 0);
        _audio.clip = hoverAudio;
        _audio.Play();
    }
}
