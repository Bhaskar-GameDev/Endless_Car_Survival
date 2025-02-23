using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class PlayGame : MonoBehaviour
{
    public TMP_Text Highscore;
    public AudioSource start;
    public AudioSource idle;
    public GameObject LoadingText;

    public void Play()
    {
        SceneManager.LoadScene("Game");
        LoadingText.SetActive(true);
        
        
    }
    private void UpdateScore()
    {
        Highscore.text=PlayerPrefs.GetInt("HighScore").ToString();
    }
    private void Start()
    {
        LoadingText.SetActive(false);
        UpdateScore();
        if(PlayerPrefs.GetInt("SoundOn")==1)
        {
            start.Play();
            idle.Play();
        }   
    }
    private void Update()
    {
        UpdateScore();
    }
}
