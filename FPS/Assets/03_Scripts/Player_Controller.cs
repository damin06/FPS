using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player_Controller : MonoBehaviour
{
    [SerializeField] private float speed = 5;
    [SerializeField] float acceleration = 0.1f;
    [SerializeField] float decceleration = 0.5f;

    Rigidbody rb;
    Animator ani;
    Transform cam;

    Vector3 look;
    Vector3 camForward;
    Vector3 move;
    Vector3 moveinput;

    float forwardAmount;
    float turnAmount;
    float velocity;


    void Start()
    {
        rb = GetComponent<Rigidbody>();
        ani = GetComponent<Animator>();

        cam = Camera.main.transform;
    }

    // Update is called once per frame
    void Update()
    {
        Movement();
    }

    private void Movement()
    {
        float x = Input.GetAxisRaw("Horizontal");
        float z = Input.GetAxisRaw("Vertical");

        Vector3 moveInput = new Vector3(x, 0, z);

        GetVelocity(moveInput);
        //rb.velocity = moveInput * speed;
        rb.AddForce(moveInput * speed / Time.deltaTime);


        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;


        if (Physics.Raycast(ray, out hit, 100))
        {
            look = hit.point;
        }
        Vector3 lookpos = look - transform.position;
        lookpos.y = 0;

        transform.LookAt(transform.position + lookpos, Vector3.up);

        Animtaion_Controll(x, z);
    }

    private void Animtaion_Controll(float x, float z)
    {
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


        ani.SetFloat("InputX", forwardAmount, 0.1f, Time.deltaTime);
        ani.SetFloat("InputZ", turnAmount, 0.1f, Time.deltaTime);
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
