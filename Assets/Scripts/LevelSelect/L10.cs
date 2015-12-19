using UnityEngine;
using System.Collections;

/// <summary>
/// Title screen script
/// </summary>
public class L10: MonoBehaviour
{
	public AudioClip sound;
	void OnMouseDown()
	{
		AudioSource.PlayClipAtPoint (sound, transform.position, 0.5f);
		Application.LoadLevel ("Level3_B");
	}
	
}