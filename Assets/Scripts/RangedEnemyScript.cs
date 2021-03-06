﻿using UnityEngine;

/// <summary>
/// Enemy generic behavior
/// </summary>
public class RangedEnemyScript : MonoBehaviour
{
	private bool hasSpawn;
	private WeaponScript[] weapons;
	public Sprite facingLeftImage;
	public Sprite facingRightImage;
	private PlayerController2 player1;
	private Vector2 direction;
	private Vector2 movement;
	private bool dead = false;
	private bool facingRight;
	private Animator animator;

	public AudioClip death;
	
	void Awake()
	{
		//Debug.Log (this.tag != "Boss3");
		if (this.tag != "Clone" && this.tag != "Boss3" && this.tag != "Boss2" && this.tag != "Boss1") {
			//Debug.Log(this.tag);
			animator = this.gameObject.GetComponent<Animator> ();
			animator.enabled = false;
		}
		// Retrieve the weapon only once
		weapons = GetComponentsInChildren<WeaponScript>();
		GameObject player = GameObject.Find ("Player");
		if(player != null)
			player1 = player.GetComponent<PlayerController2>();
		
		// Retrieve scripts to disable when not spawn
	}
	
	// 1 - Disable everything
	void Start()
	{
		hasSpawn = false;
		
		// Disable everything
		// -- collider
		GetComponent<Collider2D>().enabled = false;
		// -- Moving
		//moveScript.enabled = false;
		// -- Shooting
		foreach (WeaponScript weapon in weapons)
		{
			weapon.enabled = false;
		}
	}
	
	void Update()
	{
		// 2 - Check if the enemy has spawned.
		if (hasSpawn == false)
		{
			if (GetComponent<Renderer>().IsVisibleFrom(Camera.main))
			{
				Spawn();
			}
		}
		else
		{
			if(!dead)
			{
				if(player1 != null)
					direction = (player1.transform.position - this.transform.position).normalized;
				movement = new Vector2 (direction.x, direction.y);
				movement *= Time.deltaTime;
				
				if(movement.x > 0)
				{
					facingRight = true;
					this.GetComponent<SpriteRenderer>().sprite = facingRightImage;
					//weapons[0].transform.position = new Vector2(this.transform.position.x + .5f, this.transform.position.y + 0.25f);
					if(this.tag != "Boss3" && this.tag != "Boss1")
						weapons[0].transform.localPosition = new Vector2(0.5f, 0.25f);
				}
				else if(movement.x < 0)
				{
					facingRight = false;
					this.GetComponent<SpriteRenderer>().sprite = facingLeftImage;
					if(this.tag != "Boss3" && this.tag != "Boss1")
						weapons[0].transform.localPosition = new Vector2(-0.5f,  0.25f);
				}
				// Auto-fire
				foreach (WeaponScript weapon in weapons)
				{
					if (weapon != null && weapon.enabled && weapon.CanAttack)
					{
						weapon.Attack(true);
					}
				}
			}
			
			// 4 - Out of the camera ? Destroy the game object.
			if (GetComponent<Renderer>().IsVisibleFrom(Camera.main) == false)
			{
				Destroy(gameObject);
			}
		}
	}
	
	// 3 - Activate itself.
	private void Spawn()
	{
		hasSpawn = true;
		
		// Enable everything
		// -- Collider
		GetComponent<Collider2D>().enabled = true;
		// -- Moving
		//moveScript.enabled = true;
		// -- Shooting
		foreach (WeaponScript weapon in weapons)
		{
			weapon.enabled = true;
		}
	}

	void deadNow()
	{
		//Destroy (this.gameObject.GetComponent<Rigidbody2D> ());
		if(death != null)
			AudioSource.PlayClipAtPoint (death, transform.position, 0.35f);
		Destroy (this.gameObject.GetComponent<BoxCollider2D> ());
		Destroy (gameObject, 0.6f);
		if (this.tag != "Clone" && this.tag != "Boss3" && this.tag != "Boss2"  && this.tag != "Boss1") {
			animator.enabled = true;
			if (facingRight)
				animator.Play ("Dead Fox Right_");
			else
				animator.Play ("Dead Fox Left_");
		}
		dead = true;
		Destroy (gameObject, 1);
	}
}