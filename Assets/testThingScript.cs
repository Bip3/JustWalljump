using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class testThingScript : MonoBehaviour
{
    float timeLeft = 3;
    // Start is called before the first frame update
    void Start()
    {
        GetComponent<SpriteRenderer>().color = new Color(Random.Range(0f, 1f), Random.Range(0f, 1f), Random.Range(0f, 1f));
    }

    // Update is called once per frame
    void Update()
    {
        timeLeft -= Time.deltaTime;

        if (timeLeft <= 0)
        {
            Destroy(this.gameObject);
        }
    }
}
