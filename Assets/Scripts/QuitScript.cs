using UnityEngine;
using System.Collections;

/// <summary>
/// Title screen script
/// </summary>
public class QuitScript : MonoBehaviour
{
	void OnMouseDown()
	{
		//Debug.Log ("quitting");
		Application.Quit ();
	}
	
}