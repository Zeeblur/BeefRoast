using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StoneDragSound : MonoBehaviour {


	bool playsound = false;

	public GameObject player;
	
	// Update is called once per frame
	void Update () {

//		if (player.GetComponent<PlayerBehaviour>().ismoving && 

		if (player.GetComponent<PlayerBehaviour> ().ismoving && player.GetComponent<PlayerBehaviour> ().pooshing && playsound == false) {
			GetComponent<FMODUnity.StudioEventEmitter> ().Play ();
			playsound = true;

		}


		if (!player.GetComponent<PlayerBehaviour> ().ismoving || !player.GetComponent<PlayerBehaviour> ().pooshing && playsound) {
			GetComponent<FMODUnity.StudioEventEmitter> ().Stop ();
			playsound = false;
		}

		/// if pooshing and moving and the sound is not already playing, play sound (bool true);

		/// if not pushing or not moving and the sound IS playing, stop sound, bool false



	}
}
