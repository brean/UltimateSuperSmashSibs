﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputControl : MonoBehaviour
{
    [Tooltip("Number of the joystick")]
    public string inputName;

    [Tooltip("speed of the player")]
    public float speed = .01f;

    private Rigidbody2D rb2d;

    // Start is called before the first frame update
    void Start()
    {
        rb2d = GetComponent<Rigidbody2D>();
        inputName = this.gameObject.name;
        Debug.Log("inputname: " + inputName);
    }

    // Update is called once per frame
    void Update()
    {
        movePlayer(inputName + "Joy");
        movePlayer(inputName + "Key");

        Camera cam = Camera.main;
        float halfCamWidth = ((2f * cam.orthographicSize) * cam.aspect) / 2f;
        float mapBorderOffset = 1f;

        float maxPosX = (cam.gameObject.transform.position.x + halfCamWidth) - mapBorderOffset;
        float minPosX = (cam.gameObject.transform.position.x - halfCamWidth) + mapBorderOffset;

        print(cam.gameObject.transform.position.x + " || " + halfCamWidth);

        if (newpos.x > maxPosX || newpos.x < minPosX)
        {
            newpos.x = rb2d.position.x;
        }

        rb2d.MovePosition(newpos);


    }

    void movePlayer(string input) {
        float moveHorizontal = Input.GetAxis(input + "Vertical");
        float moveVertical = -Input.GetAxis(input + "Vertical");
        Debug.Log("horizon: " + moveHorizontal + " , vertical: " + moveVertical);

        Vector2 movement = new Vector2(moveHorizontal, moveVertical);
        Vector2 newpos = rb2d.position + (movement * speed);
    }
}
