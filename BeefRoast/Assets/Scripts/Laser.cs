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
    public GameObject pointLight;
    MeshRenderer[] meshes;

    // Use this for initialization
    void Start ()
    {
        laserScale = new Vector3(thickness, thickness, length);

        // holder is light emit
        holder = GameObject.FindGameObjectWithTag("LightEmit");

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
            beamCol.SetColor("_Color", color);
            beamCol.SetColor("_EmissionColor", color);
            m.material = beamCol;
        }

        // change light col
        pointLight.GetComponent<Light>().color = color;
        
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
