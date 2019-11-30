using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class platformScript : MonoBehaviour
{
    SpriteRenderer mySprite;
    Text countDownText;
    GameObject player;
    // Start is called before the first frame update
    void Start()
    {
        countDownText = GameObject.Find("countDownText").GetComponent<Text>();
        mySprite = GetComponent<SpriteRenderer>();
        StartCoroutine(startGame(false));
        Time.timeScale = 1;
        player = GameObject.FindGameObjectWithTag("Player");
    }
    public void startMyCour(bool restarting)
    {
        StartCoroutine(startGame(restarting));
    }
    public IEnumerator startGame(bool restarting)
    {
        if (restarting)
        {
            GameObject[] obstaclesArray = GameObject.FindGameObjectsWithTag("Obstacles");
            GameObject lowestPlatform;
            lowestPlatform = obstaclesArray[0];
            for (int i = 0; i < obstaclesArray.Length; i++)
            {
                if (obstaclesArray[i].transform.position.y < lowestPlatform.transform.position.y)
                {
                    lowestPlatform = obstaclesArray[i];
                }
            }
            float addY;
            addY = 6.5f - lowestPlatform.transform.position.y;
            for (int i = 0; i < obstaclesArray.Length; i++)
            {
                obstaclesArray[i].transform.position = new Vector3(obstaclesArray[i].transform.position.x, obstaclesArray[i].transform.position.y + addY, obstaclesArray[i].transform.position.z);
            }
            GetComponent<BoxCollider2D>().enabled = true;
            mySprite.color = new Color(mySprite.color.r, mySprite.color.g, mySprite.color.b, 1);
            player.GetComponent<PlayerScript>().resetMe();
        }
        if (!restarting)
        {
            GameManager.score = 0;
            GameManager.scoreToBeAdded = 0;
            GameObject.Find("Spawner").GetComponent<SpawnerScript>().makeFirstObstacle();
        }
        countDownText.color = new Color(countDownText.color.r, countDownText.color.g, countDownText.color.b, 1f);
        countDownText.text = "3";
        yield return new WaitForSeconds(1);
        countDownText.text = "2";
        yield return new WaitForSeconds(1);
        countDownText.text = "1";
        
        player.GetComponent<PlayerScript>().canJump = true;
        GameManager.gameActive = true;
        yield return new WaitForSeconds(1);
        GetComponent<BoxCollider2D>().enabled = false;
        countDownText.text = "GO!";       
        
        while (mySprite.color.a > 0)
        {
            mySprite.color = new Color(mySprite.color.r, mySprite.color.g, mySprite.color.b, mySprite.color.a - .05f);
            countDownText.color = new Color(countDownText.color.r, countDownText.color.g, countDownText.color.b, countDownText.color.a - .05f);
            yield return new WaitForSeconds(.01f);
        }
        
    }
}
