using UnityEngine;
using System.Collections;

public class Boss2Script : MonoBehaviour {

	public GameObject[] clones;

	private WeaponScript[] weapons;
	private HealthScript health;
	private bool phase1 = true;
	private bool phase2 = false;
	private bool phase3 = false;
	private bool top = true;
	private bool bottom = false;
	private Vector2 movement;
	private Vector3 originalPosition;
	private bool topFinished = true;
	private bool bottomFinished = true;
	private GameObject player;
	private Animator animator;
	private bool facingRight;
	private Vector2 direction;
	public Sprite facingRightImage;
	public Sprite facingLeftImage;

	void Awake()
	{
		animator = this.gameObject.GetComponent<Animator> ();
		animator.enabled = false;
		weapons = GetComponentsInChildren<WeaponScript>();
	}

	// Use this for initialization
	void Start () 
	{
		health = this.GetComponent<HealthScript>();
		originalPosition = this.transform.position;
		player = GameObject.Find("Player");
		//hp = health.hp;
	}
	
	// Update is called once per frame
	void Update () 
	{
		if(player == null)
			weapons[0].enabled = false;

		if(player != null)
			direction = (player.transform.position - this.transform.position).normalized;
		movement = new Vector2 (direction.x, direction.y);
		movement *= Time.deltaTime;
		
		if(movement.x > 0)
		{
			facingRight = true;
			this.GetComponent<SpriteRenderer>().sprite = facingRightImage;
		}
		else if(movement.x < 0)
		{
			facingRight = false;
			this.GetComponent<SpriteRenderer>().sprite = facingLeftImage;
		}

		if(health.hp == 2 && !phase2)
		{
			//StopCoroutine(driveByTop());
			//StopCoroutine(driveByBottom());
			StopAllCoroutines();
			phase1 = false;
			top = false;
			bottom = false;
			topFinished = true;
			bottomFinished = true;
			StartCoroutine(HP2());
			phase2 = true;
		}
		if(health.hp == 1 && !phase3)
		{
			StopAllCoroutines();
			phase2 = false;
			top = true;
			bottom= false;
			StartCoroutine(HP3 ());
			phase3 = true;
		}

		if(!GetComponent<Renderer>().IsVisibleFrom(Camera.main))
		{
			foreach (WeaponScript weapon in weapons)
			{
				weapon.enabled = false;
			}
		}
		else
		{
			foreach (WeaponScript weapon in weapons)
			{
				weapon.enabled = true;
				if (weapon != null && weapon.enabled && weapon.CanAttack)
				{
					weapon.Attack(true);
				}
			}
		}

		//Vector2 movement = this.transform.position;
		if(phase1 || phase3)
		{
			if(top)
			{
				//movement = new Vector2(-15, 0);
				movement.x = -15f;
				movement *= Time.deltaTime;
				transform.Translate (movement);	

				if(topFinished)
				StartCoroutine(driveByTop());

			}
			else if(bottom)
			{
				//movement = new Vector2(15, 0);
				movement.x = 15f;
				movement *= Time.deltaTime;
				transform.Translate (movement);	

				if(bottomFinished)
				StartCoroutine(driveByBottom());
			}
		}
	
	}

	IEnumerator driveByTop ()
	{
		topFinished = false;
		float timer = 0;

		while (timer < 4)
		{
			timer += Time.deltaTime;

			yield return null;
		}
		//movement = new Vector2(movement.x, movement.y -450);
		if(!phase2)
		{
			//movement.y -= 440f;
			//movement *= Time.deltaTime;
			Debug.Log ("Ended drivebyTop");
			//transform.Translate (movement);
			Vector2 pos = new Vector2(this.transform.position.x, this.transform.position.y -7.2f);
			this.transform.position = pos;
			Debug.Log(this.transform.position);
		}
		topFinished = true;
		top = false;
		bottom = true;
	}
	IEnumerator driveByBottom ()
	{
		bottomFinished = false;
		float timer = 0;
		
		while (timer < 4)
		{
			timer += Time.deltaTime;
			
			yield return null;
		}

		//movement = new Vector2(movement.x, movement.y +450);
		if(!phase2)
		{
			//movement.y += 440f;
			//movement *= Time.deltaTime;
			Debug.Log ("Ended drivebyBottom");
			Vector2 pos = new Vector2(this.transform.position.x, this.transform.position.y + 7.2f);
			this.transform.position = pos;
			Debug.Log(this.transform.position);
		}
		bottomFinished = true;
		top = true;
		bottom = false;
	}


	IEnumerator HP2 (){
		
		float timer = 0;

		Instantiate (clones[0]);
		Instantiate (clones[1]);

		Vector3 newMovement = new Vector3(Camera.main.transform.position.x, Camera.main.transform.position.y, 0);

		//Debug.Log (newMovement);
		this.transform.position = newMovement;
		//Debug.Log("Camera position" + newMovement);

		//Debug.Log ("In HP2");
		while (timer < 4)
		{
			timer += Time.deltaTime;
			
			//Debug.Log ("Inside while");
			yield return null;
		}
	}

	IEnumerator HP3 (){
		
		float timer = 0;

		for(int i = 2; i < clones.Length; i++)
			Instantiate(clones[i]);
		
		//Vector3 newMovement = new Vector3(Camera.main.transform.position.x, Camera.main.transform.position.y, 0);
		
		//Debug.Log (newMovement);
		this.transform.position = originalPosition;
		//Debug.Log("Camera position" + newMovement);
		
		//Debug.Log ("In HP2");
		while (timer < 5)
		{
			timer += Time.deltaTime;
			
			//Debug.Log ("Inside while");
			yield return null;
		}
	}

	void OnTriggerEnter2D(Collider2D collision)
	{
		PlayerController2 player = collision.gameObject.GetComponent<PlayerController2>();
		if(player != null)
		{
			Destroy(player.gameObject);
		}
	}

	void deadNow()
	{
		//Destroy (this.gameObject.GetComponent<Rigidbody2D> ());
		Destroy (this.gameObject.GetComponent<BoxCollider2D> ());
		Destroy (gameObject, 0.6f);
		animator.enabled = true;
		if(facingRight)
			animator.Play ("Dead Fox Right_");
		else
			animator.Play ("Dead Fox Left_");
		//dead = true;
		Destroy (gameObject, 1);
	}


	void OnDestroy()
	{
		//transform.parent.gameObject.AddComponent<GameWinScript>();
	}
}
