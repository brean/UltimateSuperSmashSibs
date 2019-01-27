using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WinDisplay : MonoBehaviour
{
    public List<Sprite> winTextTeam;

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
                man.SpritesForCharacter(p.character).winning;
                j++;
            }
        }
        GameObject.Find("WinningText").GetComponent<SpriteRenderer>().sprite = winTextTeam[GameManager.instance.winningTeam];

    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown("return") && GameManager.instance.previousSceneName == "Level1")
        {
            GameManager.instance.loadScene("Level2");
        }
    }
}
