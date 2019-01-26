﻿using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerSelectArrows : MonoBehaviour
{
    public Player player;
    public Character character;

    public List<Sprite> sprites;
    public List<GameObject> team;
    private bool nextPlayer;
    private bool prevPlayer;
    private bool switchTeam;

    void Start()
    {
        foreach (Transform t in transform)
        {
            if (t.gameObject.name.StartsWith("team"))
            {
                t.gameObject.SetActive(false);
                team.Add(t.gameObject);
            }
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (player.inputType == "")
        {
            return;
        }
        float moveHorizontal = Input.GetAxisRaw(player.inputName() + "Horizontal");
        float moveVertical = Input.GetAxisRaw(player.inputName() + "Vertical");
        if (moveVertical > .7 && !nextPlayer)
        {
            nextPlayer = true;
            player.character = Player.nextCharacter(player.character);
            setPlayer(player);
        }
        if (moveVertical < -.7 && !prevPlayer)
        {
            prevPlayer = true;
            player.character = Player.prevCharacter(player.character);
            setPlayer(player);
        }
        if (moveVertical < .2 && moveVertical > -.2)
        {
            prevPlayer = nextPlayer = false;
        }
        if ((moveHorizontal > .7 || moveHorizontal < -.7) && !switchTeam)
        {
            switchTeam = true;
            player.team = (player.team + 1) % 2;
            setPlayer(player);
        }
        if (moveHorizontal < .2 && moveHorizontal > -.2)
        {
            switchTeam = false;
        }
    }


    public void setPlayer(Player player)
    {
        this.player = player;
        foreach (GameObject go in team)
        {
           go.SetActive(false);
        }
        team[player.team].SetActive(true);
        GetComponentInChildren<SpriteRenderer>().sprite = sprites[player.characterNumber()];
    }
}
