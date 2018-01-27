using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerBehaviour : MonoBehaviour
{

    public float Speed = 6.0f;

    Vector3 movement;
    public Animator anim;
    Rigidbody rBod;

	bool canPush = false;
	bool push = false;

    // Use this for initialization
    void Start ()
    {

        rBod = GetComponent<Rigidbody>();


    }


	void Update(){

		if (Input.GetKeyDown ("p")) {

			GetComponent<FMODUnity.StudioEventEmitter>().Play();
			Debug.Log ("play");

		}
	}

    void FixedUpdate()
    {
        // fires every physics update

        // get input from axes
        // Raw input has fixed values -1, 0 or 1

		if (canPush && Input.GetKey ("space")) {

			push = true;

			Debug.Log ("push");

		} else {
			push = false;
		}

        float horz = Input.GetAxisRaw("Horizontal");
        float vert = Input.GetAxisRaw("Vertical");

        Move(horz, vert);

		animating (horz, vert);

    }


    void Move(float h, float v)
    {
        movement.Set(h, 0.0f, v);
        movement = movement.normalized * Speed * Time.deltaTime;

        rBod.MovePosition(transform.position + movement);

		if (h != 0 || v != 0) {
			float angle = Mathf.Atan2 (h, v) * Mathf.Rad2Deg;
			Quaternion rotation = Quaternion.Euler (0, angle, 0);
			Quaternion newRotation = Quaternion.Slerp (this.transform.rotation, rotation, 0.2f);
			rBod.MoveRotation (newRotation);
		}

    }

	void animating(float h, float v){
		bool walking = (h != 0f || v != 0f) && !push;
	
		anim.SetBool ("walking", walking);



		anim.SetBool ("pushing", push);



//		Debug.Log (push);

	}
		

	void OnCollisionEnter (Collision col)
	{

		if(col.gameObject.tag == "Pushable")
		{
			canPush = true;
		}

	}

	void OnCollisionExit (Collision col)
	{
		if (col.gameObject.tag == "Pushable")
		{
			canPush = false;
		}

	}

	public void playFootSteps(string path){

		GetComponent<FMODUnity.StudioEventEmitter>().Play();

		FMODUnity.RuntimeManager.PlayOneShot (path, GetComponent<Transform> ().position);
	}


}
