using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class UIScript : MonoBehaviour
{
    private PlayerScript player;
    public Text scoreText;
    public Text highScoreDisplay;

    public Slider musicVolume;
    public Slider soundEffectsVolume;
    
    // Start is called before the first frame update
    void Start()
    {
        musicVolume.value = GameManager.musicVolume;
        soundEffectsVolume.value = GameManager.soundEffectsVolume;

        if (musicVolume.value != 0)
        {
            PlayerPrefs.SetFloat("musicVolume", musicVolume.value);
        }
        else
        {
            PlayerPrefs.SetFloat("musicVolume", 2);
        }

        if (soundEffectsVolume.value != 0)
        {
            PlayerPrefs.SetFloat("soundEffectsVolume", soundEffectsVolume.value);
        }
        else
        {
            PlayerPrefs.SetFloat("soundEffectsVolume", 2);
        }

        if (SceneManager.GetActiveScene().name == "GameScene")
        {
            player = GameObject.Find("Player").GetComponent<PlayerScript>();

            if (GameManager.username != "" && GameObject.Find("InputField").GetComponent<InputField>().text != GameManager.username)
            {
                GameObject.Find("InputField").GetComponent<InputField>().text = GameManager.username;
            }      
        }
         
        
    }

    // Update is called once per frame
    void Update()
    {
        if (SceneManager.GetActiveScene().name == "GameScene")
        {
            if (highScoreDisplay.text != "Score " + GameManager.score)
            {
                highScoreDisplay.text = "Score " + GameManager.score;
            }
        }

            if (GameManager.scoreToBeAdded > 0)
        {
            GameManager.score += 1;
            GameManager.scoreToBeAdded -= 1;
            scoreText.text = "" + GameManager.score;
        }
    }
    public void mouseIsTouching()
    {
        player.notTouchingUI = false;
    }
    public void mousenotTouching()
    {
        player.notTouchingUI = true;
    }
}
