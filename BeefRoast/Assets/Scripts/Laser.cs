using System.Collections.Generic;
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

    List<GameObject> objSpawned = new List<GameObject>();

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

        ChangeColour(color);

    }
	
    void ChangeColour(colours c)
    {
        // change colour 
        foreach (MeshRenderer m in meshes)
        {
            m.material = colouredMat[(int)c];
        }

        Enum.GetName(typeof(colours), c);

        // change light col
        pointLight.GetComponent<Light>().color = shade[(int)c];
        emitLight.GetComponent<Light>().color = shade[(int)c];
    }

	// Update is called once per frame
	void Update ()
    {
        ChangeColour(color);


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

            if (transform.parent != null)
            {
                length *= 0.5f;
            }

            Transform target = hit.transform;

            // if hit a filter, check if filter has an emitter already.
            if (target.gameObject.tag == "Filter")
            {
                
                Transform spawner = hit.transform.GetChild(0);

                if (spawner.childCount < 1)
                {
                    Debug.Log("here");
                    // spawn an emitter
                    int chosenColour = -1;
                    // get colour from spawner
                    for(int i =0; i < shade.Length; i++)
                    {
                        if (target.GetComponent<MeshRenderer>().material.color.r == shade[i].r &&
                            target.GetComponent<MeshRenderer>().material.color.g == shade[i].g &&
                            target.GetComponent<MeshRenderer>().material.color.b == shade[i].b)
                        {
                            Debug.Log("UASS");
                            chosenColour = i;
                            break;
                        }
                    }

                    GameObject colouredBeam = Instantiate(emitPrefab) as GameObject;
                    colouredBeam.GetComponent<Laser>().color = (colours)chosenColour;
                    colouredBeam.GetComponent<Laser>().emitPrefab = emitPrefab;
                    // false = worldposition doesn't stay
                    colouredBeam.transform.SetParent(spawner, false);

                    objSpawned.Add(colouredBeam);
                }

                spawner.position = pointLight.transform.position;

                spawner.localPosition = new Vector3(spawner.localPosition.x,
                    spawner.localPosition.y,
                    -spawner.localPosition.z - 0.1f);
            }
            else
            {
                foreach(GameObject obj in objSpawned)
                {
                    DestroyImmediate(obj);
                }
                objSpawned.Clear();
            }
        }

    }
}
