using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerTeam : MonoBehaviour
{
    Color[] colors = { Color.blue, Color.red };
    public int teamnr;
    // Start is called before the first frame update
    public void SetTeam(int nr)
    {
        teamnr = nr;
        Color col = colors[nr];
        GameObject.Find("Top").GetComponent<SpriteRenderer>().color = new Color(col.r, col.g, col.b);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
