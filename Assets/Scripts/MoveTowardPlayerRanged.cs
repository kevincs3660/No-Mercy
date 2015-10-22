using UnityEngine;
using System.Collections;

public class MoveTowardPlayerRanged : MonoBehaviour {

	private float playerx;
	private float playery;
	private PlayerController2 player1;

	public Vector2 speed = new Vector2(1, 1);
	private Vector2 direction = new Vector2(0,0);

	private Vector2 movement;
	// Use this for initialization
	void Start () {
		GameObject player = GameObject.Find ("Player");
		if(player != null)
			player1 = (PlayerController2)player.GetComponent (typeof(PlayerController2));

		if(player1 != null)
			direction = (player1.transform.position - this.transform.position).normalized;
	}
	
	// Update is called once per frame
	void Update () {


		movement = new Vector2 (speed.x * direction.x, speed.y * direction.y);
		movement *= Time.deltaTime;
		
		transform.Translate (movement);

	}
}
