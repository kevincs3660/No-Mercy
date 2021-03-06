﻿using UnityEngine;
using System.Collections;


/// <summary>
/// Handle hitpoints and damages
/// </summary>
public class PlayerHealthScript : MonoBehaviour
{
	/// <summary>
	/// Total hitpoints
	/// </summary>
	public int hp = 1;
	
	/// <summary>
	/// Enemy or player?
	/// </summary>
	public bool isEnemy = true;
	public AudioClip deathSound;
	public GameObject bloodsplatter;
	
	/// <summary>
	/// Inflicts damage and check if the object should be destroyed
	/// </summary>
	/// <param name="damageCount"></param>
	public void Damage(int damageCount)
	{
		hp -= damageCount;
		
		if (hp <= 0)
		{
			GameObject playerGeneric = GameObject.Find ("Player");
			PlayerController2 player = playerGeneric.gameObject.GetComponent<PlayerController2>();
			Instantiate(bloodsplatter, player.transform.position, Quaternion.identity);
			AudioSource.PlayClipAtPoint(deathSound, transform.position, 0.3f);
			// Dead!
			Destroy(gameObject);
		}
	}
	
	void OnTriggerEnter2D(Collider2D otherCollider)
	{
		// Is this a shot?
		//Debug.Log ("COLLISION");
		ShotScript shot = otherCollider.gameObject.GetComponent<ShotScript>();
		
		if (shot != null)
		{
			// Avoid friendly fire
			if (shot.isEnemyShot != isEnemy)
			{
				Damage(shot.damage);
				
				// Destroy the shot
				Destroy(shot.gameObject); // Remember to always target the game object, otherwise you will just remove the script
			}
		}
		
	}
	/*
	void OnCollisionEnter2D(Collision2D collider)
	{
		Debug.Log("MELEE ENEMY COLLISION");
		MeleeEnemyScript melee = collider.gameObject.GetComponent<MeleeEnemyScript>();
		if(melee != null)
		{
			Debug.Log("MELEE ENEMY COLLISION");
			Damage (melee.damage);
			
		}
	}
	*/
}