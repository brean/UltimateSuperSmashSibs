using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerSelectArrows : MonoBehaviour
{
    public GameObject ArrowPrefab;
    public List<Player> players = new List<Player>();
    public Character character;   //jock, nerd, princess or hipster
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void updateArrows()
    {
        foreach(Transform t in transform)
        {
            if (t.gameObject.name.StartsWith("arrow"))
            {
                Destroy(t.gameObject);
            }
        }
        for (int i = 0; i < players.Count; i++)
        {
            GameObject ArrowInst = Instantiate(ArrowPrefab, transform, false);
            ArrowInst.transform.localPosition = new Vector3(-1.5f + i, 0);
            ArrowInst.name = players[i].inputName();
            ArrowInst.GetComponent<SpriteRenderer>().color = players[i].color;
        }
        
    }

    internal void addPlayer(Player player)
    {
        players.Add(player);
        updateArrows();
    }
}
