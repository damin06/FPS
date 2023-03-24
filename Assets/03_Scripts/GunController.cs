using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GunController : MonoBehaviour
{
    [Header("Reference")]
    [SerializeField] private Transform FriePos;
    private LineRenderer li;

    private int CurAmmo;
    void Start()
    {
        li = GetComponent<LineRenderer>();
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
            if (ray.collider)
            {
                IDamage hitTarget = ray.collider.GetComponent<IDamage>();
                if (hitTarget != null)
                {
                    hitTarget.IDamage(4, ray.normal);
                }
            }
        }
    }
}
