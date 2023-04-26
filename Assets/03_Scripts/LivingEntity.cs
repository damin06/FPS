using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public abstract class LivingEntity : MonoBehaviour, IDamage
{
    [SerializeField] private float _maxHP;
    public float MaxHP
    {
        get
        {
            return _maxHP;
        }
        protected set
        {
            _maxHP = value;
        }
    }
    public float _currentHP { get; protected set; }
    public bool _isdead { get; protected set; }

    protected virtual void OnEnable()
    {
        // 사망하지 않은 상태로 시작
        _isdead = false;
        // 체력을 시작 체력으로 초기화
        _currentHP = _maxHP;
    }

    public void IDamage(float Damage, Vector3 hitPos, Vector3 normal)
    {
        if (_isdead)
            return;

        ChangeHP(Damage, false);

        if (_currentHP <= 0)
            OnDie();
    }

    protected virtual void ChangeHP(float Damage, bool isFlus)
    {
        if (isFlus)
            _currentHP += Damage;
        else
            _currentHP -= Damage;
    }

    protected virtual void OnDie()
    {
        _isdead = true;
    }
}
