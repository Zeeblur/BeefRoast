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
    bool pushable = false;
    bool changeDirecUp = true;
    bool changeDirecDown = false;

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


        if (canPush && Input.GetKey("space"))
        {

            pushable = true;

            Debug.Log("push");
            
            // Getting vector direction
            Vector3 forward = transform.forward;
            forward.y = 0;

            float headingAngle = Quaternion.LookRotation(forward).eulerAngles.y;

            // Checking if he's facing north/south or east/west
            if ((headingAngle > 45f && headingAngle < 315f) || (headingAngle > 135f &&  headingAngle < 225f ))
            {
                Debug.Log("North/South");
            }
            else
            {
                Debug.Log("East/West");
            }

            var push = new Push();
            push.MoveBlock(movement);

            
        }
        else
        {
            pushable = false;
        }

        float horz = Input.GetAxisRaw("Horizontal");
        float vert = Input.GetAxisRaw("Vertical");

        Move(horz, vert);

        animating(horz, vert);

    }


    void Move(float h, float v)
    {
        // Allows you to change walking direction


        movement.Set(h, 0.0f, v);
        movement = movement.normalized * Speed * Time.deltaTime;

        rBod.MovePosition(transform.position + movement);

        if (h != 0 || v != 0)
        {
            float angle = Mathf.Atan2(h, v) * Mathf.Rad2Deg;
            Quaternion rotation = Quaternion.Euler(0, angle, 0);
            Quaternion newRotation = Quaternion.Slerp(this.transform.rotation, rotation, 0.2f);
            rBod.MoveRotation(newRotation);
        }



    }

    void animating(float h, float v)
    {
        bool walking = (h != 0f || v != 0f) && !pushable;

        anim.SetBool("walking", walking);

        anim.SetBool("pushing", pushable);

        Debug.Log(pushable);

    }


    void OnCollisionEnter(Collision col)
    {
        Debug.Log("Collision Happened");

        if (col.gameObject.tag == "Pushable")
        {
            canPush = true;
            Debug.Log("Can push true");
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

    }


}
