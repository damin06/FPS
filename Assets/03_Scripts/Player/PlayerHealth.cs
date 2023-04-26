using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class PlayerHealth : MonoBehaviour, IDamage
{
    public UnityEvent<float, float> OnHealthChanged = null;
    public UnityEvent OnDie = null;

    [SerializeField] public float _maxHP;
    private float _currentHP;
    public float CurrentHP
    {
        get => _currentHP;
        set
        {
            _currentHP = value;
            _currentHP = Mathf.Clamp(_currentHP, 0, _maxHP);
        }
    }
    public bool _isdead;

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
            Death();
    }

    public virtual void PlusHP(float Plus)
    {
        _currentHP += Plus;
    }

    private void Death()
    {
        _isdead = true;
        OnDie?.Invoke();
    }
}
