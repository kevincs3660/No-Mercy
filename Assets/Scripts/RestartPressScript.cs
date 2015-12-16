using UnityEngine;
using System.Collections;

/// <summary>
/// Title screen script
/// </summary>
public class RestartPressScript : MonoBehaviour
{
	void OnMouseDown()
	{
		Application.LoadLevel (Application.loadedLevel);
		Time.timeScale = 1;
	}
	
}