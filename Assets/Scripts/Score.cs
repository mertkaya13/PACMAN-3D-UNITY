using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class Score
{

    public static void Start()
    {
        PlayerController.getInstance().OnDied += scoreOnDied;
    }

    private static void scoreOnDied(object sender, EventArgs e)
    {
        TrySetNewHighscore(PlayerController.getInstance().currentScore);
    }

    public static int getHighScore()
    {
        return PlayerPrefs.GetInt("highscore");
    }

    public static bool TrySetNewHighscore(int score)
    {
        int currenthighscore = getHighScore();
        if (score > currenthighscore)
        {
            PlayerPrefs.SetInt("highscore", score);
            PlayerPrefs.Save();
            return true;
        }
        else
        {
            return false;
        }
    }
}