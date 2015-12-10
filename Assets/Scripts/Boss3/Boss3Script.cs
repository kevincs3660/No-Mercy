using UnityEngine;
using System.Collections;

public class Boss3Script : MonoBehaviour {

	private Vector3 movement;
	public float movementSpeed;
	private bool startMove = false;
	private bool hasSpawn = false;
	private bool phase1Complete = false;
	private bool phase2Complete = false;
	private HealthScript health;
	public GameObject platform1;
	public GameObject platform2; 
	public GameObject platform3;
	public GameObject platform4;
	private WeaponScript[] weapons;
	public GameObject phase2Camera;

	// Use this for initialization
	void Start () {
		movement = this.gameObject.transform.position;
		health = this.GetComponent<HealthScript>();
		weapons = GetComponentsInChildren<WeaponScript>();

	
	}
	
	// Update is called once per frame
	void Update () {

		if (hasSpawn == false)
		{
			if (GetComponent<Renderer>().IsVisibleFrom(Camera.main))
			{
				Spawn();
				StartCoroutine(startMovement());
			}
		}
		else
		{
			Debug.Log(health.hp);
			if(health.hp == 1 && phase1Complete == false)
			{
				Instantiate(platform3);
				Instantiate(platform4);
				Instantiate(phase2Camera);
				Debug.Log ("MOVE TO NEXT THING");
				startMove = true;
				weapons[0].shootingRate =1.5f;
				phase1Complete = true;
				movementSpeed = 4.5f;

			}
			if(startMove)
			{
				if(phase1Complete == false)
				{

				}
				movement.y = movementSpeed;
				movement *= Time.deltaTime;
				transform.Translate (movement);	
			}
		}
	
	}

	IEnumerator startMovement()
	{
		float timer = 0f;

		while(timer < 5)
		{
			timer += Time.deltaTime;
			yield return null;
		}
		startMove = true; 
		Instantiate(platform1);
		Instantiate(platform2);

	}

	private void Spawn()
	{
		hasSpawn = true;
		
		// Enable everything
		// -- Collider
		GetComponent<Collider2D>().enabled = true;
	}

	void OnTriggerEnter2D(Collider2D collision)
	{
		if(collision.gameObject.tag == "Boss3Stop")
		{
			Debug.Log ("collided with shit");
			startMove = false;
		}
		else if(collision.gameObject.tag == "Boss3Stop2")
		{
			startMove = false;
			phase2Complete = true;
			weapons[0].shootingRate = 1f;
		}

		PlayerController2 player = collision.gameObject.GetComponent<PlayerController2>();
		if(player != null)
		{
			Destroy(player.gameObject);
		}
	}

	void OnDestroy()
	{
		//transform.parent.gameObject.AddComponent<GameWinScript>();
	}
}
