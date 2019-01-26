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
        inputName = this.gameObject.name;
        Debug.Log("inputname: " + inputName);
    }

    // Update is called once per frame
    void Update()
    {
        movePlayer(inputName + "Joy");
        movePlayer(inputName + "Key");
    }

    void movePlayer(string input) {
        float moveHorizontal = Input.GetAxis(input + "Horizontal");
        float moveVertical = -Input.GetAxis(input + "Vertical");
        Debug.Log("horizon: " + moveHorizontal + " , vertical: " + moveVertical);
        if (Mathf.Abs(moveHorizontal) + Mathf.Abs(moveVertical) < .1) {
            return;
        }
        Vector2 movement = new Vector2(moveHorizontal, moveVertical);
        Vector2 newpos = rb2d.position + (movement * speed);

        rb2d.MovePosition(newpos);
    }
}
