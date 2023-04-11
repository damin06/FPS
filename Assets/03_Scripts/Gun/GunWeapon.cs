using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public struct Data
{
    public Transform FriePos;
    public float GunEffectTime;
    public float ReloadTime;
    public float Damage;
    public int MaxAmmo;

}
public abstract class GunWeapon : MonoBehaviour
{
    public abstract void InitSetting();

    protected virtual void shot()
    {

    }
}
