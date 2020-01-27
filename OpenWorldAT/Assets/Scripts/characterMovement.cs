﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class characterMovement : MonoBehaviour
{
    public float health = 100;

    //INPUT
    Vector2 input;
    public CharacterController controller;
    public Animator anim;
    public GameObject player;

    //CAMERA
    public Transform cam;
    Vector3 camF;
    Vector3 camR;

    //PHYSICS
    public float speed = 15;
    Vector3 intent;
    Vector3 velocity;
    Vector3 velocityXZ;
    public float accel = 15;
    public float jumpVel = 15;
    float turnSpeed;
    float turnSpeedLow = 7;
    float turnSpeedHigh = 20;

    //GRAVITY
    float grav = 10;
    bool grounded = false;

    // Use this for initialization
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        ManageInput();
        CalculateCamera();
        CalculateGround();
        DoMove();
        DoGravity();
        //DoJump();

        controller.Move(velocity * Time.deltaTime);

    }
    void ManageInput()
    {
        input = new Vector2(Input.GetAxis("Horizontal"), Input.GetAxis("Vertical"));
        input = Vector2.ClampMagnitude(input, 1);
    }

    void CalculateCamera()
    {
        camF = cam.forward;
        camR = cam.right;

        camF.y = 0;
        camR.y = 0;
        camF = camF.normalized;
        camR = camR.normalized;
    }

    void CalculateGround()
    {
        RaycastHit hit;
        if (Physics.Raycast(player.transform.position + Vector3.up * 0.1f, -Vector3.up, out hit, 0.2f))
        {
            grounded = true;

            //if (anim.GetBool("Jump") == true)
            //{
            //    anim.SetBool("Jump", false);
            //}
        }
        else
        {
            grounded = false;
        }
    }

    void DoMove()
    {
        intent = camF * input.y + camR * input.x;

        float tS = velocity.magnitude / 5;
        turnSpeed = Mathf.Lerp(turnSpeedHigh, turnSpeedLow, tS);

        if (input.magnitude > 0)
        {
            Debug.Log("yes");
            Quaternion rot = Quaternion.LookRotation(intent);
            player.transform.rotation = Quaternion.Lerp(player.transform.rotation, rot, Time.deltaTime * turnSpeed);
            anim.SetBool("Running", true);

        }
        else
        {
            anim.SetBool("Running", false);
        }

        velocityXZ = velocity;
        velocityXZ.y = 0;
        velocityXZ = Vector3.Lerp(velocityXZ, player.transform.forward * input.magnitude * speed, accel * Time.deltaTime);
        velocity = new Vector3(velocityXZ.x, velocity.y, velocityXZ.z);

    }

    void DoGravity()
    {
        if (grounded)
        {
            velocity.y = -0.5f;
        }
        else
        {
            velocity.y -= grav * Time.deltaTime;
        }

        velocity.y = Mathf.Clamp(velocity.y, -10, 10);
    }

    void DoJump()
    {
        if (grounded)
        {
            if (Input.GetButtonDown("Jump"))
            {
                velocity.y = jumpVel;
                //anim.SetBool("Jump", true);
            }
        }
    }
}
