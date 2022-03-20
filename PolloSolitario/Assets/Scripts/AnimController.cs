using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimController : MonoBehaviour
{
    // Animator component of the player
    Animator animator;
    //Hash that represents the walking state in the animator
    int isWalkingHash;
    //Hash that represents running state in animator
    int isRunningHash;

    //Grabs references for the animator ocmponent and both hashes
    void Start()
    {
        animator = GetComponent<Animator>();
        isWalkingHash = Animator.StringToHash("Walk");
        isRunningHash = Animator.StringToHash("Run");
    }

    void Update()
    {
        bool isRunning = animator.GetBool(isRunningHash);
        bool isWalking = animator.GetBool(isWalkingHash);
        bool forwardPressed = Input.GetKey("w");
        bool leftPressed = Input.GetKey("a");
        bool downPressed = Input.GetKey("s");
        bool rightPressed = Input.GetKey("d");
        bool runPressed = Input.GetKey("left shift");

        // if player presses w
        if(!isWalking && (forwardPressed || leftPressed || downPressed || rightPressed))
        {
            animator.SetBool(isWalkingHash, true);
        }
        
        // if player is not pressing directional buttons
        if(isWalking && (!forwardPressed && !leftPressed && !downPressed && !rightPressed))
        {
            animator.SetBool(isWalkingHash, false);
        }

        // if player is walking and presses left shit
        if(!isRunning &&((forwardPressed || leftPressed || downPressed || rightPressed) && runPressed))
        {
            animator.SetBool(isRunningHash, true);
        }
        //if the player releases the shift button to sprint
        if(isRunning && !runPressed)
        {
            animator.SetBool(isRunningHash, false);
        }
    }
}
