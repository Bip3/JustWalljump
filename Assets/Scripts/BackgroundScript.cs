using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BackgroundScript : MonoBehaviour
{
    public GameObject newWall;
    bool spawned = false;
    bool timing = false;
    float Timer = 5f;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (GameManager.gameActive)
        {
            transform.Translate(Vector2.down * GameManager.globalSpeed * Time.deltaTime);
        }
        if (transform.position.y <= -11.55f && !spawned)
        {
            TimeToDie();
            timing = true;

        }
        if (timing && GameManager.gameActive)
        {
            Timer -= Time.deltaTime * GameManager.activityFloat;
            if (Timer <= 0)
            {
                Destroy(this.gameObject);
            }
        }
    }

    void TimeToDie()
    {
        spawned = true;
        Instantiate(newWall, new Vector3(0, 21.4f, transform.position.z), Quaternion.identity);
    }
}
