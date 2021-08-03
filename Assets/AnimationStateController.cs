using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimationStateController : MonoBehaviour
{
    Animator animator;
    int isWalkingHash;
    int isRunningHash;
    int isJumpingHash;
    int isPullingHash;

    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponent<Animator>();

        // increaces performance
        isWalkingHash = Animator.StringToHash("isWalking");
        isRunningHash = Animator.StringToHash("isRunning");
        isJumpingHash = Animator.StringToHash("isJumping");
        isPullingHash = Animator.StringToHash("isPulling");
    }

    // Update is called once per frame
    void Update()
    {
        bool isPulling = animator.GetBool(isPullingHash);
        bool isJumping = animator.GetBool(isJumpingHash);
        bool isRunning = animator.GetBool(isRunningHash);
        bool isWalking = animator.GetBool(isWalkingHash);
        bool forwardPressed = Input.GetKey("w");
        bool runPressed = Input.GetKey("left shift");
        bool jumpPressed = Input.GetKey("space");
        bool pullPressed = Input.GetKey("s");

        // if player presses w key
        if (!isWalking && forwardPressed)
        {
            // then set the isWalking boolean to be true
            animator.SetBool(isWalkingHash, true);
        }

        //if player is not pressing w key
        if (isWalking && !forwardPressed)
        {
            //then set the isWalking boolean to be false
            animator.SetBool(isWalkingHash, false);
        }

        //if player is walking and not running and presses left shift
        if (!isRunning && runPressed)
        {
            // then set the isRunning boolean to be true
            animator.SetBool(isRunningHash, true);
        }

        // if player is running and stops running or stops walking
        if (isRunning && !runPressed)
        {
            // then set the isRunning boolean to be false
            animator.SetBool(isRunningHash, false);
        }

        // if player is walking and not jumping and presses space
        if (!isJumping && (forwardPressed && jumpPressed))
        {
            // then set the isJumping boolean to be true
            animator.SetBool(isJumpingHash, true);
        }

        // if player is jumping and stops jumping or stop walking 
        if (isJumping && (!forwardPressed || !jumpPressed))
        {
            //then set the isJumping boolean to be false
            animator.SetBool(isJumpingHash, false);
        }

        // if player presses s key
        if (!isPulling && pullPressed)
        {
            // then set the isPulling boolean to be true
            animator.SetBool(isPullingHash, true);
        }

        // if player is not pressing s key
        if (isPulling && !pullPressed)
        {
            // then set the isPulling boolean to be false
            animator.SetBool(isPullingHash, false);
        }
    }
}
