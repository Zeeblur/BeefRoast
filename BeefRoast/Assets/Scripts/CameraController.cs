using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour {

    private Quaternion initialRot;
    private Quaternion rotation;
    private Vector3 offset;

    float angle = 0;


    public float camSense = 0.05f;

	// Use this for initialization
	void Start ()
    {
        // initial rot

        initialRot = transform.rotation;

        offset = transform.localPosition;

    }
	
	// Update is called once per frame
	void Update ()
    {
        float horz = Input.GetAxisRaw("RH");
        float vert = Input.GetAxisRaw("RV");

        transform.rotation = initialRot;
        // transform.rotation = rotation;

        angle += Input.GetAxisRaw("RV") * 60;
        var angV = Input.GetAxisRaw("RH") * 45;
        transform.Rotate(new Vector3(angV * camSense * 10, 0, 0));



        transform.position = transform.parent.position + offset;

        transform.RotateAround(transform.parent.position, Vector3.up, angle * camSense);

        //transform.LookAt(transform.parent.position);
        rotation = transform.rotation;
        transform.rotation = (rotation);
        //Debug.Log(Input.GetAxis("RH") + " left: " + Input.GetAxis("RV"));
    }
}
