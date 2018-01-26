using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Laser : MonoBehaviour
{
    private GameObject laser, holder;
    private Vector3 laserScale;

    public float thickness;
    public float length;
    public Color color;

    public Material beamCol;

    // Use this for initialization
    void Start ()
    {
        laserScale = new Vector3(thickness, thickness, length);

        // holder is light emit
        holder = GameObject.FindGameObjectWithTag("LightEmit");

        // create pointer
        laser = GameObject.CreatePrimitive(PrimitiveType.Cube);
        laser.transform.parent = holder.transform;
        laser.transform.localScale = laserScale;
        laser.transform.localPosition = new Vector3(0f, 0f, length / 2);
        laser.transform.localRotation = Quaternion.identity;
        laser.layer = LayerMask.NameToLayer("Ignore Raycast");

        // set colour
        //beamCol.SetColor("_Color", color);
        laser.GetComponent<MeshRenderer>().material = beamCol;
        laser.GetComponent<BoxCollider>().enabled = false;
    }
	
	// Update is called once per frame
	void Update () {

        Animator an = GetComponentInChildren<Animator>();
	}
}
