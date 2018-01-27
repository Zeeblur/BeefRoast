using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Push : MonoBehaviour
{
    public float Speed = 6.0f;

<<<<<<< HEAD
    Vector3 pushDir;
=======
    Vector3 moveBlock;
>>>>>>> 6407a181a86c6e5538e9da1fb9cea8331a7b9587
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
<<<<<<< HEAD
        pushDir = new Vector3(playerMove.x, 0, playerMove.z);

        
=======
            moveBlock.x = playerMove.x;
            moveBlock.z = playerMove.z;
>>>>>>> 6407a181a86c6e5538e9da1fb9cea8331a7b9587
    }

}
