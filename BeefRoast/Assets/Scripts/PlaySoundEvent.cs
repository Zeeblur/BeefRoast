using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlaySoundEvent : MonoBehaviour {

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}


	public void playSound(string path){

		GetComponent<FMODUnity.StudioEventEmitter>().Play();

		FMODUnity.RuntimeManager.PlayOneShot (path, GetComponent<Transform> ().position);
	}
}
