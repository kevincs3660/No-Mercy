using UnityEngine;
using System.Collections;

public class PopUpTextScript : MonoBehaviour {

	private bool showText = false;
	private PlayerController2 player;
	public GUIStyle style;
	public string message;

	private int offset = 35;

	void Start()
	{
		Debug.Log(Screen.height);
		//Debug.Log (style.fontSize);
		if (Screen.height > 720) {
			Debug.Log ("Adjusted the shit");
			style.fontSize = style.fontSize + 20;
			offset = 50;
		}
		if (Screen.height > 1079) {
			Debug.Log("Enterd the thing");
			style.fontSize = style.fontSize + 30;
			offset = 100;
		}
	}

	void OnTriggerEnter2D(Collider2D collider)
	{
		player = collider.gameObject.GetComponent<PlayerController2>();
		if(player != null)
		{
			showText = true;
		}
	}

	void OnTriggerExit2D(Collider2D collider)
	{
		player = collider.gameObject.GetComponent<PlayerController2>();
		if(player != null)
		{
			showText = false;
		}
	}

	void OnGUI()
	{
		if(showText && player != null)
		{

			outlineText(new Rect(100, Screen.height - offset, 100, 100), message, style);
		}
			//outlineText(new Rect(100, 370, 100, 100), message, style);
			//GUI.Label(new Rect(100, 370, 5000, 5000), message, style);

	}

	void outlineText(Rect position, string text, GUIStyle styles)
	{
		styles.normal.textColor = Color.white;
		position.x--;
		GUI.Label(position, text, styles);
		position.x +=2;
		GUI.Label(position, text, styles);
		position.x--;
		position.y--;
		GUI.Label(position, text, styles);
		position.y +=2;
		GUI.Label(position, text, styles);
		position.y--;
		style.normal.textColor = Color.black;
		GUI.Label(position, text, styles);
		//style = backupStyle;
	}

}
