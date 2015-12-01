using UnityEngine;
using System.Collections;


/// <summary>
/// Start or quit the game
/// </summary>
public class GameOverScript : MonoBehaviour
{
	void Start()
	{
		StartCoroutine(automaticRestart());
	}
	void Update()
	{
		if(Input.GetKey (KeyCode.Return) == true)
			Application.LoadLevel("MainMenu");
	}

	void OnGUI()
	{
		const int buttonWidth = 120;
		const int buttonHeight = 60;
		/*
		if (
			GUI.Button(
			// Center in X, 1/3 of the height in Y
			new Rect(
			Screen.width / 2 - (buttonWidth / 2),
			(1 * Screen.height / 3) - (buttonHeight / 2),
			buttonWidth,
			buttonHeight
			),
			"Retry!"
			)
			)
		{
			// Reload the level
			Debug.Log (Application.loadedLevel);
			//Application.LoadLevel(Application.loadedLevelName);
			Application.LoadLevel(Application.loadedLevel);
		}*/

		if(GUI.Button(new Rect(Screen.width/2 - (buttonWidth/2),(1 * Screen.height/3) - (buttonHeight/2) + 100, buttonWidth, buttonHeight), "Press enter to exit"))
		{
			Debug.Log("Loading main menu");
			//Application.LoadLevel("MainMenu");
			Application.LoadLevel("MainMenu");
		}
	}

	IEnumerator automaticRestart()
	{
		float timer = 0;

		while (timer < 1f)
		{
			timer += Time.deltaTime;
			yield return null;
		}

		Application.LoadLevel(Application.loadedLevel);

	}
}