using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Item : MonoBehaviour
{
    public GameObject player;

    [Range(1,2)]
    public int itemType;

    void OnTriggerEnter2D(Collider2D other)
    {
        switch (itemType){
            case 1:
                if (other.gameObject.CompareTag("Player"))
                {
                    player = other.gameObject;
                    player.GetComponent<InputControl>().speed += 0.2f;
                    this.gameObject.SetActive(false);
                }
                break;
            case 2:
                //TODO
                break;
        }
    }
}
