using UnityEngine;
using System.Collections;
using Prime31;

public class PlayerController2 : MonoBehaviour {

	public float speed = 8;
	public float gravity = -35;
	public float jumpHeight = 5;
	public int meleeTimer = 20;
	public int rapidFireTimer = 100;
	public Sprite facingLeftImage;
	public Sprite facingRightImage;
	public AudioClip ratCollisionSound;
	public AudioClip invincibleSound;
	public AudioClip rapidFireSound;
	public AudioClip meleeAttack;
	//public GameObject bloodSplatter;



	private CharacterController2D _controller;
	private WeaponScript[] _weapons;
	private bool facingRight = true;

	private float oldFiringRate; 
	private int rapidFireTimerReset;
	private int meleeTimerReset;
	private int dashTime = 20;
	private bool dashEnabled = true;
	public float dashCounter;
	private int dashCooldown = 50;
	public  int dashCooldownCounter;
	private bool dashing;
	private Animator animator;
	//private bool rapidFire = false;
	enum characterStates
	{
		IDLE = 0,
		FACING_RIGHT = 1,
		FACING_LEFT = 2,
		RUNNING = 3,
		JUMPING = 4,
		DASHING = 5,
		MELEE = 6

	};
	private characterStates state = characterStates.IDLE;
	// Use this for initialization

	void Awake()
	{
		rapidFireTimerReset = rapidFireTimer;
		dashCounter = dashTime;
		dashCooldownCounter = dashCooldown;
		meleeTimerReset = meleeTimer;
		state = characterStates.IDLE;
		_weapons = GetComponentsInChildren<WeaponScript>();
	}

	void Start () {
		_controller = gameObject.GetComponent<CharacterController2D> ();
		animator = this.GetComponent<Animator>();
		//animator.SetInteger ("Direction", 0);
	}
	
	// Update is called once per frame
	void Update () {

		if(GetComponent<Renderer>().IsVisibleFrom(Camera.main) == false)
		{
			PlayerHealthScript health = this.GetComponent<PlayerHealthScript>();
			health.Damage(1000);
			//Destroy(gameObject);
		}

		Vector3 velocity = _controller.velocity;
		float inputX = Input.GetAxis ("Horizontal");
		//Vector2 shotDirection = Vector2.zero;
		//bool shoot = Input.GetButtonDown ("Fire1");
		bool shoot = Input.GetKey (KeyCode.RightArrow) || Input.GetKey (KeyCode.LeftArrow);


		//Debug.Log ("Axis is: " + inputX.ToString ());
		//Debug.Log ("State is : " + state);
		switch(state)
		{
		case characterStates.IDLE:
			//Debug.Log("IDLE");
			if(dashEnabled == false)
				dashCooldownCounter--;
			else
				if(dashCounter < dashTime)
					dashCounter+= 0.5f;


			velocity.x = 0;


			if (inputX < 0) {
				//Debug.Log("IN IDLE: Input is: " + inputX + " changing to face LEFT");
				velocity.x = -speed;
				animator.enabled = true;
				animator.SetInteger("Direction", 0);
				facingRight = false;
				state = characterStates.RUNNING;
				//animator.StopPlayback();
				//gameObject.GetComponent<SpriteRenderer>().sprite = facingLeftImage;
				//shotDirection = -transform.right;
			} else if (inputX > 0) {
				//Debug.Log("IN IDLE: Input is: " + inputX + " changing to face RIGHT");
				velocity.x = speed;
				animator.enabled = true;
				animator.SetInteger("Direction", 1);
				facingRight = true;
				state = characterStates.RUNNING;
				//animator.StopPlayback();
				//gameObject.GetComponent<SpriteRenderer>().sprite = facingRightImage;
				//shotDirection = transform.right;
			}
			else
			{
				//Debug.Log("STopping playback");

				//animator.SetInteger("Direction", 2);
				animator.enabled = false;
				if(facingRight)
					gameObject.GetComponent<SpriteRenderer>().sprite = facingRightImage;
				else
					gameObject.GetComponent<SpriteRenderer>().sprite = facingLeftImage;
			}


			if (Input.GetAxis ("Jump") > 0 && _controller.isGrounded) 
			{	
				state = characterStates.JUMPING;
				velocity.y = Mathf.Sqrt (2f * jumpHeight * -gravity);
			}

			if (Input.GetKeyDown (KeyCode.DownArrow) == true) {
				AudioSource.PlayClipAtPoint (meleeAttack, transform.position, 0.5f);
				if (_weapons[2] != null && _weapons[3] != null)
					if(facingRight)
						_weapons[2].Attack(false);
				else
					_weapons[3].Attack(false);
				state = characterStates.MELEE;
			}


			if (shoot) {

				if (_weapons [0] != null && _weapons [1] != null)
				if (inputX > 0)
					_weapons [0].Attack (false);
				else if (inputX < 0)
					_weapons [1].Attack (false);
				else {
					if (facingRight)
						_weapons [0].Attack (false);
					else
						_weapons [1].Attack (false);
				}
			
				//_weapons[0].Attack(false, shotDirection);
			}

			//Debug.Log("Moving player");
			velocity.y += gravity * Time.deltaTime;

			_controller.move (velocity * Time.deltaTime);
			break;
		case characterStates.RUNNING:

			if(dashEnabled == false)
				dashCooldownCounter--;
			else
				if(dashCounter < dashTime)
					dashCounter+= 0.5f;

			//Debug.Log("Dashcounter: " + dashCounter);

			if(dashCooldownCounter <= 0)
			{
				dashEnabled = true;
				dashCooldownCounter = dashCooldown;
				dashCounter = dashTime;
			}

				velocity.x = 0;
			
			if (inputX < 0) {
				//Debug.Log("Input is: " + inputX + " changing to face LEFT");
				velocity.x = -speed;
				//animator.StopPlayback();
				animator.SetInteger("Direction", 0);
				facingRight = false;
				//gameObject.GetComponent<SpriteRenderer>().sprite = facingLeftImage;
				//shotDirection = -transform.right;
			} else if (inputX > 0) {
				//Debug.Log("Input is: " + inputX + " changing to face RIGHT");
				velocity.x = speed;
				facingRight = true;
				//animator.StopPlayback();
				animator.SetInteger("Direction", 1);
				//gameObject.GetComponent<SpriteRenderer>().sprite = facingRightImage;
				//shotDirection = transform.right;
			}
			else
				state = characterStates.IDLE;
			
			if (Input.GetKey (KeyCode.LeftShift) == true && dashEnabled) {
				if (inputX < 0)
				{
					velocity.x = -speed * 2;
					dashCounter--;
					if(dashCounter <= 0)
						dashEnabled = false;
				}
				else if(inputX > 0)
				{
					velocity.x = speed * 2;
					dashCounter--;
					if(dashCounter <= 0)
						dashEnabled = false;
				}
				//Debug.Log("DashCounter: " + dashCounter);
			}
			
			if (Input.GetAxis ("Jump") > 0 && _controller.isGrounded) {
				state = characterStates.JUMPING;
				velocity.y = Mathf.Sqrt (2f * jumpHeight * -gravity);
			}

			if (Input.GetKeyDown (KeyCode.DownArrow) == true) {
				if (_weapons[2] != null && _weapons[3] != null)
					if(facingRight)
					_weapons[2].Attack(false);
					else
					_weapons[3].Attack(false);
				state = characterStates.MELEE;
				break;
			}
			
			if (shoot) {
				if (_weapons [0] != null && _weapons [1] != null)
					if (inputX > 0)
						_weapons [0].Attack (false);
				else if (inputX < 0)
					_weapons [1].Attack (false);
				else {
					if (facingRight)
						_weapons [0].Attack (false);
					else
						_weapons [1].Attack (false);
				}
				
				//_weapons[0].Attack(false, shotDirection);
			}

			if(state != characterStates.MELEE)
			{
			velocity.y += gravity * Time.deltaTime;
			
			_controller.move (velocity * Time.deltaTime);
			}
			break;
		case characterStates.JUMPING:

			if(dashEnabled == false)
				dashCooldownCounter--;
			else
				if(dashCounter < dashTime)
					dashCounter+= 0.5f;

			// movement
			if (inputX < 0) {
				velocity.x = -speed * 1f;
				animator.SetInteger("Direction", 0);
				facingRight = false;
				//animator.SetInteger("Direction", 1);
				//gameObject.GetComponent<SpriteRenderer>().sprite = facingLeftImage;
				//shotDirection = -transform.right;
			} else if (inputX > 0) {
				velocity.x = speed * 1f;
				animator.SetInteger("Direction", 1);
				facingRight = true;
				//animator.SetInteger("Direction", 0);
				//gameObject.GetComponent<SpriteRenderer>().sprite = facingRightImage;
				//shotDirection = transform.right;
			}
			else
			{
				//state = characterStates.IDLE;
				//break;
			}

			if (Input.GetKey (KeyCode.LeftShift) == true && dashEnabled) {
				if (inputX < 0)
				{
					velocity.x = -speed * 2;
					dashCounter--;
					if(dashCounter <= 0)
						dashEnabled = false;
				}
				else if(inputX > 0)
				{
					velocity.x = speed * 2;
					dashCounter--;
					if(dashCounter <= 0)
						dashEnabled = false;
				}
			}

			// shooting
			if (shoot) {
				if (_weapons [0] != null && _weapons [1] != null)
					if (inputX > 0)
						_weapons [0].Attack (false);
				else if (inputX < 0)
					_weapons [1].Attack (false);
				else {
					if (facingRight)
						_weapons [0].Attack (false);
					else
						_weapons [1].Attack (false);
				}
				
				//_weapons[0].Attack(false, shotDirection);
			}

			velocity.y += gravity * Time.deltaTime; // Take out -20
			_controller.move (velocity * Time.deltaTime);

			if(_controller.isGrounded)
				state = characterStates.IDLE; 

			break;
		case characterStates.MELEE:

			if(dashEnabled == false)
				dashCooldownCounter--;
			else
				if(dashCounter < dashTime)
					dashCounter+= 0.5f;

			meleeTimer--;

			if(meleeTimer <= 0)
			{
				state = characterStates.IDLE;
				meleeTimer = meleeTimerReset;
			}

			break;
		}
		var dist = (transform.position - Camera.main.transform.position).z;
		
		var leftBorder = Camera.main.ViewportToWorldPoint(
			new Vector3(0, 0, dist)
			).x;
		
		var rightBorder = Camera.main.ViewportToWorldPoint(
			new Vector3(1, 0, dist)
			).x;
		
		transform.position = new Vector3(
			Mathf.Clamp(transform.position.x, leftBorder, rightBorder),
			transform.position.y,
			transform.position.z
			);
	}

	void OnTriggerEnter2D(Collider2D collision)
	{
		RapidFireScript rapid = collision.gameObject.GetComponent<RapidFireScript>();
		InvincibilityScript invincible = collision.gameObject.GetComponent<InvincibilityScript>();

		if(invincible != null)
		{
			Debug.Log("Collision with invincible");

			AudioSource.PlayClipAtPoint (invincibleSound, transform.position, 0.5f);
			PlayerHealthScript playerhealth = this.GetComponent<PlayerHealthScript>();
			playerhealth.hp = 2000;
			StartCoroutine(invincibleIndicator(invincible.timeLength));
			StartCoroutine(invincibilityRoutine(invincible.timeLength, playerhealth));
			Destroy(invincible.gameObject);
		}
		// Rapid fire upgrade checking 
		if(rapid != null)
		{
			AudioSource.PlayClipAtPoint (rapidFireSound, transform.position, 0.5f);
			//rapidFire = true; 
			foreach(WeaponScript weapon in _weapons)
			{
				oldFiringRate = weapon.shootingRate;
				weapon.shootingRate = rapid.shootingRate;
			}
			StartCoroutine(upgradeTimerRoutine(rapid.time));
			Destroy(rapid.gameObject);
		}
	}

	void OnCollisionEnter2D(Collision2D collision)
	{
		bool damagePlayer = false;
		// Collision with enemy
		MeleeEnemyScript enemy = collision.gameObject.GetComponent<MeleeEnemyScript>();


		if (enemy != null)
		{
			AudioSource.PlayClipAtPoint(ratCollisionSound, transform.position, 0.3f);
			// Kill the enemy
			//HealthScript enemyHealth = enemy.GetComponent<HealthScript>(); // took this out for some reason
			//if (enemyHealth != null) 
			//	enemyHealth.Damage(enemyHealth.hp);
			
			damagePlayer = true;
		}
		
		// Damage the player
		if (damagePlayer)
		{
			PlayerHealthScript playerHealth = this.GetComponent<PlayerHealthScript>();
			if (playerHealth != null) 
				playerHealth.Damage(1);
		}


	}

	IEnumerator upgradeTimerRoutine (float timelength){

		float timer = 0;

		while (timer < timelength){

			//do this thing

			timer += Time.deltaTime;
			yield return null;

		}

		rapidFireTimer = rapidFireTimerReset;
		//rapidFire = false;
		foreach(WeaponScript weapon in _weapons)
		{
			weapon.shootingRate = oldFiringRate;
		}
		//do this last thing

	}

	IEnumerator invincibilityRoutine(float timelength, PlayerHealthScript playerhealth)
	{
		float timer = 0;

		while (timer < timelength)
		{
			timer += Time.deltaTime;
			yield return null;
		}

		playerhealth.hp = 1;

	}

	IEnumerator invincibleIndicator(float timeLength)
	{
		float timer = 0;
		bool thing = true;

		Debug.Log("Doing THE THING");
		while (timer < timeLength)
		{
			if(thing)
			{
				thing = false;
				Debug.Log("Entering even number thing" );
				gameObject.GetComponent<SpriteRenderer>().color = new Color(1f,1f,1f,0.5f);
			}
			else
			{
				thing = true;
				Debug.Log("Entering odd number thing");
				gameObject.GetComponent<SpriteRenderer>().color = new Color(1f,1f,1f,1f);
			}

			timer += Time.deltaTime;
			yield return null;
		}

		gameObject.GetComponent<SpriteRenderer>().color = new Color(1f,1f,1f,1f);

	}

	public float getDashCounter()
	{
		return dashCounter;
	}

	void OnDestroy()
	{
		//Instantiate(bloodSplatter, this.gameObject.transform.position, Quaternion.identity);
		// Game Over.
		// Add the script to the parent because the current game
		// object is likely going to be destroyed immediately.
		transform.parent.gameObject.AddComponent<GameOverScript>();
	}
}
