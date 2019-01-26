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
    public Character character; //jock, nerd, princess or hipster

    [SerializeField]
    public Color color;

    [SerializeField]
    public int team = 0;  // Team 0 or 1

    public static Character nextCharacter(Character lastCharacter)
    {
        return (Character)(((int)lastCharacter + 1) % 4);
    }

    public static Character prevCharacter(Character lastCharacter)
    {
        if (lastCharacter == 0)
        {
            return (Character)3;
        }
        return (Character)(((int)lastCharacter - 1) % 4);
    }

    public int characterNumber()
    {
        return (int)character;
    }

    public string inputName()
    {
        return "Player" + number + inputType;
    }
}