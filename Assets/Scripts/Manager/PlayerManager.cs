using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
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

    public Dictionary<string, GameObject> PlayerDict = new Dictionary<string, GameObject>();
    GameObject camera;

    // Start is called before the first frame update
    void Start()
    {
        //hardcoded test players
        PlayerDict.Add("moep1", GameObject.Find("Player 1"));
        PlayerDict.Add("moep2", GameObject.Find("Player 2"));
        PlayerDict.Add("moep3", GameObject.Find("Player 3"));
        PlayerDict.Add("moep4", GameObject.Find("Player 4"));

        Camera cam = Camera.main;
        camera = Camera.main.gameObject;

        camHalfWidth = ((2f * cam.orthographicSize) * cam.aspect) / 2f;

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

        List<float> xess = new List<float>();

        foreach (GameObject entry in PlayerDict.Values)
        {
            xess.Add(entry.transform.position.x);
        }

        if (xess.Count != 0)
        {
            xPlayerMin = xess.Min();
            xPlayerMax = xess.Max();
        }

        float playerOffset = 4f; //half width of a player
        float mapPaddingOffset = 1f;

        float camMaxPosX = camHalfWidth - playerOffset - mapPaddingOffset;

        //don't go behind start
        if (xPlayerMax < camHalfWidth)
        {
            camera.transform.position = new Vector3(camHalfWidth - mapPaddingOffset, camera.transform.position.y, camera.transform.position.z);
        }
        else if (xPlayerMax - xPlayerMin > camHalfWidth) //distance between first and last player
        {
            camera.transform.position = new Vector3((xPlayerMin - mapPaddingOffset + camHalfWidth), camera.transform.position.y, camera.transform.position.z);
        }
        else
        {
            camera.transform.position = new Vector3(xPlayerMax - mapPaddingOffset, camera.transform.position.y, camera.transform.position.z);
        }


    }
}
