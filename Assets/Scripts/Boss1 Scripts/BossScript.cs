using UnityEngine;
using System.Collections;

/// <summary>
/// Enemy generic behavior
/// </summary>
public class BossScript: MonoBehaviour
{
	public GameObject rat1;
	public GameObject rat2;
	public GameObject rat3;
	public GameObject rat4;
	public GameObject rat5;

	public int numberOfRats = 5;

	private GameObject[] rats;
	private WeaponScript[] weapons;
	private HealthScript health;

	private Vector2 initalPosition;
	private float initialFiringRate;
	private int hp; 
	private bool hp2phase = false;
	private bool hp1phase = false;
	public GameObject bloodsplatter;
	
	void Awake()
	{
		// Retrieve the weapon only once
		weapons = GetComponentsInChildren<WeaponScript>();
		
		// Retrieve scripts to disable when not spawn
	}
	
	// 1 - Disable everything
	void Start()
	{
		rats = new GameObject[] {rat1,rat2,rat3,rat4,rat5};
		initialFiringRate = weapons[0].shootingRate;
		initalPosition = transform.position;
		health = this.GetComponent<HealthScript>();
		hp = health.hp;
	}

	void Update()
	{
		hp = health.hp;
		//Debug.Log("HP is" + hp);
		//if (hp == 0)
		//	Application.LoadLevel ("Level2_A");
		if(hp == 2 && !hp2phase)
		{
			//Debug.Log ("Starting Coroutine");
			StartCoroutine(HP2());
			hp2phase = true;
		}
		else if(hp == 1 && !hp1phase)
		{
			StartCoroutine(HP1 ());
			hp1phase = true;
		}


	}

	IEnumerator Dead()
	{
		float timer = 0;
		while(timer < 5)
		{
			timer += Time.deltaTime;
			yield return null;
		}
		Application.LoadLevel("MainMenu");
	}


	IEnumerator HP2 (){
		
		float timer = 0;

		Instantiate(bloodsplatter, initalPosition, Quaternion.identity);
		Instantiate (rats [1]);
		Instantiate (rats [3]);

		Vector2 movement = this.transform.position;
		movement = new Vector2(movement.x - 1000, movement.y + 300);
		movement *= Time.deltaTime;
		
		transform.Translate (movement);

		while (timer < 5){
			
			//do this thing
			foreach(WeaponScript weapon in weapons)
			{
				//oldFiringRate = weapon.shootingRate;
				//Debug.Log ("Old firing rate" + oldFiringRate);
				weapon.shootingRate = initialFiringRate / 3;
			}
			
			timer += Time.deltaTime;

			//Debug.Log ("Inside while");
			yield return null;
		}

		Debug.Log("Made it to the end");

		foreach(WeaponScript weapon in weapons)
		{
			//Debug.Log("setting to oldfirerate" + oldFiringRate);
			weapon.shootingRate = initialFiringRate / 1.5f;
		}

		transform.position = initalPosition;
	}

	IEnumerator HP1 ()
	{
		float timer = 0;
		Instantiate(bloodsplatter, initalPosition, Quaternion.identity);
		foreach(GameObject rat in rats)
			Instantiate(rat);
		
		Vector2 movement = this.transform.position;
		movement = new Vector2(movement.x - 1000, movement.y + 300);
		movement *= Time.deltaTime;

		transform.Translate (movement);
		
		while (timer < 5){
			
			//do this thing
			foreach(WeaponScript weapon in weapons)
			{
				//oldFiringRate = weapon.shootingRate;
				//Debug.Log ("Old firing rate" + oldFiringRate);
				weapon.shootingRate = initialFiringRate / 5;
			}
			
			timer += Time.deltaTime;
			
			//Debug.Log ("Inside while");
			yield return null;
		}
		
		Debug.Log("Made it to the end");
		
		foreach(WeaponScript weapon in weapons)
		{
			//Debug.Log("setting to oldfirerate" + oldFiringRate);
			weapon.shootingRate = initialFiringRate / 3;
		}

		transform.position = initalPosition;
	}

	void OnDestroy()
	{
		// for now just move to level 2
		//Application.LoadLevel ("Level2_A");
		//transform.parent.gameObject.AddComponent<GameWinScript>();
		//GameObject player = GameObject.Find ("Player");
		//if(player != null)
		//	StartCoroutine(bossDead());
			//Application.LoadLevel("MainMenu");
	}


	/*
	IEnumerator bossDead()
	{
		float timer = 0;

		while(timer < 5);
		{
			Debug.Log ("IN WHILE");
			timer += Time.deltaTime;
			yield return null;
		}
		Application.LoadLevel("MainMenu");
	}*/
}