using UnityEngine;
using System.Collections;

/// <summary>
/// Title screen script
/// </summary>
public class ExitPressScript : MonoBehaviour
{
	public AudioClip sound;
	void OnMouseDown()
	{
		//Debug.Log ("Doing the thing");
		AudioSource.PlayClipAtPoint (sound, transform.position, 0.5f);
		Application.LoadLevel ("MainMenu");
		Time.timeScale = 1;
	}
	
}