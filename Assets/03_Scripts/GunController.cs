using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GunController : MonoBehaviour
{
    [Header("Reference")]
    [SerializeField] private Transform FriePos;

    private int CurAmmo;
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    private void Shoot()
    {
        RaycastHit ray;

        if (Physics.Raycast(FriePos.position, FriePos.forward, out ray, 50))
        {

        }
    }
}
