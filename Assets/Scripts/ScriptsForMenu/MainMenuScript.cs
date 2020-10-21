using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MainMenuScript : MonoBehaviour
{
    public Button playButton;
    public Button quitButton;
    // Start is called before the first frame update
    void Start()
    {
        playButton.onClick.AddListener(playButtonMethod);
        quitButton.onClick.AddListener(quitButtonMethod);
        SoundManager.getInstance().playMenuSound(true);
    }

    private void quitButtonMethod()
    {
        SoundManager.getInstance().playButtonSound(true);
        Application.Quit();
    }

    private void playButtonMethod()
    {
        SoundManager.getInstance().playButtonSound(true);
        SceneLoader.Load(SceneLoader.Scene.GameScene);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
