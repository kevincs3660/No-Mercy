using UnityEngine;
using System.Collections;

/// <summary>
/// Title screen script
/// </summary>
public class RestartPressScript : MonoBehaviour
{
	public AudioClip sound;
	void OnMouseDown()
	{
		AudioSource.PlayClipAtPoint (sound, transform.position, 0.5f);
		Application.LoadLevel (Application.loadedLevel);
		Time.timeScale = 1;
	}
	
}