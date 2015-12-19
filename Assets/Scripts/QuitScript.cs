using UnityEngine;
using System.Collections;

/// <summary>
/// Title screen script
/// </summary>
public class QuitScript : MonoBehaviour
{
	public AudioClip sound;
	void OnMouseDown()
	{
		AudioSource.PlayClipAtPoint (sound, transform.position, 0.5f);
		//Debug.Log ("quitting");
		Application.Quit ();
	}
	
}