using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerBehaviour : MonoBehaviour
{


    public float Speed = 6.0f;

    Vector3 movement;
    Animator anim;
    Rigidbody rBod;

    // Use this for initialization
    void Start ()
    {
        anim = GetComponent<Animator>();
        rBod = GetComponent<Rigidbody>();
    }

    void FixedUpdate()
    {
        // fires every physics update

        // get input from axes
        // Raw input has fixed values -1, 0 or 1
        float horz = Input.GetAxisRaw("Horizontal");
        float vert = Input.GetAxisRaw("Vertical");


        Move(horz, vert);
    }


    void Move(float h, float v)
    {
        movement.Set(h, 0.0f, v);
        movement = movement.normalized * Speed * Time.deltaTime;

        rBod.MovePosition(transform.position + movement);

    }

}
