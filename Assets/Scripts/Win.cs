using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Win : MonoBehaviour
{

    public Player winningPlayer;

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            Debug.Log(other.gameObject.name + " from team " + winningPlayer.team + " won!");
            winningPlayer = GameManager.instance.playerForCharacter(
                            other.gameObject.GetComponent<CharacterSetting>().character);
            GameManager.instance.winningTeam = winningPlayer.team;
            GetComponent<AudioSource>().Play(0);
            StartCoroutine(goToWinScreen());
                        
        }
    }

    IEnumerator goToWinScreen() {
        yield return new WaitForSecondsRealtime(0.5f);
        GameManager.instance.loadScene("Win");
    }
}
