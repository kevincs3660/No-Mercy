using UnityEngine;
using System.Collections;

public class Boss3DeadCheck : MonoBehaviour {
	
	private GameObject boss;
	public string bossname;
	public string level;
	
	// Use this for initialization
	void Start () {
		boss = GameObject.Find ("Boss3");
	}
	
	// Update is called once per frame
	void Update () 
	{
		if (boss == null)
			StartCoroutine (nextLevel ());
	}
	
	IEnumerator nextLevel (){
		
		float timer = 0;
		
		while (timer < 5f){
			
			//do this thing
			
			timer += Time.deltaTime;
			yield return null;
			
		}
		transform.parent.gameObject.AddComponent<GameWinScript>();
		//do this last thing
		
	}
}