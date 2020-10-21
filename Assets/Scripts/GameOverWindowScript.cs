using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameOverWindowScript : MonoBehaviour
{
    public Button restartButton;
    public Button menuButton;
    public Text highscoreText;
    public Text yourScoreText;
    public PlayerController controller;
    // Start is called before the first frame update
    void Start()
    {
        restartButton.onClick.AddListener(restartButtonClicked);
        menuButton.onClick.AddListener(menuButtonClicked);
    }

    private void menuButtonClicked()
    {
        SoundManager.getInstance().playButtonSound(true);
        SceneLoader.Load(SceneLoader.Scene.MainMenu);
        SceneManager.LoadScene(0);
        Debug.Log("MainMenu");
    }

    private void Awake()
    {
        controller.OnDied += OnDied_GameOverWindow;
    }

    private void restartButtonClicked()
    {
        SoundManager.getInstance().playButtonSound(true);
        SceneLoader.Load(SceneLoader.Scene.GameScene);
    }

    private void OnDied_GameOverWindow(object sender, EventArgs e)
    {
        ScoreCanvas.getInstance().gameObject.SetActive(false);
        SoundManager.getInstance().playMenuSound(true);
        int score = PlayerController.getInstance().currentScore;
        yourScoreText.text = "Your Score : " + score;
        if (score >= Score.getHighScore())
        {
            //New High
            highscoreText.text = "NEW HIGHSCORE! : " + score;
        }
        else
        {
            highscoreText.text = "HIGHSCORE : " + Score.getHighScore();
        }
    }

    // Update is called once per frame
    void Update()
    {
    }
}
