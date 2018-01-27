using System.Collections;
using System;
using UnityEngine;

public enum colours
{
    white, red, green, blue, yellow, orange, purple
}



public class Laser : MonoBehaviour
{

    private GameObject laser, holder;
    private Vector3 laserScale;

    public float thickness;
    public float length;
    public colours color;

    public GameObject pointLight, emitLight;
    MeshRenderer[] meshes;

    public Material[] colouredMat;

    private Color[] shade =
    {
        Color.white,
        Color.red,
        Color.green,
        Color.blue,
        Color.yellow,
        new Color(1.0f, 0.5f, 0.0f),
        new Color(0.5f, 0.0f, 1.0f)
    };

    // Use this for initialization
    void Start ()
    {
        laserScale = new Vector3(thickness, thickness, length);

        // holder is light emit
        holder = gameObject;

        // create pointer
        laser = transform.GetChild(0).gameObject;
        laser.transform.parent = holder.transform;
        laser.transform.localScale = laserScale;
        laser.transform.localPosition = new Vector3(0f, 0f, length / 10);
        laser.transform.localRotation = Quaternion.identity;
        laser.layer = LayerMask.NameToLayer("Ignore Raycast");

        // set colour
        //beamCol.SetColor("_Color", color);
        meshes = laser.GetComponentsInChildren<MeshRenderer>();

        // change colour 
        foreach (MeshRenderer m in meshes)
        {
            Debug.Log((int)color);
            m.material = colouredMat[(int)color];
        }

        Enum.GetName(typeof(colours), color);

        // change light col
        pointLight.GetComponent<Light>().color = shade[(int)color];
        emitLight.GetComponent<Light>().color = shade[(int)color];
    }
	
	// Update is called once per frame
	void Update ()
    {
        // change size per raycast
        laserScale = new Vector3(thickness, thickness, 10 * length);
        laser.transform.localScale = laserScale;
        laser.transform.localPosition = new Vector3(0f, 0f, (length * 10 / 2) + (transform.localScale.x /2));

        pointLight.transform.localPosition = new Vector3(0f, 0f, (length * 10)-1);

        Ray raycast = new Ray(transform.position, transform.forward);
        RaycastHit hit;
        bool bHit = Physics.Raycast(raycast, out hit);

        if (bHit)
        {
            length = hit.distance;
            Debug.Log("Leng" + length);
        }

    }
}
