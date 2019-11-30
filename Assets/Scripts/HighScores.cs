using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HighScores : MonoBehaviour
{
    const string privateCode = "MaNNDIC-zU6uMmmOBCtYugZw4kxqaXREu1BtfPQtNRtg";
    const string publicCode = "5ca420a13eba4d041cd26d50";
    const string webURL = "http://dreamlo.com/lb/";

    public HighscoreStruct[] highScoresList;
    static HighScores instance;
    DisplayHighScores highscoresDisplay;

    private void Awake()
    {
        instance = this;
        highscoresDisplay = GetComponent<DisplayHighScores>();
        if (GameManager.newHighScore)
        {
            AddNewHighscore(GameManager.username, GameManager.highScore);
            GameManager.newHighScore = false;
        }
    }
    public static void AddNewHighscore(string username, int score)
    {
        instance.StartCoroutine(instance.UploadNewHighscore(username, score));
    }
    IEnumerator UploadNewHighscore(string username, int score)
    {
        WWW www = new WWW(webURL + privateCode + "/add/" + WWW.EscapeURL(username) + "/" + score);
        yield return www;

        if (string.IsNullOrEmpty(www.error))
        {
            print("Upload Successful");
            DownloadHighscores();
        }
        else
        {
            print("Error uploading: " + www.error);
        }

    }

    public void DownloadHighscores()
    {
        StartCoroutine(DownloadHighscoresFromDatabase());
    }

    IEnumerator DownloadHighscoresFromDatabase()
    {
        WWW www = new WWW(webURL + publicCode + "/pipe/");
        yield return www;

        if (string.IsNullOrEmpty(www.error))
        {
            FormatHighScores(www.text);
            highscoresDisplay.OnHighscoresDownloaded(highScoresList);
        }
        else
        {
            print("Error downloading: " + www.error);
        }

    }

    void FormatHighScores(string textStream)
    {
        string[] entries = textStream.Split(new char[] { '\n' }, System.StringSplitOptions.RemoveEmptyEntries);
        highScoresList = new HighscoreStruct[entries.Length];
        for (int i = 0; i < entries.Length; i++)
        {
            string[] entryInfo = entries[i].Split(new char[] { '|' });
            string username = entryInfo[0];
            int score = int.Parse(entryInfo[1]);
            highScoresList[i] = new HighscoreStruct(username, score);
            print(highScoresList[i].username + ": " + highScoresList[i].score);
        }
    }
}
public struct HighscoreStruct
{
    public string username;
    public int score;

    public HighscoreStruct(string _username, int _score)
    {
        username = _username;
        score = _score;
    }
}
