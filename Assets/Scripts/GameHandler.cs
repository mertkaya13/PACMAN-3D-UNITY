using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameHandler : MonoBehaviour
{
    // Start is called before the first frame update

    public GameObject gameOverWindow;
    void Start()
    {
        PlayerController.getInstance().OnDied += OnGameEnd_OnDied;
        ScoreCanvas.getInstance().gameObject.SetActive(true);
    }
    private void Awake()
    {
        gameOverWindow.SetActive(false);
    }

    private void OnGameEnd_OnDied(object sender, EventArgs e)
    {
        gameOverWindow.SetActive(true);
    }


    // Update is called once per frame
    void Update()
    {
        
    }
}
