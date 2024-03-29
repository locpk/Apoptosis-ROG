﻿using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class SceneFadeInOut : MonoBehaviour {

    public RawImage FadeImg;
    public float fadeSpeed;
    public bool sceneStarting = true;


    void Awake()
    {
        FadeImg.uvRect = new Rect(0, 0, Screen.width, Screen.height);
        FadeImg.SetNativeSize();
    }

    void Update()
    {
        // If the scene is starting...
        if (sceneStarting)
            // ... call the StartScene function.
            StartScene();

       
    }


    void FadeToClear()
    {
        // Lerp the colour of the image between itself and transparent.
        FadeImg.color = Color.Lerp(FadeImg.color, Color.clear, fadeSpeed * Time.deltaTime);
    }


    void FadeToBlack()
    {
        // Lerp the colour of the image between itself and black.
        FadeImg.color = Color.Lerp(FadeImg.color, Color.black, fadeSpeed * Time.deltaTime);
    }


    void StartScene()
    {
        // Fade the texture to clear.
        FadeToClear();

        // If the texture is almost clear...
        if (FadeImg.color.a <= 0.05f)
        {
            // ... set the colour to clear and disable the RawImage.
            FadeImg.color = Color.clear;
            FadeImg.enabled = false;

            // The scene is no longer starting.
            sceneStarting = false;
        }
    }


    public void EndScene()
    {
        // Make sure the RawImage is enabled.
        FadeImg.enabled = true;

        // Start fading towards black.
        FadeToBlack();

        // If the screen is almost black...
        if (FadeImg.color.a >= 0.95f)
            // ... reload the level
            Application.LoadLevel("MainScene(Dank)");
    }

    public void LoadMainLevel()
    {
        Application.LoadLevel("MainScene(Dank)");
    }
}
