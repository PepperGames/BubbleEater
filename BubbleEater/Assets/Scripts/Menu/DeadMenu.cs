using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class DeadMenu : MonoBehaviour
{
    public AudioSource ambientAudioSource;
    public AudioSource clickAudioSource;
    private float bestScore = 0f;
    private float score = 0f;
    public Text ScoreText;
    public Text BestScoreText;
    private void Awake()
    {
        
        if (PlayerPrefs.HasKey("SaveScore"))
        {
            score = PlayerPrefs.GetFloat("SaveScore");
        }

        if (PlayerPrefs.HasKey("SaveBestScore"))
        {
            bestScore = PlayerPrefs.GetFloat("SaveBestScore");
        }

        ScoreText.text = $"Score: {System.Math.Round(score,1)}";
        BestScoreText.text = $"Best Score: {System.Math.Round(bestScore, 1)}";
    }
    private void Start()
    {
        ambientAudioSource.Play();
    }
    public void PlayClick()
    {
        clickAudioSource.Play();
        Invoke(nameof(Play), 0.5f);
    }
    public void ToMainMenuClick()
    {
        clickAudioSource.Play();
        Invoke(nameof(ToMainMenu), 0.5f);
    }
    public void Play()
    {
        SceneManager.LoadScene("Game");
    }
    public void ToMainMenu()
    {
        SceneManager.LoadScene("MainMenuScene");
    }
}
