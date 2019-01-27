using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Button : MonoBehaviour
{
    public GameObject myObstacle;
    public GameObject mySupport;

    public Sprite activatedSprite;
    public Sprite deactivatedSprite;

    [Range(1, 2)] // 1 = Good, 2 = Evil
    public int buttonType;

    //for Evil Buttons
    float fenceUpDuration = 5f;

    bool buttonActive = true;

    private void Start()
    {
        activatedSprite = GetComponent<SpriteRenderer>().sprite;

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
                if (other.gameObject.CompareTag("Player") && buttonActive)
                {
                    gameObject.GetComponent<AudioSource>().Play(0);
                    StartCoroutine(putUpTheBridge());
                    buttonActive = false;
                    this.GetComponent<SpriteRenderer>().sprite = deactivatedSprite;
                    Debug.Log("Good Button activated!");
                }
                break;
            case 2:
                if (other.gameObject.CompareTag("Player") && buttonActive)
                {
                    gameObject.GetComponent<AudioSource>().Play(0);
                    StartCoroutine(putUpTheFence());
                    Debug.Log("Bad Button activated!");
                }
                break;
        }
    }

    IEnumerator putUpTheBridge(){
        mySupport.SetActive(true);
        StartCoroutine(blinkObject(1f, transform.Find("Bridge").gameObject));
        yield return new WaitForSecondsRealtime(1.5f);
        myObstacle.SetActive(false);
        Debug.Log("Bridge up!");

    }

    IEnumerator putUpTheFence() {
        myObstacle.SetActive(true);
        Debug.Log("Fence Up!");
        buttonActive = false;
        this.GetComponent<SpriteRenderer>().sprite = deactivatedSprite;
        yield return new WaitForSecondsRealtime(fenceUpDuration - fenceUpDuration / 4);
        StartCoroutine(blinkObject(fenceUpDuration / 4, transform.Find("Fence").gameObject));
        yield return new WaitForSecondsRealtime(fenceUpDuration / 4);
        myObstacle.SetActive(false);
        Debug.Log("Fence Down!");
        this.GetComponent<SpriteRenderer>().sprite = activatedSprite;
        buttonActive = true;
     }

    IEnumerator blinkObject(float blinkTime, GameObject childObject) {
        Debug.Log("Blinking object for " + blinkTime);
        float endTime = Time.time + blinkTime;
        while (Time.time < endTime) {
            childObject.GetComponent<SpriteRenderer>().enabled = false;
            yield return new WaitForSecondsRealtime(0.15f);
            childObject.GetComponent<SpriteRenderer>().enabled = true;
            yield return new WaitForSecondsRealtime(0.15f);
        }
    }
}