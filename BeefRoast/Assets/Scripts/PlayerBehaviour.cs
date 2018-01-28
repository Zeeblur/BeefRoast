using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerBehaviour : MonoBehaviour
{

    public float Speed = 6.0f;

    Vector3 movement;
    public Animator anim;
    Rigidbody rBod;

	bool push = false;
	bool canRotate = false;
    bool canPush = false;
    bool pushable = false;
    bool rotatable = false;
    bool changeDirecUp = true;
    bool changeDirecAcross = true;

	public List<GameObject> pushables = new List<GameObject>();


	bool canPoosh = false;
	public List<GameObject> pooshables = new List<GameObject>();
	GameObject closestPooshable;


	bool pooshing = false;



	GameObject closestPushable;

    // Use this for initialization
    void Start()
    {

        rBod = GetComponent<Rigidbody>();

		// add all pushable objects to pushables list
		foreach(GameObject g in GameObject.FindGameObjectsWithTag("Pushable")) {
			pushables.Add (g);
		}

    }


	void Update(){

//		closestPushable = pushables [0];
//		// set shortest distance to the distance between the player and the first pushable
//		float shortestDistance = Vector3.Distance (this.transform.position, pushables[0].transform.position);
//		//check for the shortest distance
//		foreach (GameObject g in pushables) {
//
//			if (Vector3.Distance(g.transform.position, this.transform.position) 
//				< Vector3.Distance(closestPushable.transform.position, this.transform.position)){
//				closestPushable = g;
//				}
//		}
//		Debug.Log ("the closest block is " + Vector3.Distance(closestPushable.transform.position, this.transform.position) + " away");

		if (Input.GetKeyDown("t") && pooshables.Count!=0 && !pooshing) {

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
	}

	void ReleasePush(){

		// change the block parent to the main heirarchy
		gameObject.transform.GetChild(gameObject.transform.childCount-1).transform.parent = null;
		changeDirecAcross = true;
		changeDirecUp = true;
	}



    void FixedUpdate()
    {
        // fires every physics update

        // get input from axes
        // Raw input has fixed values -1, 0 or 1

        // Checking for push
        if (canPush && Input.GetKey("space"))
        {

            pushable = true;

            Debug.Log("push");
            
            // Getting vector direction
            Vector3 forward = transform.forward;
            forward.y = 0;

            float headingAngle = Quaternion.LookRotation(forward).eulerAngles.y;

            // Checking if facing north/south or east/west
            if ((headingAngle > 45f && headingAngle < 315f) || (headingAngle > 135f &&  headingAngle < 225f ))
            {
                Debug.Log("North/South");
                changeDirecAcross = false;
            }
            else
            {
                Debug.Log("East/West");
                changeDirecUp = false;
            }

            var push = new Push();
            push.MoveBlock(movement);
            
        } // Checking for rotate
        else if (canRotate && Input.GetKey("space"))
        {
            rotatable = true;

            Debug.Log("Rotate");

        }
        else
        {
            rotatable = false;
            pushable = false;
            changeDirecUp = true;
            changeDirecAcross = true;
        }

        float horz = Input.GetAxisRaw("Horizontal");
        float vert = Input.GetAxisRaw("Vertical");

        // Stopping char from moving sideways with block
        if (changeDirecUp == false)
        {
            Move(horz, 0);
        }
		else if (changeDirecAcross == false)
        {
            Move(0, vert);
        }
        else
        {
            Move(horz, vert);
        }
        
        animating(horz, vert);

    }




	void LockToPush(Transform sideToLockOnTo){

//		Debug.Log (sideToLockOnTo.gameObject.name);

		Vector3 temp = transform.position;
		temp.z = sideToLockOnTo.transform.position.z;
		temp.x = sideToLockOnTo.transform.position.x;

		transform.position = temp;
		transform.rotation = sideToLockOnTo.transform.rotation;


		changeDirecAcross = false;
		changeDirecUp = false;

		// change brick parent to player
		sideToLockOnTo.parent.transform.parent = this.gameObject.transform;


	}


    void Move(float h, float v)
    {


        Vector3 orient = GameObject.FindGameObjectWithTag("MainCamera").transform.forward;


        movement = (orient * v) + (GameObject.FindGameObjectWithTag("MainCamera").transform.right * h) ;

        movement = movement.normalized;

        rBod.MovePosition(transform.position + (movement * Speed * Time.deltaTime));

        if (h != 0 || v != 0)
        {
            Quaternion rotation = Quaternion.identity;
            rotation.SetLookRotation(movement, Vector3.up);
            Quaternion newRotation = Quaternion.Slerp(this.transform.rotation, rotation, 0.2f);
            rBod.MoveRotation(newRotation);

        }

        rBod.rotation = new Quaternion(0, rBod.rotation.y, 0, rBod.rotation.w);
    }

    void animating(float h, float v)
    {
        bool walking = (h != 0f || v != 0f) && !pushable;

        anim.SetBool("walking", walking);

        anim.SetBool("pushing", pushable);


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


	void OnTriggerEnter(Collider col){

//		if (col.GetType () != typeof(MeshCollider)) {
//			return;
//		}

		Debug.Log ("BAWS");
		if (col.gameObject.tag == "Side")
		{
			canPoosh = true;

			pooshables.Add (col.gameObject);
		}
	}

	void OnTriggerExit(Collider col){

		if (col.gameObject.tag == "Side")
		{
			Debug.Log ("BOOS");
			canPoosh = true;

			pooshables.Remove(col.gameObject);

		}
	}



    void OnCollisionExit(Collision col)
    {
        Debug.Log("Collision stopped");

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

    // Joining push block and character
    // Joining rotate block and charcter
    // Rotating around the centre of a block

}
