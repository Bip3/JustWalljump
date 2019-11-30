using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class skyScript : MonoBehaviour
{
    // Start is called before the first frame update
    Vector2 origPoint;
    public GameObject skyPrefab;
    bool spawned = false;
    float Timer = 10;
    bool timing = false;
    void Start()
    {
        origPoint = transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        if (GameManager.gameActive)
        {
            transform.Translate(Vector2.down * (.6f * GameManager.globalSpeed) * Time.deltaTime * GameManager.activityFloat);
        }

        if (transform.position.y <= 0 && !spawned)
        {
            SpawnNewSky();
            timing = true;
        }
        if (timing && GameManager.gameActive)
        {
            Timer -= Time.deltaTime;
            if (Timer <= 0)
            {
                Destroy(this.gameObject);
            }
        }
        
    }
    void SpawnNewSky()
    {
        spawned = true;
        GameObject prefab = Instantiate(skyPrefab, new Vector3(transform.position.x, 9.9f, skyPrefab.transform.position.z), transform.rotation);
        prefab.transform.localScale = new Vector3(transform.localScale.x, prefab.transform.localScale.y * -1, prefab.transform.localScale.z);

    }
}
