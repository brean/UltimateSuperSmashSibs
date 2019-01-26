using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Item : MonoBehaviour
{
    public GameObject player;

    [Range(1,3)]
    public int itemType;

    void OnTriggerEnter2D(Collider2D other)
    {
        switch (itemType){
            case 1:
                if (other.gameObject.CompareTag("Player"))
                {
                    player = other.gameObject;
                    player.GetComponent<InputControl>().speedPlayerUp(0.2f, 2f);
                    this.gameObject.SetActive(false);
                }
                break;
            case 2:
                if (other.gameObject.CompareTag("Player"))
                {
                    player = other.gameObject;
                    Debug.Log("Warpfeld activated!");
                    // TODO:
                    // teamMember = player.getTeamMember();
                    // teamMember.transform.position = new Vector2(x, y);
                }
                break;
            case 3:
                //Cloud smoke, to invert stuff
                if (other.gameObject.CompareTag("Player"))
                {
                    player = other.gameObject;
                    player.GetComponent<InputControl>().invertedTimer = 10f;
                    this.gameObject.SetActive(false);
                }
                break;
        }
    }

    void OnTriggerEnter2D()
    {

    }
    
}
