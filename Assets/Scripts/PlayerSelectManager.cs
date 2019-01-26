using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerSelectManager : MonoBehaviour
{
    public static PlayerSelectManager instance;
    public List<GameObject> characterSelect = new List<GameObject>();
    [SerializeField]
    public List<Player> players = new List<Player>();
    public Color[] colors = new Color[]{ Color.red, Color.blue, Color.green, Color.yellow };

    public List<Character> remainingCharacter = new List<Character>(new[] { Character.jock, Character.hipster, Character.nerd, Character.princess });

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
            characterSelect.Add(charPrefab);
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

    void checkAddPlayer(int number, InputType inputType)
    {
        foreach (Player p in players)
        {
            if (p.number == number && p.inputType == inputType)
            {
                return;
            }
        }

        if (Input.GetKeyUp(Player.inputButton(inputType, number)) || 
           (Mathf.Abs(Input.GetAxis("Player" + number + inputType + "Vertical")) > 0.3 || 
            Mathf.Abs(Input.GetAxis("Player" + number + inputType + "Horizontal")) > 0.3))
        {
            Player p = new Player
            {
                inputType = inputType,
                active = true,
                number = number,
                color = colors[players.Count]
            };
            assignPlayerToCharacter(players.Count, p);
            remainingCharacter.RemoveAt(0);
            players.Add(p);
        }
    }

    public void assignPlayerToCharacter(int number, Player player)
    {
        characterSelect[number].GetComponent<PlayerCharacterSelection>().setPlayer(player);
    }

    // Update is called once per frame
    void Update()
    {
        // new player can only until we have 4 players
        if (players.Count < 4)
        {
            for (int i = 0; i < characterSelect.Count; i++)
            {
                checkAddPlayer(i + 1, InputType.Joy);
                checkAddPlayer(i + 1, InputType.Key);
            }
        }
        
    }

    internal void checkReady()
    {
        if (players.Count != 4)
        {
            // still waiting for group to be complete
            return;
        }
        List<Character> chars = new List<Character>();
        int numTeam1 = 0;
        foreach (Player p in players)
        {
            if (!p.ready)
            {
                // not all player are ready
                return;
            }
            if (p.team == 0)
            {
                numTeam1++;
            }
            if (chars.Contains(p.character))
            {
                // char already taken
                return;
            }
            chars.Add(p.character);
        }
        if (numTeam1 != 2)
        {
            // not 2 equal teams!
            return;
        }
        SceneManager.LoadScene("Level1");
    }
}

