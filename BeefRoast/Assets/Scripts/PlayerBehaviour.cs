using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerBehaviour : MonoBehaviour
{

    public float Speed = 3.0f;

    public float RotateSpeed = 0.000001f;

    Vector3 movement;
    public Animator anim;
    Rigidbody rBod;
    Rigidbody parentrBod;

	bool push = false;
    bool canPush = false;
    bool pushable = false;
    bool rotatable = false;
    bool changeDirecUp = true;
    bool changeDirecAcross = true;
    private bool changeRotate = true;
    string nsewDir;


	bool canPoosh = false;
	public List<GameObject> pooshables = new List<GameObject>();
	GameObject closestPooshable;
	bool pooshing = false;

    private bool canRotate;
    public List<GameObject> rotatables = new List<GameObject>();
    GameObject closestRotatable;
    public bool rotating;

    public Material clothes;
    public Material invise;

    // Use this for initialization
    void Start()
    {

        rBod = GetComponent<Rigidbody>();

    }

    public void changeColour(Color c)
    {
        // clothes
        invise.color = c;
        GameObject.FindGameObjectWithTag("clothes").GetComponent<MeshRenderer>().material = invise;
    }

    void Update(){

		if (Input.GetKeyDown("t") && pooshables.Count!=0 && !pooshing) {

            Debug.Log(pooshables.Count+": is pooshable count");
			if (pooshables.Count == 1) {
				// lock to pooshable 1
				LockToPush (pooshables[0].transform);

			} else {
				// find the closest pushable parent block

				closestPooshable = pooshables [0];
				foreach (var p in pooshables) {
					if (Vector3.Distance (this.transform.position, p.transform.position)
						< Vector3.Distance (this.transform.position, closestPooshable.transform.position)) {
						closestPooshable = p;
					}
				}
				// lock to closest pushable
				LockToPush (closestPooshable.transform);

			}

			pooshing = true;

		} else if (Input.GetKeyDown ("t") && pooshing) {
			// release push
			ReleasePush();
			pooshing = false;
			
		}

        // Rotatables
	    if (Input.GetKeyDown("r") && rotatables.Count != 0 && !rotating)
	    {

	        if (rotatables.Count == 1)
	        {
	            // lock to pooshable 1
	            LockToRotate(rotatables[0].transform);
	        }
	        else
	        {
	            // find the closest pushable parent block

	            closestRotatable = rotatables[0];
	            foreach (var p in rotatables)
	            {
	                if (Vector3.Distance(transform.position, p.transform.position)
	                    < Vector3.Distance(transform.position, closestRotatable.transform.position))
	                {
	                    closestRotatable = p;
	                }
	            }
	            // lock to closest pushable
	            LockToRotate(closestRotatable.transform);

	        }

	        rotating = true;

	    }
	    else if (Input.GetKeyDown("r") && rotating)
	    {
	        // release push
	        ReleaseRotate();
	        rotating = false;

	    }
    }

	void ReleasePush(){

		// change the block parent to the main heirarchy
		gameObject.transform.GetChild(gameObject.transform.childCount-1).transform.parent = null;
		changeDirecAcross = true;
		changeDirecUp = true;
	    rBod.constraints = RigidbodyConstraints.None;
	}

    void ReleaseRotate()
    {
        Debug.Log("Release Rotate Class");
        // change the block parent to the main heirarchy
        gameObject.transform.GetChild(gameObject.transform.childCount - 1).transform.parent = null;
        changeRotate = true;
        rBod.constraints = RigidbodyConstraints.None;
    }

    void FixedUpdate()
    {
        // fires every physics update

        // get input from axes
        // Raw input has fixed values -1, 0 or 1

        float horz = Input.GetAxisRaw("Horizontal");
        float vert = Input.GetAxisRaw("Vertical");

        // Getting vector direction
        Vector3 forward = transform.forward;
        forward.y = 0;
        float headingAngle = Quaternion.LookRotation(forward).eulerAngles.y;

        /*
        // Checking if facing north/south or east/west
        if ((headingAngle >= 45f && headingAngle <= 315f && changeDirecAcross == false) || (headingAngle >= 135f && headingAngle <= 225f && changeDirecAcross == false))
        {
            Debug.Log("North/South");
            Move(0, vert);
        }
        else if ((headingAngle >= 225f && headingAngle <= 315f && changeDirecUp == false) || (headingAngle >= 45f && headingAngle <= 135f && changeDirecUp == false))
        {
            Debug.Log("East/West");
            Move(horz, 0);
        }
        */

        // Stopping char from moving sideways with block
        if (changeDirecAcross == false)
        {
            Debug.Log("vert");
            Move(vert, false);
            //rBod.constraints = RigidbodyConstraints.FreezeAll;
        }else if (changeRotate == false)
        {
            //locked on
            MoveRotate(horz);
            // rBod.constraints = RigidbodyConstraints.FreezeAll;
        }
        else
        {
            Move(horz, vert);
            animating(horz, vert);
        }

      

 

    }

    // Changes rotation of player to match block
	void LockToPush(Transform sideToLockOnTo){
		Vector3 temp = transform.position;
		temp.z = sideToLockOnTo.transform.position.z;
		temp.x = sideToLockOnTo.transform.position.x;

		transform.position = temp;
		transform.rotation = sideToLockOnTo.transform.rotation;

	    // Getting vector direction
	    Vector3 forward = transform.forward;
	    forward.y = 0;

        /*
        // THROWING OUT REALLY STRANGE ANGLES (90.0003Degrees etc)
	    float headingAngle = Quaternion.LookRotation(forward).eulerAngles.y;
        Debug.Log(headingAngle + ": heading angle");

	    // Checking if facing north/south/east/west
	    if (headingAngle <= 45f && headingAngle >= 315f || headingAngle >= 135f && headingAngle <= 225f)
	    {
	        changeDirecAcross = false;
            Debug.Log("N/S");
        }
	    else
	    {
	        changeDirecUp = false;
            Debug.Log("E/W");
        }
        */

	    changeDirecAcross = false;

        // change brick parent to player
	    chosenBlock = sideToLockOnTo.parent.transform;
	    chosenBlock.parent = gameObject.transform;

	}

    private Transform chosenBlock;

    // Changes rotation of player to match block
    void LockToRotate(Transform sideToLockOnTo)
    {
        Debug.Log("LOCKINGTOROTATE");

        Vector3 temp = transform.position;
        temp.z = sideToLockOnTo.transform.position.z;
        temp.x = sideToLockOnTo.transform.position.x;

        transform.position = temp;
        transform.rotation = sideToLockOnTo.transform.rotation;

        changeRotate = false;

        // change brick parent to player
        chosenBlock = sideToLockOnTo.parent.transform;
        chosenBlock.SetParent(gameObject.transform, true);

        //rBod.constraints = RigidbodyConstraints.FreezeRotation;
    }


    void Move(float h, float v)
    { 
        Vector3 orient = GameObject.FindGameObjectWithTag("MainCamera").transform.forward;


        movement = (orient * v) + (GameObject.FindGameObjectWithTag("MainCamera").transform.right * h) ;

        movement = movement.normalized;

        if (changeRotate == false)
        {
            Debug.Log("SPEED REDUCED");
            rBod.MovePosition(transform.position + (movement * RotateSpeed * Time.deltaTime));
        }
        else
        {
            rBod.MovePosition(transform.position + (movement * Speed * Time.deltaTime));
        }
        

        if (h != 0 || v != 0)
        {
            Quaternion rotation = Quaternion.identity;
            rotation.SetLookRotation(movement, Vector3.up);
            Quaternion newRotation = Quaternion.Slerp(this.transform.rotation, rotation, 0.2f);
            rBod.MoveRotation(newRotation);
        }

        rBod.rotation = new Quaternion(0, rBod.rotation.y, 0, rBod.rotation.w);
    }

    void Move(float dir, bool horizon)
    {
        Debug.Log("horiztom " + horizon);
        Vector3 orient;
        if (horizon)
        {
            orient = transform.right;

            
        }
        else
        {
            orient = transform.forward;
        }


        movement = (orient * dir);
        movement = movement.normalized;
        rBod.MovePosition(transform.position + (movement * Speed * Time.deltaTime));

    }

    void MoveRotate(float h)
    {
       
        if (h != 0 )
        {
            Quaternion rotation = Quaternion.identity;
           // rotation.Rot(movement, Vector3.up);
            transform.RotateAround(chosenBlock.transform.position, Vector3.up, -h);
            Quaternion newRotation = Quaternion.Slerp(this.transform.rotation, rotation, 0.2f);
            rBod.MoveRotation(newRotation);
        }

        rBod.rotation = new Quaternion(0, rBod.rotation.y, 0, rBod.rotation.w);
    }

    void animating(float h, float v)
    {
        bool walking = (h != 0f || v != 0f) && !pushable;

        anim.SetBool("walking", walking);

        anim.SetBool("pushing", pooshing);

    }

    void OnTriggerEnter(Collider col){

		
		if (col.gameObject.tag == "Side")
		{
		    Debug.Log("Pooshable");
		    canPoosh = true;
		    pooshables.Add(col.gameObject);
        }

        if (col.gameObject.tag == "RotatableSide")
        {
            Debug.Log("Rotatable");
            canRotate = true;
            rotatables.Add(col.gameObject);
        }
    }

	void OnTriggerExit(Collider col){

		if (col.gameObject.tag == "Side")
		{
			Debug.Log ("SideCode");
			canPoosh = true;

			pooshables.Remove(col.gameObject);

		}

	    if (col.gameObject.tag == "RotatableSide")
	    {
	        Debug.Log("SideCode");
	        canRotate = true;

	        rotatables.Remove(col.gameObject);

	    }
    }

    void OnCollisionEnter(Collision col)
    {
        Debug.Log("Collision Happened");

        if (col.gameObject.tag == "Pushable")
        {
            canPush = true;
            //            Debug.Log("Can push true");
        }

        if (col.gameObject.tag == "Rotatable")
        {
            canRotate = true;
            //            Debug.Log("Can rotate true");
        }

    }

    void OnCollisionExit(Collision col)
    {
        if (col.gameObject.tag == "Pushable")
        {
            canPush = false;
            Debug.Log("Can push false");
        }

        if (col.gameObject.tag == "Rotatable")
        {
            canRotate = false;
            Debug.Log("Can rotate false");
        }
    }

	public void playFootSteps(string path){

		GetComponent<FMODUnity.StudioEventEmitter>().Play();

		FMODUnity.RuntimeManager.PlayOneShot (path, GetComponent<Transform> ().position);
	}


}
