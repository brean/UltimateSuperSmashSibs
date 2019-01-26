using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Player : MonoBehaviour
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

    public string inputName()
    {
        return "Player" + number + inputType;
    }
}