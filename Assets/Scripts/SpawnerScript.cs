using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using UnityEngine.Advertisements;

public class SpawnerScript : MonoBehaviour
{
    public GameObject[] obstacles;
    public GameObject gameOverPanel;
    public GameObject pausePanel;

    platformScript plat;
    private PlayerScript player;


    private void Start()
    {
        GameManager.adUsed = false;
        plat = GameObject.Find("Platform").GetComponent<platformScript>();
        Advertisement.Initialize("1703491", true);
        player = GameObject.Find("Player").GetComponent<PlayerScript>();
    }
    public void makeNewObstacle()
    {
        Instantiate(obstacles[Random.Range(0, obstacles.Length)], new Vector3(-2.53f, 8, transform.position.z), Quaternion.identity);
    }
    public void makeFirstObstacle()
    {
        Instantiate(obstacles[Random.Range(0, obstacles.Length)], new Vector3(-2.53f, 5, transform.position.z), Quaternion.identity);
    }
    public IEnumerator TransitionToGameOver()
    {
        yield return new WaitForSeconds(1f);
    }
    public void endGame()
    {
        gameOverPanel.GetComponent<Animator>().SetBool("TransitionIn", true);
        GameObject.Find("ButtonManager").GetComponent<buttonManager>().playWhoosh();
        Debug.Log("GameOver");
    }
    public void restart()
    {
        SceneManager.LoadScene("GameScene");
    }

    public void continueGame()
    {
        plat.startMyCour(true);
        gameOverPanel.GetComponent<Animator>().SetBool("TransitionIn", false);
        GameObject.Find("ButtonManager").GetComponent<buttonManager>().playWhoosh();
    }





    public void ShowRewardedAd()
    {
        if (Advertisement.IsReady("rewardedVideo") && !GameManager.adUsed)
        {
            var options = new ShowOptions { resultCallback = HandleShowResult };
            Advertisement.Show("rewardedVideo", options);
        }
    }

    private void HandleShowResult(ShowResult result)
    {
        switch (result)
        {
            case ShowResult.Finished:
                Debug.Log("The ad was successfully shown.");

                continueGame();
                GameManager.adUsed = true;
                break;
            case ShowResult.Skipped:
                Debug.Log("The ad was skipped before reaching the end.");
                restart();
                break;
            case ShowResult.Failed:
                Debug.LogError("The ad failed to be shown.");
                restart();
                break;
        }
    }
}
