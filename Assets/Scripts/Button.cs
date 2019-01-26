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
                    mySupport.SetActive(true);
                    this.GetComponent<SpriteRenderer>().sprite = deactivatedSprite;
                    Debug.Log("Good Button activated!");
                }
                break;
            case 2:
                if (other.gameObject.CompareTag("Player"))
                {
                    putUpTheFences();
                    Debug.Log("Bad Button activated!");
                }
                break;
        }
    }

    IEnumerator putUpTheFences()
    {
        myObstacle.SetActive(true);
        yield return new WaitForSeconds(5f);
        myObstacle.SetActive(false);
    }

}