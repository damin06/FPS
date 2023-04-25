using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class LivingEntity : MonoBehaviour, IDamage
{
    [SerializeField] protected float _maxHP;
    public float _currentHP { get; protected set; }
    public bool dead { get; protected set; }

    public void IDamage(float Damage, Vector3 hitPos, Vector3 normal)
    {

    }

    protected abstract void OnDie();
}
