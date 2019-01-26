using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Obstacles : MonoBehaviour
{


    GameObject bridge;
    GameObject tilemap_bridge;

    // Start is called before the first frame update
    void Start()
    {
        bridge = GameObject.Find("Bridge");
        tilemap_bridge = GameObject.Find("Tilemap_Bridge");
        bridge.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {

    }

    void OnTriggerEnter2D (Collider2D other)
    {
        // collect treasures
        if (other.gameObject.CompareTag("Button"))
        {
            bridge.SetActive(true);
            tilemap_bridge.SetActive(false);
            Debug.Log("triggered");
        }
    }

}
