using UnityEngine;
using System.Collections;

public class MoveToNextLevel : MonoBehaviour {

	public string nextLevel;

	void OnTriggerEnter2D(Collider2D collider)
	{
		PlayerController2 player = collider.gameObject.GetComponent<PlayerController2>();
		if(player != null)
			Application.LoadLevel(nextLevel);
	}

}
