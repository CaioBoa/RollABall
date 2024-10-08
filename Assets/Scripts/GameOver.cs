using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;

public class GameOver : MonoBehaviour
{
    public GameObject TimeTextObject;
    public TextMeshProUGUI TimeText;
    public TextMeshProUGUI GameOverText;
    public GameObject ScoreTextObject;
    public TextMeshProUGUI ScoreText;
    public AudioSource WinAudio;
    public AudioSource LoseAudio;
    void Start()
    {
        if (PlayerPrefs.GetInt("GameOver") == 0)
        {
            GameOverText.text = "Game Over!";
            TimeTextObject.SetActive(false);
            ScoreTextObject.SetActive(true);
            ScoreText.text = "Score: " + PlayerPrefs.GetInt("Score").ToString();
            LoseAudio.Play();
        }
        else
        {
            GameOverText.text = "You Won!";
            int Minutes = Mathf.FloorToInt(PlayerPrefs.GetFloat("Time") / 60F);
            int Seconds = Mathf.FloorToInt(PlayerPrefs.GetFloat("Time") - Minutes * 60);
            TimeText.text = Minutes.ToString() + ":" + Seconds.ToString();
            TimeTextObject.SetActive(true);
            ScoreTextObject.SetActive(false);
            WinAudio.Play();
        }
    }

    public void PlayAgain()
    {
        SceneManager.LoadScene("Minigame");
    }

    public void MainMenu()
    {
        SceneManager.LoadScene("MainMenu");
    }

}
