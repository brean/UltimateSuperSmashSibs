using System.Collections;
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
    }

    // Update is called once per frame
    void Update()
    {
        float moveHorizontal = Input.GetAxis(inputName + "Horizontal");
        float moveVertical = -Input.GetAxis(inputName + "Vertical");

        Vector2 movement = new Vector2(moveHorizontal, moveVertical);
        Vector2 newpos = rb2d.position + (movement * speed);


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
}
