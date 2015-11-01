using UnityEngine;
using System.Collections;

public class InvincibilityScript : MonoBehaviour {
	

	public float timeLength = 5;
	
	void OnCollisionEnter2D(Collision2D collision)
	{
		Destroy(gameObject);
	}
}
