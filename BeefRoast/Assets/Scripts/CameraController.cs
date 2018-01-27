using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour {

    private Quaternion rotation;
    private Vector3 offset;

	// Use this for initialization
	void Start ()
    {
        // initial rot
        rotation = transform.rotation;

        offset = transform.localPosition;

    }
	
	// Update is called once per frame
	void Update ()
    {
        transform.rotation = rotation;
        transform.position = transform.parent.position + offset;
	}
}
