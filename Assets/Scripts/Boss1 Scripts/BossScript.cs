using UnityEngine;

/// <summary>
/// Enemy generic behavior
/// </summary>
public class BossScript: MonoBehaviour
{
	private WeaponScript[] weapons;
	private HealthScript health;

	private int hp; 
	private bool hp2phase = false;
	
	void Awake()
	{
		// Retrieve the weapon only once
		weapons = GetComponentsInChildren<WeaponScript>();
		
		// Retrieve scripts to disable when not spawn
	}
	
	// 1 - Disable everything
	void Start()
	{
		health = this.GetComponent<HealthScript>();
		hp = health.hp;
	}

	void Update()
	{
		hp = health.hp;
		//Debug.Log("HP is" + hp);

		if(hp == 2 && !hp2phase)
		{
			Debug.Log ("Adding script");
			this.gameObject.AddComponent<Boss1HP2Script>();
			hp2phase = true;
		}

	}
}