using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Plane : MonoBehaviour
{
    [SerializeField] private GameObject GameManger;
    private void Awake()
    {

    }

    // Update is called once per frame
    void Update()
    {
        transform.RotateAround(GameManger.transform.position, Vector3.up, 1);
    }
}
