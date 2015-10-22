using UnityEngine;
using System.Collections;

public class Boss1HP2Script : MonoBehaviour {

	// Use this for initialization
	void Start () {

		Debug.Log ("INnside Boss1HP2Script");
		this.transform.position = new Vector3(Screen.width/2, Screen.height/2 , 0);

		//GameObject newFireball = (GameObject) Instantiate(FireballRight, 

		Debug.Log("Desgtroying sciprt");
		Destroy (this.gameObject.GetComponent<Boss1HP2Script>());
		Debug.Log ("Sciprt destroyed");
	}

}
