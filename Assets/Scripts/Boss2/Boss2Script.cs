using UnityEngine;
using System.Collections;

public class Boss2Script : MonoBehaviour {

	public GameObject clone1;
	public GameObject clone2;

	private WeaponScript[] weapons;
	private HealthScript health;
	private bool phase1 = true;
	private bool phase2 = false;
	private bool top = true;
	private bool bottom = false;
	private Vector2 movement;
	private bool topFinished = true;
	private bool bottomFinished = true;

	void Awake()
	{
		weapons = GetComponentsInChildren<WeaponScript>();
	}

	// Use this for initialization
	void Start () 
	{
		health = this.GetComponent<HealthScript>();
		//hp = health.hp;
	}
	
	// Update is called once per frame
	void Update () 
	{
		if(health.hp == 2 && !phase2)
		{
			StopCoroutine(driveByTop());
			StopCoroutine(driveByBottom());
			phase1 = false;
			top = false;
			bottom = false;
			topFinished = true;
			bottomFinished = true;
			StartCoroutine(HP2());
			phase2 = true;
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
		if(phase1)
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

		while (timer < 3)
		{
			timer += Time.deltaTime;

			yield return null;
		}
		//movement = new Vector2(movement.x, movement.y -450);
		if(!phase2)
		{
			movement.y -= 450f;
			movement *= Time.deltaTime;
			Debug.Log ("Ended drivebyTop");
			transform.Translate (movement);	
		}
		topFinished = true;
		top = false;
		bottom = true;
	}
	IEnumerator driveByBottom ()
	{
		bottomFinished = false;
		float timer = 0;
		
		while (timer < 3)
		{
			timer += Time.deltaTime;
			
			yield return null;
		}

		//movement = new Vector2(movement.x, movement.y +450);
		if(!phase2)
		{
			movement.y += 450f;
			movement *= Time.deltaTime;
			Debug.Log ("Ended drivebyBottom");
			transform.Translate (movement);	
		}
		bottomFinished = true;
		top = true;
		bottom = false;
	}


	IEnumerator HP2 (){
		
		float timer = 0;
		
		Instantiate (clone1);
		Instantiate (clone2);

		//Camera camera = GetComponent<Camera>();
		Vector3 newMovement = Camera.main.ScreenToWorldPoint( new Vector3(Screen.width/2f, Screen.height/2f, Camera.main.nearClipPlane) );
		Debug.Log (newMovement);
		/*Vector2 movement = this.transform.position;
		movement = new Vector2(movement.x - 1000, movement.y + 300);*/
		//movement = newMovement;
		//movement = new Vector2(newMovement.x, newMovement.y);
		movement.x = 2.6f;
		movement.y = 4.2f;
		movement *= Time.deltaTime;
		
		transform.Translate (movement);
		Debug.Log ("In HP2");
		while (timer < 5){
			
			//do this thing
			/*foreach(WeaponScript weapon in weapons)
			{
				//oldFiringRate = weapon.shootingRate;
				//Debug.Log ("Old firing rate" + oldFiringRate);
				//weapon.shootingRate = initialFiringRate / 3;
			}*/
			
			timer += Time.deltaTime;
			
			//Debug.Log ("Inside while");
			yield return null;
		}
	}

	void OnTriggerEnter2D(Collider2D collision)
	{
		//Debug.Log ("Collision");
		PlayerController2 player = collision.gameObject.GetComponent<PlayerController2>();
		if(player != null)
		{
			Destroy(player.gameObject);
		}
	}
}
