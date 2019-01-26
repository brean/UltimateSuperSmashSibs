using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerSelectManager : MonoBehaviour
{
    public static PlayerSelectManager instance;
    public List<GameObject> characters = new List<GameObject>();
    [SerializeField]
    public List<Player> players = new List<Player>();
    public Color[] colors = new Color[]{ Color.red, Color.blue, Color.green, Color.yellow };

    public List<Character> remainingPlayer = new List<Character>(new[] { Character.jock, Character.hipster, Character.nerd, Character.princess });

    private void Awake()
    {
        DontDestroyOnLoad(this);
        instance = this;
    }

    // Start is called before the first frame update
    void Start()
    {
        
        for (int i = 0; i < 4; i++)
        {
            GameObject charPrefab = GameObject.Find("Character" + (i + 1) + "SelectPrefab");
            characters.Add(charPrefab);
        }
    }

    internal Player getPlayerForCharacter(Character character)
    {
        foreach (Player p in players)
        {
            if (p.character == character)
            {
                return p;
            }
        }
        return null;
    }

    void checkAddPlayer(int number, string inputType)
    {
        foreach (Player p in players)
        {
            if (p.number == number && p.inputType == inputType)
            {
                return;
            }
        }

        if (Mathf.Abs(Input.GetAxis("Player" + number + inputType + "Vertical")) > 0.3 || 
            Mathf.Abs(Input.GetAxis("Player" + number + inputType + "Horizontal")) > 0.3)
        {
            Player p = new Player();
            p.inputType = inputType;
            p.number = number;
            p.color = colors[players.Count];
            assignPlayerToCharacter(remainingPlayer[0], p);
            remainingPlayer.RemoveAt(0);
            players.Add(p);
            if (players.Count == 4)
            {
                // TODO: wait for all player to be ready
                SceneManager.LoadScene("Level1");
            }
        }
    }

    public void assignPlayerToCharacter(Character character, Player player)
    {
        foreach (GameObject go in characters)
        {
            if (go.GetComponent<PlayerSelectArrows>().character == character)
            {
                go.GetComponent<PlayerSelectArrows>().addPlayer(player);
                player.character = character;
            }
        }
    }

    // Update is called once per frame
    void Update()
    {
        // new player can only until we have 4 players
        if (players.Count < 4)
        {
            for (int i = 0; i < characters.Count; i++)
            {
                checkAddPlayer(i + 1, "Joy");
                checkAddPlayer(i + 1, "Key");
            }
        }
        
    }
}

