using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using System;
/**
 * Adds new Player when a joystick gets connected or a key is pressed
 *
 * TODO: replace this with cinemachine!
 */

public class PlayerManager : MonoBehaviour
{
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
            colliders[0].offset = new Vector2(-camHalfWidth + mapBorderOffset - 10.5f, 0);
            colliders[0].size = new Vector2(20, 100);
            //end
            colliders[1].offset = new Vector2(camHalfWidth - mapBorderOffset + 10.5f, 0);
            colliders[1].size = new Vector2(20, 100);
        }

    }

    // Update is called once per frame
    void Update()
    {
        GameObject[] players = GameObject.FindGameObjectsWithTag("Player");

        if(players.Length != 0)
        {
            List<float> xess = new List<float>();

            foreach (GameObject player in players)
            {
                try
                {
                    xess.Add(player.transform.position.x);
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
