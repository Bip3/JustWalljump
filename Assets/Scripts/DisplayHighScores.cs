using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

public class DisplayHighScores : MonoBehaviour
{
    public Text[] highscoreText;
    public HighScores highscoreManager;
    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(RefreshHighscores());
        for (int i = 0; i < highscoreText.Length; i++)
        {
            highscoreText[i].text = i + 1 + ".Fetching...";
        }

        highscoreManager = GetComponent<HighScores>();
    }

    public void OnHighscoresDownloaded(HighscoreStruct[] highscoreList)
    {
        for (int i = 0; i < highscoreText.Length; i++)
        {
            highscoreText[i].text = i + 1 + ". ";
            if (highscoreList.Length > i)
            {
                highscoreText[i].text += highscoreList[i].username + ": " + highscoreList[i].score;
            }
        }
    }

    IEnumerator RefreshHighscores()
    {
        while (true)
        {
            highscoreManager.DownloadHighscores();
            yield return new WaitForSeconds(30);
        }
    }
}
