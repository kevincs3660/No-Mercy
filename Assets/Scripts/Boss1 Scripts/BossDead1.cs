﻿using UnityEngine;
using System.Collections;

public class BossDead1 : MonoBehaviour {

	private GameObject boss;

	// Use this for initialization
	void Start () {
		boss = GameObject.Find ("Boss");
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
		Application.LoadLevel ("Level2_A");
		//do this last thing
		
	}
}
