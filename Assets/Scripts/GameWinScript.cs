using UnityEngine;
using System.Collections;

/// <summary>
/// Start or quit the game
/// </summary>
public class GameWinScript : MonoBehaviour
{
	private bool show = false;
	void Start()
	{
		StartCoroutine(showButton());
	}
	void OnGUI()
	{
		const int buttonWidth = 120;
		const int buttonHeight = 60;

		if(show)
		{
			if(GUI.Button(new Rect(Screen.width/2 - (buttonWidth/2),(1 * Screen.height/3) - (buttonHeight/2) + 100, buttonWidth, buttonHeight), "Main Menu"))
			{
				Debug.Log("Loading main menu");
				//Application.LoadLevel("MainMenu");
				Application.LoadLevel("MainMenu");
			}
		}
	}

	IEnumerator showButton()
	{
		float timer = 0;

		while(timer < 5)
		{
			Debug.Log ("In while");
			timer += Time.deltaTime;
			yield return null;
		}
		Debug.Log ("changing to true");
		show = true;
	}
}