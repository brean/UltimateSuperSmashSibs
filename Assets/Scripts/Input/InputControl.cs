﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class InputControl : MonoBehaviour
{
    [Tooltip("Player information")]
	public Player player;

    [Tooltip("speed of the player")]
    public float speed = .01f;
    float startingSpeed;

    [Tooltip("invert a stoned player")]
    public float invertedTimer = 0f;

    [Tooltip("stun a player for some time")]
    public float stunTimer = 0f;

    public float screamAnimationTimer = 0f;


    public Rigidbody2D snatchedBy;
    public float snatchedTimer = 0f;

    [Tooltip("keep all the ghosts in a box")]
    public List<GameObject> ghostBox;

    private Rigidbody2D rb2d;
	
	Animator animator;
	
	private bool facingRight;
	private Vector3 initialScale;

    public float backwardsTimer = 0f;
    public LimitedQueue<Vector2> movementHistory = new LimitedQueue<Vector2>(130);
    public Queue<Vector2> revQueue = new Queue<Vector2>();
    //ability stuff
    public GameObject SmokeyPrefab;
    public GameObject GhostyPrefab;
    private float abilityCooldown = 5;
    CharacterSpiteSettings spriteSettings;

    // Start is called before the first frame update
    void Start()
    {
        player = GameManager.instance.playerForCharacter(GetComponent<CharacterSetting>().character);
        spriteSettings = GetComponent<CharacterSpriteManager>().SpritesForCharacter(player.character);

		animator = GetComponent<Animator>();
		animator.speed = 1;
        facingRight = true;
		rb2d = GetComponent<Rigidbody2D>();
		
		initialScale = transform.localScale;

        startingSpeed = speed;
    }

    // Update is called once per frame
    void Update()
    {
        fadeDaGhosts();
        decScream();

        if (backwardsTimer > 0f && revQueue.Count > 0)
        {
            foreach (Transform t in transform)
            {
                if (t.gameObject.name == "TimeTravel")
                {
                    t.gameObject.GetComponent<SpriteRenderer>().enabled = true;
                }
            }

            backwardsTimer -= 1;
            Vector2 retracePosition = revQueue.Dequeue();
            if (backwardsTimer % 2 == 0) //limit ghosties
                dropGhost(retracePosition);

            rb2d.position = retracePosition;
        }
        else if (snatchedTimer > 0f)
        {
            snatchedTimer -= Time.deltaTime;
            rb2d.position = snatchedBy.transform.position;
        } else if(stunTimer > 0f)
        {

            foreach (Transform t in transform)
            {
                if (t.gameObject.name == "Stun")
                {
                    t.gameObject.GetComponent<SpriteRenderer>().enabled = true;
                }
            }
            stunTimer -= Time.deltaTime;
        }
        else
        {
            foreach (Transform t in transform)
            {
                if (t.gameObject.name == "Stun")
                {
                    t.gameObject.GetComponent<SpriteRenderer>().enabled = false;
                }
                if (t.gameObject.name == "TimeTravel")
                {
                    t.gameObject.GetComponent<SpriteRenderer>().enabled = false;
                }
            }
            movePlayer(player.inputName());

            updateAbility();
        }


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
        //Debug.Log("horizon: " + moveHorizontal + " , vertical: " + moveVertical);
        if (Mathf.Abs(moveHorizontal) + Mathf.Abs(moveVertical) < .1) {
            return;
        }
        Vector2 movement = new Vector2(moveHorizontal, moveVertical);
        Vector2 newpos = rb2d.position + (movement * speed);
		
		Flip(moveHorizontal);
		FrontBack(moveVertical);
        movementHistory.Enqueue(newpos);
        rb2d.MovePosition(newpos);

    }
	
	private void Flip (float moveHorizontal)
	{
		if (moveHorizontal > 0.1 || moveHorizontal < -0.1 )
		{
            GetComponent<SpriteRenderer>().sprite = spriteSettings.left;
			facingRight = moveHorizontal < -0.1;
			animator.SetInteger ("Direction", 2);
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
			GetComponent<SpriteRenderer>().sprite = spriteSettings.back;
			transform.localScale = initialScale;
			animator.SetInteger ("Direction", 1);
		}
		
		if(moveVertical < -0.1)
		{
			GetComponent<SpriteRenderer>().sprite = spriteSettings.front;
			transform.localScale = initialScale;
			animator.SetInteger ("Direction", 3);
		}
	}

    public void speedPlayerUp(float speedIncrease, float duration) {
        speed += speedIncrease;
        StartCoroutine(speedPlayerBackDown(duration));

    }

    IEnumerator speedPlayerBackDown(float duration) {
        yield return new WaitForSeconds(duration);
        speed = startingSpeed;
    }

    public void updateAbility() {

        if (abilityCooldown <= 0)
        {
            // if (Input.GetAxis("UseAbility") == 1)
            if (Input.GetKeyDown(player.inputButton()))
            {
                switch (player.character)
                {
                    case Character.princess:
                        GameObject[] players = GameObject.FindGameObjectsWithTag("Player");

                        flashScream(rb2d.position);

                        foreach (GameObject player in players)
                        {
                            float dist = Vector3.Distance(player.transform.position, rb2d.position);
                            if(dist <= 6f && dist != 0f)
                            {
                                if (player.gameObject.GetComponent<InputControl>().player.character == Character.princess)
                                {
                                    continue;
                                }
                                player.GetComponent<InputControl>().stunTimer = 3f;

                            }
                            
                        }
                
                        break;
                    case Character.jock: //Macho
                        GameObject[] players2 = GameObject.FindGameObjectsWithTag("Player");

                        foreach (GameObject player in players2)
                        {
                            float dist = Vector3.Distance(player.transform.position, rb2d.position);
                            if (dist <= 2f && dist != 0f)
                            {
                                print("snatch!!!!!!!!!!!!!!!!!!!!!!!");
                                player.GetComponent<InputControl>().snatchedBy = rb2d;
                                player.GetComponent<InputControl>().snatchedTimer = 4f;
                            }
                        }

                        break;
                    case Character.hipster:
                        GameObject smokey = Instantiate(SmokeyPrefab);
                        smokey.name = "SmokeyWeedyBombyThingy";
                        smokey.transform.position = new Vector2(rb2d.position.x, (float)(rb2d.position.y-0.8));
                        break;
                    case Character.nerd:
                        GameObject[] players3 = GameObject.FindGameObjectsWithTag("Player");

                        foreach (GameObject player in players3)
                        {
                            if (player.gameObject.GetComponent<InputControl>().player.character == Character.nerd)
                            {
                                continue;
                            }

                            player.GetComponent<InputControl>().backwardsTimer = 130f;
                            player.GetComponent<InputControl>().revQueue = player.GetComponent<InputControl>().movementHistory.revQueue();

                        }

                        break;
                    default: break;
                }

                abilityCooldown = 5;
            }
        }
        else
        {
            abilityCooldown -= Time.deltaTime;
        }
    }

    void flashScream(Vector2 pos)
    {
        screamAnimationTimer = 0.5f;

        foreach (Transform t in transform)
        {
            if (t.gameObject.name == "Scream")
            {
                t.gameObject.GetComponent<SpriteRenderer>().enabled = true;
            }
        }
    }

    void decScream()
    {
        if (screamAnimationTimer > 0) {
            screamAnimationTimer -= 0.1f;
        }
        else {
            foreach (Transform t in transform)
            {
                if (t.gameObject.name == "Scream")
                {
                    t.gameObject.GetComponent<SpriteRenderer>().enabled = false;
                }
            }
        }

    }

    void dropGhost(Vector2 pos)
    {
        GameObject smokey = Instantiate(GhostyPrefab);
        smokey.transform.position = pos;
        ghostBox.Add(smokey);
    }

    void fadeDaGhosts()
    {
        for (int i = 0; i < ghostBox.Count; i++)
        {
            var ghost = ghostBox[i];
            var next = new Color(ghost.GetComponent<SpriteRenderer>().color.a, ghost.GetComponent<SpriteRenderer>().color.b, (ghost.GetComponent<SpriteRenderer>().color.g - 0.05f));
            Debug.Log(ghost.GetComponent<SpriteRenderer>().color);
            ghost.GetComponent<SpriteRenderer>().color = next;
            if (next.g <= 0f)
            {
                ghostBox.Remove(ghost);
                Destroy(ghost);
            }
        }
    }

}


/// <summary>
/// Represents a limited set of first-in, first-out objects.
/// </summary>
/// <typeparam name="T">The type of each object to store.</typeparam>
public class LimitedQueue<T> : Queue<T>
{
    /// <summary>
    /// Stores the local limit instance.
    /// </summary>
    private int limit = -1;

    /// <summary>
    /// Sets the limit of this LimitedQueue. If the new limit is greater than the count of items in the queue, the queue will be trimmed.
    /// </summary>
    public int Limit
    {
        get
        {
            return limit;
        }
        set
        {
            limit = value;
            while (Count > limit)
            {
                Dequeue();
            }
        }
    }

    /// <summary>
    /// Initializes a new instance of the LimitedQueue class.
    /// </summary>
    /// <param name="limit">The maximum number of items to store.</param>
    public LimitedQueue(int limit)
        : base(limit)
    {
        this.Limit = limit;
    }

    /// <summary>
    /// Adds a new item to the queue. After adding the item, if the count of items is greater than the limit, the first item in the queue is removed.
    /// </summary>
    /// <param name="item">The item to add.</param>
    public new void Enqueue(T item)
    {
        while (Count >= limit)
        {
            Dequeue();
        }
        base.Enqueue(item);
    }

    public Queue<T> revQueue()
    {
        return new Queue<T>(base.ToArray().Reverse());
    }

}
