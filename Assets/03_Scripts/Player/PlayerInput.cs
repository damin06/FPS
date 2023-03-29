using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInput : MonoBehaviour
{
    static public PlayerInput Instance;
    public Vector3 MoveInput { get; private set; }
    public Action OnShot { get; set; }
    public Action OnReload { get; set; }

    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        float x = Input.GetAxisRaw("Horizontal");
        float z = Input.GetAxisRaw("Vertical");

        MoveInput = new Vector3(x, 0, z);


        if (Input.GetKeyDown(KeyCode.R))
        {
            OnReload?.Invoke();
        }

        if (Input.GetButton("Fire1"))
        {
            OnShot?.Invoke();
        }
    }
}
