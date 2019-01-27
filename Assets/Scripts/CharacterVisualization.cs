using System.Collections;
using System.Collections.Generic;
using Unity.Collections;
using UnityEngine;

public class CharacterVisualization : MonoBehaviour
{
    [Tooltip("Player information")]
    [ReadOnly]
    public Player player;

    public Dictionary<int, Color> teamColors = new Dictionary<int, Color>
    {
        { 0, Color.blue },
        { 1, Color.red }
    };
    // Start is called before the first frame update
    void Start()
    {
        
        player = PlayerSelectManager.playerForCharacter(GetComponent<CharacterSetting>().character);

        foreach (Transform t in transform)
        {
            if (t.gameObject.name == "TeamCircle")
            {
                t.gameObject.GetComponent<SpriteRenderer>().color = teamColors[player.team];
            }
        }
        // set team color from 
    }
}
