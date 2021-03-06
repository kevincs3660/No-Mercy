﻿using UnityEngine;

/// <summary>
/// Handle hitpoints and damages
/// </summary>
public class HealthScript : MonoBehaviour
{
	/// <summary>
	/// Total hitpoints
	/// </summary>
	public int hp = 1;
	public GameObject bloodsplatter;
	
	/// <summary>
	/// Enemy or player?
	/// </summary>
	public bool isEnemy = true;
	
	/// <summary>
	/// Inflicts damage and check if the object should be destroyed
	/// </summary>
	/// <param name="damageCount"></param>
	public void Damage(int damageCount)
	{
		hp -= damageCount;
		
		if (hp <= 0)
		{
			// Dead!
			//Destroy (gameObject, 5);

			//Debug.Log("Attempginty to send message");
			gameObject.SendMessage("deadNow");
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
				Debug.Log("BloodPLASPTEKTRJ:");
				Instantiate(bloodsplatter, gameObject.transform.position, Quaternion.identity);
				Damage(shot.damage);
				
				// Destroy the shot
				//Debug.Log("Killin ratata");
				Destroy(shot.gameObject); // Remember to always target the game object, otherwise you will just remove the script
			}
		}
	
	}	
}