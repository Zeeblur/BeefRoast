using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerBehaviour : MonoBehaviour
{

    public float Speed = 6.0f;

    Vector3 movement;
    public Animator anim;
    Rigidbody rBod;

    bool canRotate = false;
    bool canPush = false;
    bool pushable = false;
    bool rotatable = false;
    bool changeDirecUp = true;
    bool changeDirecAcross = true;

    // player transform


    // Use this for initialization
    void Start()
    {

        rBod = GetComponent<Rigidbody>();

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
        }else if (changeDirecAcross == false)
        {
            Move(0, vert);
        }
        else
        {
            Move(horz, vert);
        }
        
        animating(horz, vert);

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

       // Debug.Log(pushable);

    }


    void OnCollisionEnter(Collision col)
    {
        Debug.Log("Collision Happened");

        if (col.gameObject.tag == "Pushable")
        {
            canPush = true;
            Debug.Log("Can push true");
        }

        if (col.gameObject.tag == "Rotatable")
        {
            canRotate = true;
            Debug.Log("Can rotate true");
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

    // Joining push block and character
    // Joining rotate block and charcter
    // Rotating around the centre of a block

}
