using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WinDisplay : MonoBehaviour
{
    public Sprite wintextteam1;
    public Sprite wintextteam2;

    // Start is called before the first frame update
    void Start()
    {
        CharacterSpriteManager man = GameObject.Find("Player").GetComponent<CharacterSpriteManager>();
        int j = 1;
        for (int i = 0; i < GameManager.instance.players.Count; i++)
        {
            Player p = GameManager.instance.players[i];
            if (p.team == GameManager.instance.winningTeam)
            { 
            GameObject.Find("WinningPlayer"+j).GetComponent<SpriteRenderer>().sprite = 
                man.SpritesForCharacter(p.character).front;
                j++;
            }
        }

        switch (GameManager.instance.winningTeam) { 
            case 1:
                GameObject.Find("WinningText").GetComponent<SpriteRenderer>().sprite = wintextteam1;
                break;
            case 2:
                GameObject.Find("WinningText").GetComponent<SpriteRenderer>().sprite = wintextteam2;
                break;
        }

    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown("return"))
        {
            GameManager.instance.loadScene("Level2");
        }
    }
}
