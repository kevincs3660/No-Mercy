using UnityEngine;
using System.Collections;

public class EscapeKeyCheckScript : MonoBehaviour {

	private bool escapePressed = false;
	public GameObject restartMenuPrefab;
	private GameObject menuObject;
	private SpriteRenderer menu;
	// Use this for initialization
	void Start () {
		menuObject = Instantiate (restartMenuPrefab) as GameObject;
		//menuObject = thing.gameObject;
		menu = menuObject.gameObject.GetComponent<SpriteRenderer> ();
		menu.enabled = false;
	}
	
	// Update is called once per frame
	void Update () {
		if(Input.GetKeyDown (KeyCode.Escape) == true && escapePressed == false)
		{
			//Debug.Log ("Pause");
			//Instantiate(restartMenu, new Vector3(, Screen.height/2, 0), Quaternion.identity);
			//Instantiate(restartMenu);
			Vector3 newPosition =  Camera.main.ScreenToWorldPoint(new Vector3(Screen.width/2, Screen.height/2, 0));
			newPosition = new Vector3(newPosition.x, newPosition.y, 0);
			//Debug.Log("Camera position is: " + newPosition);
			//Debug.Log("Old position is: " + menuObject.gameObject.transform.position);
			//menuObject.gameObject.transform.Translate(newPosition);
			menuObject.gameObject.transform.position = newPosition;
			//Debug.Log("New position is: " + menuObject.gameObject.transform.position);
			menu.enabled = true;
			Time.timeScale = 0;
			escapePressed = true;
			//menuObject.gameObject.transform.position = 
		}
		else if (Input.GetKeyDown (KeyCode.Escape) == true && escapePressed == true) 
		{

			//DestroyImmediate(restartMenu, true);
			menu.enabled = false;
			Time.timeScale = 1;
			escapePressed = false;
		}
	}
}
