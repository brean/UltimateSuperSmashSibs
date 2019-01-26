using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Button : MonoBehaviour
{
    public GameObject myObstacle;
    public GameObject mySupport;

    private void Start()
    {
        //mySupport = this.transform.Find("Support").gameObject;
        myObstacle.SetActive(true);
        mySupport.SetActive(false);
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            myObstacle.SetActive(false);
            mySupport.SetActive(true);
            Debug.Log("Button activated!");
        }
    }
}
