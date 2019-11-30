using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static float musicVolume = 1;
    public static float soundEffectsVolume = 1;

    public static float activityFloat = 1;
    public static float globalSpeed = 2;
    public static bool gameActive = false;

    public static bool adUsed = false;

    public static bool newHighScore = false;
    public static string username;
    public static int scoreToBeAdded = 0;
    public static int score = 0;
    public static int highScore = 0;

    public static void SaveScore()
    {
        if (score > highScore)
        {
            highScore = score;
            PlayerPrefs.SetInt("highScore", highScore);
            PlayerPrefs.SetInt("NewHighScore", 1);
            PlayerPrefs.Save();
            newHighScore = true;
        }
        score = 0;
    }
    public static void saveUsername()
    {
        PlayerPrefs.SetString("username", username);
        PlayerPrefs.Save();
    }
    public static void loadGame()
    {
        if (PlayerPrefs.GetFloat("musicVolume") != 0)
        {
            if (PlayerPrefs.GetFloat("musicVolume") == 2)
            {
                musicVolume = 0;
            }
            else
            {
                musicVolume = PlayerPrefs.GetFloat("musicVolume");
            }
        }

        if (PlayerPrefs.GetFloat("soundEffectsVolume") != 0)
        {
            if (PlayerPrefs.GetFloat("soundEffectsVolume") == 2)
            {
                soundEffectsVolume = 0;
            }
            else
            {
                soundEffectsVolume = PlayerPrefs.GetFloat("soundEffectsVolume");
            }
        }

        if (PlayerPrefs.GetInt("highScore") > highScore)
        {
            highScore = PlayerPrefs.GetInt("highScore");
        }
        if (PlayerPrefs.GetInt("NewHighScore") != 0)
        {
            newHighScore = true;
        }
        if (PlayerPrefs.GetString("username") != null)
        {
            username = PlayerPrefs.GetString("username");
        }
    }
    

}
