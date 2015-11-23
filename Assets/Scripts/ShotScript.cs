using UnityEngine;
using System.Collections;

public class ShotScript : MonoBehaviour {

	public int damage = 1;
	public bool isEnemyShot = false;

	private Vector3 _origPos;

	// Use this for initialization
	void Start () 
	{
		_origPos = this.transform.position;
	}

	void Update()
	{
		if(!GetComponent<Renderer>().IsVisibleFrom(Camera.main))
		{
			Destroy (gameObject);
		}

	}
	
}
