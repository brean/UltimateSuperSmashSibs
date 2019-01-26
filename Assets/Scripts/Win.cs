using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Win : MonoBehaviour
{

    public GameObject winningPlayer;
    public GameObject winningTeam;


    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            winningPlayer = other.gameObject;
            //TODO: winningTeam = winningPlayer.team;
            //TODO: go to win screen
        }
    }
}
