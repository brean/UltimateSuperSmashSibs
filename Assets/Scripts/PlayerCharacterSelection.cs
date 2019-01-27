using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCharacterSelection : MonoBehaviour
{
    public Player player;
    public Character character;

    public List<Sprite> sprites;
    private List<GameObject> team = new List<GameObject>();
    private GameObject check;
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
            if (t.gameObject.name == "check")
            {
                t.gameObject.SetActive(false);
                check = t.gameObject;
            }
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (!player.active)
        {
            return;
        }
        if (Input.GetKeyUp(player.inputButton())) {
            player.ready = !player.ready;
            setPlayer(player);
        }
        if (player.ready)
        {
            // player is already ready, he should not be able to change the other stuff again!
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
        check.SetActive(player.ready);
        GetComponentInChildren<SpriteRenderer>().sprite = sprites[player.characterNumber()];

        GameObject.Find("Manager").GetComponent<PlayerSelectManager>().checkReady();
    }
}
