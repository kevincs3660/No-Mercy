using UnityEngine;
using System.Collections;

public class PopUpTextScript : MonoBehaviour {

	private bool showText = false;
	private PlayerController2 player;
	public GUIStyle style;
	public string message;

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
			outlineText(new Rect(100, 370, 100, 100), message, style);
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
