using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlockMover : MonoBehaviour {




	void Update (){
	}


	void OnCollisionEnter (Collision col)
	{
		if(col.gameObject.name == "Player")
		{
			Debug.Log ("BANG");
		}

	}

	void OnCollisionExit (Collision col)
	{
		if(col.gameObject.name == "Player")
		{
			Debug.Log ("BONG");
		}

	}



}
