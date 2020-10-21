﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ScoreCanvas : MonoBehaviour
{
    // Start is called before the first frame update

    public Text scoreText;
    private static ScoreCanvas instance;
    void Start()
    {
        instance = this;
    }
    public static ScoreCanvas getInstance()
    {
        return instance;
    }

    // Update is called once per frame
    void Update()
    {
        scoreText.text = "Score : " + PlayerController.getInstance().getCurrentScore();
    }
}
