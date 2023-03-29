using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowGun : MonoBehaviour
{
    [SerializeField] private Transform GunPos;
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        transform.position = GunPos.position;
    }
}
