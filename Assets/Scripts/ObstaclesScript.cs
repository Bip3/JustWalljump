using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObstaclesScript : MonoBehaviour
{
    bool spawned = false;
    private SpawnerScript spawner;
    // Start is called before the first frame update
    void Start()
    {
        spawner = GameObject.Find("Spawner").GetComponent<SpawnerScript>();
    }

    // Update is called once per frame
    void Update()
    {
        if (transform.position.y <= .75f && !spawned)
        {
            spawned = true;
            spawner.makeNewObstacle();
        }
        if (transform.position.y <= -20)
        {
            Destroy(this.gameObject);
        }
        if (GameManager.gameActive)
        {
            transform.Translate(Vector2.down * 2 * Time.deltaTime);
        }
    }
}
