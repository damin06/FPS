using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public abstract class LivingEntity : MonoBehaviour, IDamage
{
    public UnityEvent<float, float> OnHealthChanged = null;
    public UnityEvent OnDie = null;
    [SerializeField] public float _maxHP;
    public bool _isdead { get; protected set; }
    private float _currentHP;
    public float _CurrentHP
    {
        get => _currentHP;
        protected set
        {
            _currentHP = Mathf.Clamp(_currentHP, 0, _maxHP);
            _currentHP = value;
        }
    }

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

        _currentHP -= Damage;
        OnHealthChanged?.Invoke(_currentHP, _maxHP);

        if (_currentHP <= 0)
            OnDeath();
    }


    protected virtual void OnDeath()
    {
        _isdead = true;
        OnDie?.Invoke();
    }
}
