using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuCont : MonoBehaviour {

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
	    if (Input.GetKey(KeyCode.Return))
	    {
	        SceneManager.LoadScene("Intoduction");
	    }

	    if (Input.GetKey(KeyCode.Space))
	    {
	        SceneManager.LoadScene("Intoduction");
	    }

	    if (Input.GetKey(KeyCode.E))
	    {
	        SceneManager.LoadScene("Intoduction");
	    }

	    if (Input.GetKey(KeyCode.W))
	    {
	        SceneManager.LoadScene("Intoduction");
	    }
    }
}
