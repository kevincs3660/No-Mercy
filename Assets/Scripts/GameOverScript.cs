using UnityEngine;
using UnityEditor;

/// <summary>
/// Start or quit the game
/// </summary>
public class GameOverScript : MonoBehaviour
{
	void OnGUI()
	{
		const int buttonWidth = 120;
		const int buttonHeight = 60;
		
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
			Debug.Log (Application.loadedLevelName);
			//Application.LoadLevel(Application.loadedLevelName);
			Application.LoadLevel("BossTest");
		}

		if(GUI.Button(new Rect(Screen.width/2 - (buttonWidth/2),(1 * Screen.height/3) - (buttonHeight/2) + 100, buttonWidth, buttonHeight), "Main Menu"))
		{
			Debug.Log("Loading main menu");
			Application.LoadLevel("MainMenu");
		}
	}
}