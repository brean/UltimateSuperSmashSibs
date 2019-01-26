using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GetChildren : MonoBehaviour
{
    // get child with given name
    public static GameObject findByName(GameObject go, string name)
    {
        if (go.name == name)
        {
            return go;
        }
        foreach (Transform t in go.transform)
        {
            GameObject g = findByName(t.gameObject, name);
            if (g != null)
            {
                return g;
            }
        }
        return null;
    }
}
