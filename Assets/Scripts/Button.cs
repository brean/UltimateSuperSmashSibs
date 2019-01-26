using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Button : MonoBehaviour
{
    public GameObject myObstacle;
    public GameObject mySupport;

    public Sprite deactivatedSprite;

    [Range(1, 2)] // 1 = Good, 2 = Evil
    public int buttonType;

    //for Evil Buttons
    float fenceUpDuration = 5f;

    private void Start()
    {

        switch (buttonType)
        {
            //if GOOD BUTTON (removes obstacle, potentially adds support)
            case 1:
                if (transform.childCount == 2)
                {
                    mySupport = this.gameObject.transform.GetChild(0).gameObject;
                    myObstacle = this.gameObject.transform.GetChild(1).gameObject;
                }
                myObstacle.SetActive(true);
                mySupport.SetActive(false);
                break;

            //if BAD BUTTON (adds obstacle)
            case 2:
                myObstacle = this.gameObject.transform.GetChild(0).gameObject;
                myObstacle.SetActive(false);
                break;
        }
    }

    void OnTriggerEnter2D(Collider2D other)
    {

        switch (buttonType)
        {
            case 1:
                if (other.gameObject.CompareTag("Player"))
                {
                    myObstacle.SetActive(false);
                    StartCoroutine(putUpTheBridge());
                    this.GetComponent<SpriteRenderer>().sprite = deactivatedSprite;
                    Debug.Log("Good Button activated!");
                }
                break;
            case 2:
                if (other.gameObject.CompareTag("Player"))
                {
                    StartCoroutine(putUpTheFence());
                    Debug.Log("Bad Button activated!");
                }
                break;
        }
    }

    IEnumerator putUpTheBridge(){
        StartCoroutine(blinkObject(1f, transform.Find("Bridge").gameObject));
        yield return new WaitForSecondsRealtime(1f);
        mySupport.SetActive(true);
        Debug.Log("Bridge up!");

    }

    IEnumerator putUpTheFence() {
        myObstacle.SetActive(true);
        Debug.Log("Fence Up!");
        yield return new WaitForSecondsRealtime(fenceUpDuration-fenceUpDuration/4);
        StartCoroutine(blinkObject(fenceUpDuration/4, transform.Find("Fence").gameObject));
    }

    IEnumerator blinkObject(float blinkTime, GameObject childObject) {
        Debug.Log("Blink Fence");
        float endTime = Time.time + blinkTime;
        while (Time.time < endTime) {
            childObject.GetComponent<SpriteRenderer>().enabled = false;
            yield return new WaitForSecondsRealtime(0.15f);
            childObject.GetComponent<SpriteRenderer>().enabled = true;
            yield return new WaitForSecondsRealtime(0.15f);
        }
        myObstacle.SetActive(false);
        Debug.Log("Fence Down!");
    }
}