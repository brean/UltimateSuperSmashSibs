using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/**
 * Adds new Player when a joystick gets connected or a key is pressed
 */

public class PlayerManager : MonoBehaviour
{
    public GameObject PlayerPrefab;
    Dictionary<string, GameObject> Player = new Dictionary<string, GameObject>();

    // Start is called before the first frame update
    void Start()
    {
        
    }

    void CreatePlayer(string inputName)
    {
        if (Player.ContainsKey(inputName))
        {
            return;
        }
        GameObject NextPlayer = Instantiate(PlayerPrefab);
        NextPlayer.GetComponent<InputControl>().inputName = inputName;
        // TODO: team = Player.Count % 2
        Player.Add(inputName, NextPlayer);
    }

    // Update is called once per frame
    void Update()
    {
        if ((Input.GetKeyDown(KeyCode.UpArrow) || 
            Input.GetKeyDown(KeyCode.DownArrow) || 
            Input.GetKeyDown(KeyCode.LeftArrow) || 
            Input.GetKeyDown(KeyCode.RightArrow))) {
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
        }
    }
}
