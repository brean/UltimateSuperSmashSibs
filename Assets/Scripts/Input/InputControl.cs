﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputControl : MonoBehaviour
{
	public Player player;

    [Tooltip("character for input control")]
    public Character character;

    [Tooltip("Number of the joystick")]
    public string inputName;

    [Tooltip("speed of the player")]
    public float speed = .01f;
    float startingSpeed;

    [Tooltip("invert a stoned player")]
    public float invertedTimer = 0f;

    private Rigidbody2D rb2d;
	
	private bool facingRight;
	public Sprite front;
	public Sprite back;
	public Sprite leftright;
	private Vector3 initialScale;

    // Start is called before the first frame update
    void Start()
    {
		PlayerSelectManager playerSelectManager = FindObjectOfType<PlayerSelectManager>();
        if (playerSelectManager != null) {
            player = playerSelectManager.getPlayerForCharacter(character);
        }
        
        facingRight = true;
		rb2d = GetComponent<Rigidbody2D>();
        inputName = this.gameObject.name;
        Debug.Log("inputname: " + inputName);
		
		GetComponent<SpriteRenderer>().sprite = front;
		initialScale = transform.localScale;

        startingSpeed = speed;
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
        if (invertedTimer > 0f)
        {
            invertedTimer -= Time.deltaTime;
            moveHorizontal = -moveHorizontal;
            moveVertical = -moveVertical;
        }
        Debug.Log("horizon: " + moveHorizontal + " , vertical: " + moveVertical);
        if (Mathf.Abs(moveHorizontal) + Mathf.Abs(moveVertical) < .1) {
            return;
        }
        Vector2 movement = new Vector2(moveHorizontal, moveVertical);
        Vector2 newpos = rb2d.position + (movement * speed);
		
		Flip(moveHorizontal);
		FrontBack(moveVertical);

        rb2d.MovePosition(newpos);
    }
	
	private void Flip (float moveHorizontal)
	{
		if (moveHorizontal > 0.1 || moveHorizontal < -0.1 )
		{
			GetComponent<SpriteRenderer>().sprite = leftright;
			facingRight = moveHorizontal < -0.1;
			
			Vector3 theScale = transform.localScale;
			
			if (moveHorizontal > 0.1) {
				theScale.x = -initialScale.x;
			} else {
				theScale.x = initialScale.x;
			}
			
			
			transform.localScale = theScale;
		}
	}
	
	public void FrontBack (float moveVertical)
	{
		if(moveVertical > 0.1)
		{
			GetComponent<SpriteRenderer>().sprite = back;
			transform.localScale = initialScale;
		}
		
		if(moveVertical < -0.1)
		{
			GetComponent<SpriteRenderer>().sprite = front;
			transform.localScale = initialScale;
		}
	}

    public void speedPlayerUp(float speedIncrease, float duration) {
        speed += speedIncrease;
        speedPlayerBackDown(duration);

    }

    IEnumerator speedPlayerBackDown(float duration) {
        yield return new WaitForSeconds(duration);
        speed = startingSpeed;
    }
}
