using UnityEngine;
using System.Collections;

public class MeleeEnemyScript : MonoBehaviour {
	
	private float playerx;
	private float playery;
	private PlayerController2 player1;
	private bool hasSpawn;
	private Animator animator;

	
	public Vector2 speed = new Vector2(1, 0);
	public int damage = 1;

	//private Rigidbody2D rb;
	private Vector2 direction;
	private Vector2 movement;
	// Use this for initialization
	void Start () {

		//rb = GetComponent<Rigidbody2D>();
		GameObject player = GameObject.Find ("Player");
		if(player != null)
			player1 = (PlayerController2)player.GetComponent (typeof(PlayerController2));
		

		animator = this.GetComponent<Animator>();
		//GameObject parent = player.transform.parent.gameObject.transform.position.x;
		//player1 = (PlayerController2)GameObject.Find ("Player");
		//player x = transform.Find("Player").GetComponent.

		//Debug.Log ("Player X: " + playerx + " Player Y: " + playery);
		//Debug.Log ("Transform parent x: " + player.transform.parent.gameObject.transform.position.x + " Tranform parent y: " + player.transform.parent.gameObject.transform.position.y);
	}


	void FixedUpdate()
	{
		if (hasSpawn == false)
		{
			if (GetComponent<Renderer>().IsVisibleFrom(Camera.main))
			{
				Spawn();
			}
		}
		else
		{
			if(player1 != null)
				direction = (player1.transform.position - this.transform.position).normalized;
			movement = new Vector2 (speed.x * direction.x, speed.y * direction.y);
			movement *= Time.deltaTime;

			if(movement.x > 0)
				animator.SetInteger("Direction", 1);
			else if(movement.x < 0)
				animator.SetInteger("Direction", 0);
			
			//rb.MovePosition(movement);
			transform.Translate (movement);

			// 4 - Out of the camera ? Destroy the game object.
			if (GetComponent<Renderer>().IsVisibleFrom(Camera.main) == false)
			{
				Destroy(gameObject);
			}
		}
	}

	private void Spawn()
	{
		hasSpawn = true;
		
		// Enable everything
		// -- Collider
		GetComponent<Collider2D>().enabled = true;
	}
}
