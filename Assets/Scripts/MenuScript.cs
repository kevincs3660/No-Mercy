using UnityEngine;
using System.Collections;

/// <summary>
/// Title screen script
/// </summary>
public class MenuScript : MonoBehaviour
{
	void OnMouseDown()
	{
		Application.LoadLevel("Intro Level");
	}

}