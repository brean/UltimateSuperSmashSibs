using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class CharacterSpiteSettings
{
    public Character character;
    public Sprite left;
    public Sprite front;
    public Sprite back;
    public Sprite winning;
}

public class CharacterSpriteManager : MonoBehaviour
{

    public List<CharacterSpiteSettings> settings = new List<CharacterSpiteSettings>();
    // Start is called before the first frame update

    public CharacterSpiteSettings SpritesForCharacter(Character character)
    {
        foreach (CharacterSpiteSettings setting in settings)
        {
            if (setting.character == character)
            {
                return setting;
            }
        }
        return null;
    }
}
