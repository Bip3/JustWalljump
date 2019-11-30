using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MusicManager : MonoBehaviour
{
    AudioSource myAudio;

    void Update()
    {
        myAudio.volume = GameManager.musicVolume;
    }

    void Awake()
    {
        myAudio = GetComponent<AudioSource>();

        GameObject[] objs = GameObject.FindGameObjectsWithTag("ButtonManager");

        if (objs.Length > 1)
        {
            Destroy(this.gameObject);
        }

        DontDestroyOnLoad(this.gameObject);

    }
}
