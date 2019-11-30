using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WallCheck : MonoBehaviour
{
    private PlayerScript player;

    private void Start()
    {
        player = GetComponentInParent<PlayerScript>();
    }
    private void Update()
    {
        if (player.GetComponent<BoxCollider2D>().enabled == false)
        {
            GetComponent<BoxCollider2D>().enabled = false;
        }
        else
        {
            GetComponent<BoxCollider2D>().enabled = true;
        }
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (!collision.CompareTag("Player"))
        {
            player.canJump = true;
        }

        if (collision.gameObject.CompareTag("plat"))
        {
            if (collision.gameObject.GetComponent<platformScoreIncreaserClass>().hit == false)
            {
                GameManager.scoreToBeAdded += 10;
                collision.gameObject.GetComponent<platformScoreIncreaserClass>().hit = true;
            }
            
        }
        if (collision.gameObject.CompareTag("plat") || collision.gameObject.CompareTag("wall"))
        {
            player.spawnDustClouds();
            player.musicPlayer(player.hitWall);
        }

    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (!collision.CompareTag("Player") && (collision.gameObject.CompareTag("plat") || collision.gameObject.CompareTag("wall")))
        {
            player.clinging = false;
        }
    }
    private void OnTriggerStay2D(Collider2D collision)
    {
        if (!collision.CompareTag("Player") && (collision.gameObject.CompareTag("plat") || collision.gameObject.CompareTag("wall")))
        {
            player.clinging = true;
        }
    }
}
