using System.Collections;
using System;
using UnityEngine;

public enum colours
{
    white, red, green, blue, yellow, orange, purple
}



public class Laser : MonoBehaviour
{

    public GameObject emitPrefab;

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

            Transform target = hit.transform;

            // if hit a filter, check if filter has an emitter already.
            if (target.gameObject.tag == "Filter")
            {
                
                Transform spawner = hit.transform.GetChild(0);

                Debug.Log("name: " + spawner.name);

                if (spawner.childCount < 1)
                {
                    Debug.Log("here");
                    // spawn an emitter
                    int chosenColour = -1;
                    // get colour from spawner
                    for(int i =0; i < shade.Length; i++)
                    {
                        if (target.GetComponent<MeshRenderer>().material.color == shade[i])
                        {
                            chosenColour = i;
                            return;
                        }
                    }

                    GameObject colouredBeam = Instantiate(emitPrefab) as GameObject;
                    colouredBeam.GetComponent<Laser>().color = (colours)chosenColour;
                    colouredBeam.transform.SetParent(spawner, false);
                }
            }
        }

    }
}
