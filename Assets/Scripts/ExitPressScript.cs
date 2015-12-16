using UnityEngine;
using System.Collections;

/// <summary>
/// Title screen script
/// </summary>
public class ExitPressScript : MonoBehaviour
{
	void OnMouseDown()
	{
		Application.LoadLevel ("MainMenu");
		Time.timeScale = 1;
	}
	
}