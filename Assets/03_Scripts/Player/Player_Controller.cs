using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player_Controller : MonoBehaviour
{
    [SerializeField] private float speed = 5;
    [SerializeField] private float RotateSpeed = 8;
    [SerializeField] float acceleration = 0.1f;
    [SerializeField] float decceleration = 0.5f;
    [SerializeField] float currentspeed => new Vector2(ch.velocity.x, ch.velocity.z).magnitude;


    PlayerInput playerInput;
    CharacterController ch;
    Rigidbody rb;
    Animator ani;
    Transform cam;

    Vector3 look;
    Vector3 camForward;
    Vector3 moveinput;

    float forwardAmount;
    float turnAmount;
    float velocity;


    private void Start()
    {
        playerInput = GetComponent<PlayerInput>();
        rb = GetComponent<Rigidbody>();
        ani = GetComponent<Animator>();
        ch = GetComponent<CharacterController>();
        cam = Camera.main.transform;
    }

    // Update is called once per frame
    private void Update()
    {
        Animtaion_Controll(playerInput.MoveInput.x, playerInput.MoveInput.z);
    }

    private void FixedUpdate()
    {
        Movement();
        Rotation();
    }

    private void Rotation()
    {
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;

        if (Physics.Raycast(ray, out hit, 100))
        {
            look = hit.point;
        }
        Vector3 lookpos = look - transform.position;
        lookpos.y = 0;


        Quaternion toRotation = Quaternion.LookRotation(lookpos);
        transform.rotation = Quaternion.Lerp(transform.rotation, toRotation, RotateSpeed * Time.deltaTime);
    }

    private void Movement()
    {
        ch.SimpleMove(playerInput.MoveInput * speed);
        GetVelocity(playerInput.MoveInput);
    }


    private void Animtaion_Controll(float x, float z)
    {
        Vector3 move;
        if (cam != null)
        {
            camForward = Vector3.Scale(cam.up, new Vector3(1, 0, 1)).normalized;
            move = z * camForward + x * cam.right;
        }
        else
        {
            move = z * Vector3.forward + x * Vector3.right;
        }

        if (move.magnitude > 1)
        {
            move.Normalize();
        }

        this.moveinput = move;

        Vector3 localMove = transform.InverseTransformDirection(moveinput);
        turnAmount = localMove.x;

        forwardAmount = localMove.z;


        ani.SetFloat("InputX", localMove.z, 0.1f, Time.deltaTime);
        ani.SetFloat("InputZ", localMove.x, 0.1f, Time.deltaTime);
    }

    private void GetVelocity(Vector3 moveinput)
    {
        if (moveinput != Vector3.zero && velocity < 1)
        {
            velocity += Time.deltaTime * acceleration;
        }
        else if (moveinput == Vector3.zero && velocity > 0)
        {
            velocity -= Time.deltaTime * decceleration;
        }

        ani.SetFloat("velocity", velocity);
    }

}
