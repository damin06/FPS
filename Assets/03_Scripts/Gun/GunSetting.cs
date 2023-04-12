using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "SO/Gun/GunsSetting")]
public class GunSetting : ScriptableObject
{
    public Transform FriePos;
    public float GunEffectTime;
    public float ReloadTime;
    public float Damage;
    public int MaxAmmo;
    public float MaxDis;
    public float TimebetweenShot;
}
