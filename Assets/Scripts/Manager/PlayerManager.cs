﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using System;
/**
* Adds new Player when a joystick gets connected or a key is pressed
*/

public class PlayerManager : MonoBehaviour
{
    public GameObject PlayerPrefab;

    [Tooltip("max x of players")]
    public float xPlayerMax = 0;

    [Tooltip("min x of players")]
    public float xPlayerMin = 0;

    public float camHalfWidth = 0;
    public float mapBorderOffset = 0.5f;

    public Dictionary<string, GameObject> PlayerDict = new Dictionary<string, GameObject>();
    GameObject camera;

    // Start is called before the first frame update
    void Start()
    {
        //hardcoded test players
        PlayerDict.Add("moep1", GameObject.Find("Player1"));
        PlayerDict.Add("moep2", GameObject.Find("Player2"));
        PlayerDict.Add("moep3", GameObject.Find("Player3"));
        PlayerDict.Add("moep4", GameObject.Find("Player4"));

        Camera cam = Camera.main;
        camera = Camera.main.gameObject;
        camHalfWidth = ((2f * cam.orthographicSize) * cam.aspect) / 2f;

        camera.AddComponent<BoxCollider2D>();
        camera.AddComponent<BoxCollider2D>();

        BoxCollider2D[] colliders = camera.GetComponents<BoxCollider2D>();
        if (colliders.Length == 2)
        {
            //start
            colliders[0].offset = new Vector2(-camHalfWidth + mapBorderOffset, 0);
            colliders[0].size = new Vector2(mapBorderOffset, 100);
            //end
            colliders[1].offset = new Vector2(camHalfWidth - mapBorderOffset, 0);
            colliders[1].size = new Vector2(mapBorderOffset, 100);
        }

    }

    void CreatePlayer(string inputName)
    {
        if (PlayerDict.ContainsKey(inputName))
        {
            return;
        }
        GameObject NextPlayer = Instantiate(PlayerPrefab);
        NextPlayer.GetComponent<InputControl>().inputName = inputName;
        NextPlayer.GetComponent<PlayerTeam>().SetTeam(PlayerDict.Count % 2);
        // TODO: team = Player.Count % 2
        PlayerDict.Add(inputName, NextPlayer);
    }

    // Update is called once per frame
    void Update()
    {/*
        if ((Input.GetKeyDown(KeyCode.UpArrow) ||
            Input.GetKeyDown(KeyCode.DownArrow) ||
            Input.GetKeyDown(KeyCode.LeftArrow) ||
            Input.GetKeyDown(KeyCode.RightArrow)))
        {
            CreatePlayer("ArrowKeys");
        }
        if ((Input.GetKeyDown(KeyCode.W) ||
            Input.GetKeyDown(KeyCode.S) ||
            Input.GetKeyDown(KeyCode.A) ||
            Input.GetKeyDown(KeyCode.D)))
        {
            CreatePlayer("WasdKeys");
        }
        for (int i = 0; i < 4; i++)
        {
            if (Input.GetAxis("Joy" + i + "Horizontal") > .2 ||
                Input.GetAxis("Joy" + i + "Horizontal") > .2)
            {
                CreatePlayer("Joy" + i);
            }
        }*/

        if(PlayerDict.Count != 0)
        {
            List<float> xess = new List<float>();

            foreach (GameObject entry in PlayerDict.Values)
            {
                try
                {
                    xess.Add(entry.transform.position.x);
                }
                catch (NullReferenceException e)
                {
                    //mmmhh, guter code ...
                }

            }

            if (xess.Count != 0)
            {
                xPlayerMin = xess.Min();
                xPlayerMax = xess.Max();
            }

            // camera is always in the middle of all players
            // camera ignores padding
            float newCamPosX = xPlayerMin + ((xPlayerMax - xPlayerMin) / 2);
            if (newCamPosX < camHalfWidth)
            {
                camera.transform.position = new Vector3(camHalfWidth, camera.transform.position.y, camera.transform.position.z);
            }
            else
            {
                camera.transform.position = new Vector3(newCamPosX, camera.transform.position.y, camera.transform.position.z);
            }
        }
    }
}
