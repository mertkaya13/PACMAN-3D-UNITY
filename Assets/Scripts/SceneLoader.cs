using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public static class SceneLoader
{
    public enum Scene
    {
        GameScene,
        MainMenu,
    }
    private static Scene targetScene;
    public static void Load(Scene scene)
    {
        SceneManager.LoadScene(Scene.GameScene.ToString());
        targetScene = scene;
    }
    public static void LoadTargetScene()
    {
        SceneManager.LoadScene(targetScene.ToString());
    }

}
