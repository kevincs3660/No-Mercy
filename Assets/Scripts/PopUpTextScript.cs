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
			GUI.Label(new Rect(100, 370, 5000, 5000), message, style);
	}

}
