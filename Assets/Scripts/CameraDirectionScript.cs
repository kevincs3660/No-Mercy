using UnityEngine;
using System.Collections;

public class CameraDirectionScript : MonoBehaviour {
		
	public float timeUntilSwitch = 5;
	public Vector2 newDirection = new Vector2(0,1);
	public Vector2 cameraSpeed = new Vector2(1,1);

	void Start()
	{
		this.gameObject.GetComponent<SpriteRenderer>().enabled = false;
	}

	// Update is called once per frame
	void Update () {

		if(GetComponent<Renderer>().IsVisibleFrom(Camera.main))
		{
			//GameObject camera = GameObject.Find ("Player");
			ScrollingScript script = Camera.main.GetComponent<ScrollingScript>();
			if(script != null)
			{
				StartCoroutine(changeDirection(script));
			}
		}
	}

	IEnumerator changeDirection(ScrollingScript script)
	{
		float timer = 0;

		while (timer < timeUntilSwitch)
		{
			timer += Time.deltaTime;
			yield return null;
		}
		script.direction = newDirection;
		script.speed = cameraSpeed;
		Destroy (this.gameObject);
	}
}
