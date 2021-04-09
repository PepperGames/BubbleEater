using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    public AudioSource ambientAudioSource;
    public AudioSource clickAudioSource;
    private void Start()
    {
        ambientAudioSource.Play();
    }
    public void PlayClick()
    {
        clickAudioSource.Play();
        Invoke(nameof(Play), 0.5f);
    }
    public void ExitClick()
    {
        clickAudioSource.Play();
        Invoke(nameof(Exit), 0.5f);
    }
    public void Play()
    {
        SceneManager.LoadScene("Game");
    }
    public void Exit()
    {
        Application.Quit();
    }
}
