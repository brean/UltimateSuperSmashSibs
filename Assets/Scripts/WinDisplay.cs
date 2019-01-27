using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WinDisplay : MonoBehaviour
{
    public Sprite wintext1;
    public Sprite wintext2;
    public GameObject finalWin;

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

        switch (GameManager.instance.winningTeam) { 
            case 0:
                GameObject.Find("WinText").GetComponent<SpriteRenderer>().sprite = wintext1;
                break;
            case 1:
                GameObject.Find("WinText").GetComponent<SpriteRenderer>().sprite = wintext2;
                break;
        }

    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown("return") && GameManager.instance.previousSceneName == "Level1")
        {
            GameManager.instance.loadScene("Level2");
        }
        else if (GameManager.instance.previousSceneName == "Level2") {
            finalWin.SetActive(true);
        }
    }
}
