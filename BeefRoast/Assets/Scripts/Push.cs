using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Push : MonoBehaviour
{
    public float Speed = 6.0f;

    Vector3 moveBlock;
    public Animator anim;
    Rigidbody rb;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    // Called if player is adjacent to a block and 
    //  A key is being held to push the block
    public void MoveBlock(Vector3 playerMove)
    {
            moveBlock.x = playerMove.x;
            moveBlock.z = playerMove.z;
    }

}
