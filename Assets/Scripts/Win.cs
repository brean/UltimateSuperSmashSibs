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
            Debug.Log(other.gameObject.name + " from team " + "???" + " won!");
            winningPlayer = GameManager.instance.playerForCharacter(
                            other.gameObject.GetComponent<CharacterSetting>().character);
            GameManager.instance.winningTeam = winningPlayer.team;
            GameManager.instance.loadScene("Win");
        }
    }
}
