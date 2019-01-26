using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Player
{
    [SerializeField]
    public string inputType; // key or joy

    [SerializeField]
    [Range(1, 5)]
    public int number; // number of input (1-4)

    [SerializeField]
    public string character;

    [SerializeField]
    public Color color;

    //ability stuff
    public GameObject SmokeyPrefab;
    private float abilityCooldown = 5;

    public string inputName()
    {
        return inputType + number;
    }

    void updateAbility(bool activateAbility) {
        
        if (abilityCooldown <= 0)
        {
            if (activateAbility)
            {
                GameObject smokey = Instantiate(SmokeyPrefab);
                smokey.name = "SmokeyWeedyBombyThingy";
                smokey.transform.position = rb2d.position;
                abilityCooldown = 5;
            }
        }
        else
        {
            abilityCooldown -= Time.deltaTime;
        }
    }
}