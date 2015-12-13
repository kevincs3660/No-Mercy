using UnityEngine;
using System.Collections;

public class MoveTowardPlayerRanged : MonoBehaviour {

	private float playerx;
	private float playery;
	private PlayerController2 player1;

	public Vector2 speed = new Vector2(1, 1);
	private Vector2 direction = new Vector2(0,0);
	private Vector2 bulletDirection = new Vector2(0, -1);
	private GameObject player;
	private float angle;

	private Vector2 movement;
	// Use this for initialization
	void Start () {
		//GameObject player = GameObject.Find ("Player");
		player = GameObject.Find ("Player");
		if(player != null)
			player1 = (PlayerController2)player.GetComponent (typeof(PlayerController2));

		if(player1 != null)
			direction = (player1.transform.position - this.transform.position).normalized;

		//direction = player1.GetComponent<Rigidbody2D>().velocity;
		if (direction != Vector2.zero) 
		{
			float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
			angle += 90;
			//angle = Vector2.Angle (direction, bulletDirection);
			//angle += 90;
			//Debug.Log(angle);
			Debug.Log (angle);
			//if(angle > 90 && angle < 270)
			//	direction.x = -direction.x;

			transform.rotation = Quaternion.AngleAxis(angle, Vector3.forward);
		}

		//direction =  Input.GetAxis("Vertical") * transform.forward;
		//direction.x = -direction.x;
		//direction.y = -direction.y;
		Debug.Log(direction);

		//direction = (player1.transform.position - this.transform.position).normalized;


		//Debug.Log ("Angle: " + angle);
		//this.gameObject.transform.rotation = Quaternion.Euler (new Vector3 (angle, 0, 0));
		//this.gameObject.transform.rotation.y = angle;
		//transform.Rotate (angle,0, 0);

		//transform.LookAt (player.transform.position);


		//direction = Vector2.Dot (bulletDirection, direction);

		//direction = direction / (Vector3.Magnitude (new Vector3 (direction.x, direction.y, 0)) * Vector3.Magnitude (new Vector3 (0, -1, 0)));

		//direction = gameObject.transform.Rotate (direction);


		/*
		Vector2 moveDirection = (player1.transform.position - this.transform.position).normalized; 
		Debug.Log ("moveDirection: " + moveDirection);
		Debug.Log ("Direction: " + direction);
		if (moveDirection != Vector2.zero) 
		{
			float angle = Mathf.Atan2(moveDirection.y, moveDirection.x) * Mathf.Rad2Deg;
			transform.rotation = Quaternion.AngleAxis(angle, Vector3.forward);
		}
		*/
		//transform.Rotate(Vector3.down * Time.deltaTime);
		//Vector3 moveDirection = (player1.transform.position.normalized);

		//float angle = Mathf.Atan2(moveDirection.y, moveDirection.x) * Mathf.Rad2Deg;
		//transform.rotation = Quaternion.LookRotation(Vector3.forward, moveDirection-this.transform.position);
	}
	
	// Update is called once per frame
	void Update () {

		//transform.LookAt (player.transform.position);
		//Debug.Log (transform.forward);
		//transform.position += transform.forward * Time.deltaTime * 1;

		//Debug.Log("x: " + direction.x);
		//Debug.Log ("y: " + direction.y);


		//transform.Translate(transform.position);
		//transform.position +=
		//movement = transform.forward * Time.deltaTime * 10;
		//Debug.Log(direction);
		movement = new Vector2 (speed.x * direction.x, speed.y * direction.y);
		Debug.Log(movement);
		movement *= Time.deltaTime;
		transform.position += new Vector3(movement.x, movement.y, 0);
		//transform.Translate (movement);


	}
}
