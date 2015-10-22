using UnityEngine;
using System.Collections;

public class RapidFireScript : MonoBehaviour {


	public float shootingRate = 1;
	public float time = 5;

	void OnCollisionEnter2D(Collision2D collision)
	{
		Destroy(gameObject);
	}
}
