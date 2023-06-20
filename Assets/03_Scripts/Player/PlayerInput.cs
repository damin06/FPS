using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;


public class PlayerInput : MonoBehaviour
{
    static public PlayerInput Instance;
    //public Vector3 MoveInput { get; private set; }
    public Action OnShot { get; set; }
    public Action OnReload { get; set; }

    public UnityEvent<Vector3> OnMove;

    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {



        if (Input.GetKeyDown(KeyCode.R))
        {
            OnReload?.Invoke();
        }

        if (Input.GetButton("Fire1"))
        {
            OnShot?.Invoke();
        }
    }

    private void FixedUpdate()
    {
        MoveInput();
    }

    private void MoveInput()
    {
        float x = Input.GetAxisRaw("Horizontal");
        float z = Input.GetAxisRaw("Vertical");

        OnMove?.Invoke(new Vector3(z, 0, -x));
    }
}
