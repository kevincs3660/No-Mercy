using UnityEngine;
using System.Collections;

public class SprintBarScript : MonoBehaviour {

	// Testing sprint bar stuff
	private float barDisplay;
	private Vector2 pos = new Vector2(20,40);
	private Vector2 size = new Vector2(200,25);
	public Texture2D progressBarEmpty;
	public Texture2D progressBarFull;
	private PlayerController2 player;
	public GUIStyle progress_empty, progress_full;
	private float maxSprint = 50;

	void Start()
	{
		GameObject playerGeneric = GameObject.Find ("Player");
		player = playerGeneric.gameObject.GetComponent<PlayerController2>();
		size.x = Screen.width/10;
		size.y = Screen.height/8;

		if(size.x < 125)
			size.x = 125;
		//barDisplay = size.x;
		//Debug.Log(size.x);
		//Debug.Log (size.y);
	}

	void OnGUI()
	{
		size.x = Screen.width/10;
		size.y = Screen.height/8;
		
		if(size.x < 100)
			size.x = 100;
		Debug.Log (size.x);
		//Debug.Log("Size.x" + size.x);
		// draw the background:
		GUI.BeginGroup (new Rect (pos.x, pos.y, size.x, size.y));
			GUI.Box(new Rect(0,0, size.x, size.y), progressBarEmpty, progress_empty);
		
		// draw the filled-in part:
		GUI.BeginGroup (new Rect (0, 0, size.x * barDisplay, size.y));
		//GUI.BeginGroup (new Rect (0, 0, size.x * (barDisplay), size.y));
			GUI.Box(new Rect(0,0, size.x, size.y), progressBarFull, progress_full);
		GUI.EndGroup ();
		
		GUI.EndGroup ();
	}
	

	// Update is called once per frame
	void FixedUpdate () 
	{
		//if(player.dashCounter > 0)
		barDisplay = Time.deltaTime * (player.dashCounter/maxSprint) * size.x;
		//barDisplay = (player.dashCounter/maxSprint) * size.x;
		//barDisplay = Time.deltaTime * player.dashCounter * size.x;
		//else
			//barDisplay = Time.deltaTime * player.dashCooldownCounter;
	}
}
