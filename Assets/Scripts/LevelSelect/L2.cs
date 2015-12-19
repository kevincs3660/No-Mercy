using UnityEngine;
using System.Collections;

/// <summary>
/// Title screen script
/// </summary>
public class L2: MonoBehaviour
{
	public AudioClip sound;
	void OnMouseDown()
	{
		AudioSource.PlayClipAtPoint (sound, transform.position, 0.5f);
		Application.LoadLevel ("Level1_B");
	}
	
}