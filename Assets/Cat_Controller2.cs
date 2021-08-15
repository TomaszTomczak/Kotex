using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cat_Controller2 : MonoBehaviour
{
    float speed = 15;
    float rotSpeed = 60;
    float rot = 0f;
    float gravity = 100;
    float pullspeed = 2;
    float jump = 0;
    float runspeed = 10;


    Vector3 moveDir = Vector3.zero;

    CharacterController controller;
    Animator anim;

    void Start()
    {
        controller = GetComponent<CharacterController>();
        anim = GetComponent<Animator>();
    }

    void Update()
    {
        Movement();
        GetInput();
        pulling();
        Jumping();
        Running();

    }

    void Movement()
    {
            if (controller.isGrounded)
        {
            if (Input.GetKey(KeyCode.W))
            {
                if (anim.GetBool("attacking") == true)
                {
                    return;
                }
                else if(anim.GetBool("attacking") == false)
                {
                    anim.SetBool("running", true);
                    anim.SetInteger("condition", 1);
                    moveDir = new Vector3(0, 0, 1);
                    moveDir *= speed;
                    moveDir = transform.TransformDirection(moveDir);
                }
            }
            if (Input.GetKeyUp(KeyCode.W))
            {
                anim.SetBool("running", false);
                anim.SetInteger ("condition", 0);
                moveDir = new Vector3(0, 0, 0);
            }
        }
        rot += Input.GetAxis("Horizontal") * rotSpeed * Time.deltaTime;
        transform.eulerAngles = new Vector3 (0, rot, 0);

        moveDir.y -= gravity * Time.deltaTime;
        controller.Move(moveDir * Time.deltaTime);
    }

    void GetInput()
    {
        if (controller.isGrounded)
        {
            if (Input.GetMouseButtonDown (0))
            {
                if (anim.GetBool ("running") == true)
                {
                    anim.SetBool ("running", false);
                    anim.SetInteger("condition", 0);
                }
                if (anim.GetBool ("running") == false)
                {
                    Attacking();
                }
            }
        }
    }
    
    void Attacking()
    {
        StartCoroutine(AttackRoutine());
    }

    IEnumerator AttackRoutine()
    {
        anim.SetBool("attacking", true);
        anim.SetInteger("condition", 2);
        yield return new WaitForSeconds(1);
        anim.SetInteger("condition", 0);
        anim.SetBool("attacking", false);
    }

    void pulling()
    {
        if (controller.isGrounded)
        {
            if (Input.GetKey(KeyCode.S))
            {
                anim.SetInteger("condition", 5);
                moveDir = new Vector3(0, 0, -5);
                moveDir *= pullspeed;
                moveDir = transform.TransformDirection(moveDir);
            }
            if (Input.GetKeyUp(KeyCode.S))
            {
                anim.SetInteger("condition", 0);
                moveDir = new Vector3(0, 0, 0);
            }
        }
    }
    void Jumping()
    {
        if (controller.isGrounded)
        {
            if (Input.GetKey(KeyCode.Space))
            {
                anim.SetInteger("condition", 3);
                moveDir = new Vector3(0, 3, 0);
                moveDir *= jump;
            }
            if (Input.GetKeyUp(KeyCode.Space))
            {
                anim.SetInteger("condition", 0);
                moveDir = new Vector3(0, 3, 0);
            }
        }
    }
    void Running()
    {
        if (controller.isGrounded)
        {
            if (Input.GetKey(KeyCode.LeftShift))
            {
                if (anim.GetInteger("condition") == 1)
                {
                    anim.SetInteger("walking", 4);
                    moveDir = new Vector3(0, 0, 4);
                    moveDir *= runspeed;
                    moveDir = transform.TransformDirection(moveDir);
                }
            }
            if (Input.GetKeyUp(KeyCode.LeftShift))
            {
                anim.SetInteger("walking", 0);
                moveDir = new Vector3(0, 0, 0);
            }
        }
        moveDir.y -= gravity * Time.deltaTime;
        controller.Move(moveDir * Time.deltaTime);
    }
}