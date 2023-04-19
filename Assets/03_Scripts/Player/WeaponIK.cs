using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponIK : MonoBehaviour
{
    private Animator _ani;

    [Header("Right")]
    [SerializeField] private float RightHandWeight;
    [SerializeField] private Transform RightHandTarget;

    [Space]

    [Header("Left")]
    [SerializeField] private float LeftHandWeight;
    [SerializeField] private Transform LeftHandTarget;

    public Transform weapon;
    public Vector3 lookpos;

    void Awake()
    {
        _ani = GetComponent<Animator>();
    }

    // Update is called once per frame
    void OnAnimatorIK()
    {
        _ani.SetIKPositionWeight(AvatarIKGoal.LeftHand, LeftHandWeight);
        _ani.SetIKPosition(AvatarIKGoal.LeftHand, LeftHandTarget.position);
        _ani.SetIKRotationWeight(AvatarIKGoal.LeftHand, LeftHandWeight);
        _ani.SetIKRotation(AvatarIKGoal.LeftHand, LeftHandTarget.rotation);

        _ani.SetIKPositionWeight(AvatarIKGoal.RightHand, RightHandWeight);
        _ani.SetIKPosition(AvatarIKGoal.RightHand, RightHandTarget.position);
        _ani.SetIKRotationWeight(AvatarIKGoal.RightHand, RightHandWeight);
        _ani.SetIKRotation(AvatarIKGoal.RightHand, RightHandTarget.rotation);
    }
}
