using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DustCloud : MonoBehaviour
{
    SpriteRenderer mySprite;
    float rotateVal;
    float vertVal;
    float horizVal;
    float scaleVal;
    // Start is called before the first frame update
    void Start()
    {
        mySprite = GetComponent<SpriteRenderer>();
        StartCoroutine(destroyMe());
        rotateVal = Random.Range(-2.0f, 2.0f);
        scaleVal = Random.Range(.8f, 1.5f);
        vertVal = Random.Range(.5f, 1f);
        horizVal = Random.Range(-1.0f, 1.0f);
        transform.localScale = new Vector3(scaleVal, scaleVal, transform.localScale.z);
    }

    // Update is called once per frame
    void Update()
    {
        transform.Translate(Vector2.down * GameManager.globalSpeed * Time.deltaTime * GameManager.activityFloat * vertVal);
        transform.Translate(Vector2.right * horizVal * Time.deltaTime);
        transform.Rotate(Vector3.back * (rotateVal * 10) * Time.deltaTime);
    }
    IEnumerator destroyMe()
    {
        while (mySprite.color.a > 0)
        {
            mySprite.color = new Color(mySprite.color.r, mySprite.color.g, mySprite.color.b, mySprite.color.a - .02f);
            yield return new WaitForSeconds(.01f);
        }
        Destroy(this.gameObject);
    }
}
