using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class buttonManager : MonoBehaviour
{
    public static bool gameLoaded = false;
    public AudioClip uiWhoosh;
    public AudioSource myAudio;

    void Awake()
    {
        Application.targetFrameRate = 60;
        myAudio = GetComponent<AudioSource>();
        if (SceneManager.GetActiveScene().name == "StartScene" && !gameLoaded)
        {
            GameManager.loadGame();
            gameLoaded = true;
        }
        GameObject[] objs = GameObject.FindGameObjectsWithTag("ButtonManager");

        if (objs.Length > 1)
        {
            Destroy(this.gameObject);
        }

        DontDestroyOnLoad(this.gameObject);
        
    }
    public void playWhoosh()
    {
        myAudio.PlayOneShot(uiWhoosh);
    }
    public void restartGame()
    {
        GameManager.SaveScore();
        GameManager.adUsed = false;
        SceneManager.LoadScene("GameScene");
    }
    public void saveUsername()
    {
        GameManager.username = GameObject.Find("InputField").GetComponent<InputField>().text;
        GameManager.saveUsername();
    }
    public void backToMenu()
    {
        SceneManager.LoadScene("StartScene");
    }
    public void pauseGame()
    {
        playWhoosh();
        if(GameObject.FindGameObjectWithTag("Player").GetComponent<BoxCollider2D>().isActiveAndEnabled)
        {
            if (Time.timeScale == 0)
            {
                GameObject.Find("PauseMenu").GetComponent<Animator>().SetBool("TransitionIn", false);
                Time.timeScale = 1;
            }
            else
            {
                GameObject.Find("PauseMenu").GetComponent<Animator>().SetBool("TransitionIn", true);
                Time.timeScale = 0;

            }
        }
        
        
    }
    public void startPauseGame()
    {
        pauseGame();
    }
    public void loadLeaderboard()
    {
        GameManager.SaveScore();
        SceneManager.LoadScene("Leaderboard");
    }
    public void Options()
    {
        SceneManager.LoadScene("Options");
    }
    public void BackToStart()
    {
        SceneManager.LoadScene("StartScene");
    }
    private void Update()
    {
        myAudio.volume = GameManager.soundEffectsVolume;

        if (Input.GetKeyDown(KeyCode.Escape))
        {
            startPauseGame();
        }
        if (SceneManager.GetActiveScene().name == "GameScene")
        {
            if (GameManager.adUsed)
            {
                GameObject.Find("AdButton").GetComponent<Button>().interactable = false;
            }
            else
            {
                GameObject.Find("AdButton").GetComponent<Button>().interactable = true;
            }

        }
    }
    public void saveVolume(int type)
    {
        if (type == 1)
        {
            GameManager.musicVolume = GameObject.Find("UIManager").GetComponent<UIScript>().musicVolume.GetComponent<Slider>().value;

            if (GameObject.Find("UIManager").GetComponent<UIScript>().musicVolume.GetComponent<Slider>().value == 0)
            {
                PlayerPrefs.SetFloat("musicVolume", GameManager.musicVolume);
            }
        }
        if (type == 2)
        {
            GameManager.soundEffectsVolume = GameObject.Find("UIManager").GetComponent<UIScript>().soundEffectsVolume.GetComponent<Slider>().value;

            if (GameObject.Find("UIManager").GetComponent<UIScript>().soundEffectsVolume.GetComponent<Slider>().value == 0)
            {
                PlayerPrefs.SetFloat("soundEffectsVolume", GameManager.soundEffectsVolume);
            }
        }
    }

}
