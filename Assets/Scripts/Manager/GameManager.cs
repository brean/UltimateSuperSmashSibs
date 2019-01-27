﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{

    public static GameManager instance;
    public List<Player> players = new List<Player>();


    // Start is called before the first frame update
    void Start()
    {
        //Check if instance already exists
        if (instance == null)

            //if not, set instance to this
            instance = this;

        //If instance already exists and it's not this:
        else if (instance != this)

            //Then destroy this. This enforces our singleton pattern, meaning there can only ever be one instance of a GameManager.
            Destroy(gameObject);

        //Sets this to not be destroyed when reloading scene
        DontDestroyOnLoad(gameObject);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public Player playerForCharacter(Character character)
    {
        if (players.Count != 4)
        {
            // quickly fake inputs for testing
            players = new List<Player>
            {
                new Player{
                    character = Character.princess,
                    inputType = InputType.Key,
                    number = 1,
                    color = Color.red,
                    team = 0,
                    active = true,
                    ready = true
                },
                new Player{
                    character = Character.jock,
                    inputType = InputType.Key,
                    number = 2,
                    color = Color.blue,
                    team = 0,
                    active = true,
                    ready = true
                },
                new Player{
                    character = Character.hipster,
                    inputType = InputType.Key,
                    number = 3,
                    color = Color.green,
                    team = 1,
                    active = true,
                    ready = true
                },
                new Player{
                    character = Character.nerd,
                    inputType = InputType.Key,
                    number = 4,
                    color = Color.yellow,
                    team = 1,
                    active = true,
                    ready = true
                }
            };
        }
        foreach (Player p in players)
        {
            if (p.character == character)
            {
                return p;
            }
        }
        return null;
    }
}