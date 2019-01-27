using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Item : MonoBehaviour
{
    public GameObject player;

    [Range(1,3)]
    public int itemType;

    private void Start() {

    }

    void OnTriggerEnter2D(Collider2D other)
    {
        switch (itemType){
            case 1:
                if (other.gameObject.CompareTag("Player")){
                    Debug.Log("Speedboost activated!");
                    player = other.gameObject;
                    player.GetComponent<InputControl>().speedPlayerUp(0.2f, 2f);
                    StartCoroutine(playSoundThenDeactivate());
                }
                break;
            case 2:
                if (other.gameObject.CompareTag("Player")){
                    player = other.gameObject;
                    Debug.Log("Warpfeld activated!");
                    // TODO:
                    // teamMember = player.getTeamMember();
                    // teamMember.transform.position = new Vector2(x, y);
                }
                break;
            case 3:
                //Cloud smoke, to invert stuff
                if (other.gameObject.CompareTag("Player")){
                    Debug.Log("Hipster Smokebomb activated!");
                    if (other.gameObject.GetComponent<InputControl>().player.character == Character.hipster)
                    {
                        break;
                    }
                    player = other.gameObject;
                    player.GetComponent<InputControl>().invertedTimer = 10f;
                    this.gameObject.SetActive(false);
                }
                break;
        }
    }

    IEnumerator playSoundThenDeactivate() {
        gameObject.GetComponent<AudioSource>().Play(0);
        yield return new WaitForSecondsRealtime(0.5f);
        this.gameObject.SetActive(false);
    }


}
