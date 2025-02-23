using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;
using Car;

public class ResumePanel : MonoBehaviour
{
    public GameObject resumePanel;
    public GameObject GameBoard;
    public TMP_Text Score;
    public TMP_Text HighScore;
    public CarController car;

    void Start()
    {
        resumePanel.SetActive(false);
        GameBoard.SetActive(true);
      
    }
    public void OpenResumePanel()
    {
        resumePanel.SetActive(true);
        Time.timeScale=0;
        car.audioSource.Stop();
        GameBoard.SetActive(false);
    }
    public void closeResumePanel()
    {
        resumePanel.SetActive(false);
        Time.timeScale=1f;
        if(PlayerPrefs.GetInt("SoundOn")==1){
        car.audioSource.Play();}
        GameBoard.SetActive(true);
    }
    public void Retry()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        Time.timeScale=1f;
        GameBoard.SetActive(true);

    }
    public void ReturnMainMenu()
    {
        SceneManager.LoadScene("Lobby");
        Time.timeScale=1f;
    }
    private void Update()
    {
        Score.text=car.distanceTraveled.ToString("0");
        HighScore.text=PlayerPrefs.GetInt("HighScore").ToString("0");
    }
    public void Resume()
    {
        resumePanel.SetActive(false );
        Time.timeScale = 1f;
        if(PlayerPrefs.GetInt("SoundOn")==1){
        car.audioSource.Play();}
        GameBoard.SetActive(true);
    }

}
