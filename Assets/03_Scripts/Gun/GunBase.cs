using System.Collections;
using System.Collections.Generic;
using UnityEngine;

enum State
{
    Idle,
    Shooting,
    Reloading,
    Empty
}

public abstract class GunBase : MonoBehaviour
{
    [SerializeField] protected GunSetting _data;
    public float _CurAmmo;
    public float _currentAmmo
    {
        get { return _CurAmmo; }
        set
        {
            _CurAmmo = Mathf.Clamp(value, 0, _data.MaxAmmo);
            if (_CurAmmo <= 0) _state = State.Empty;
        }
    }
    private State _state;

    private void Start()
    {
        //InitSetting();
        _currentAmmo = _data.MaxAmmo;
    }
    //public abstract void InitSetting();

    public virtual void shot()
    {
        if (_state != State.Idle)
            return;

        RaycastHit ray;
        Vector3 hitPos = Vector3.zero;
        Vector3 offset = new Vector3(50, 0, 0);

        if (Physics.Raycast(_data.FriePos.position, _data.FriePos.forward, out ray, _data.MaxDis))
        {

            Gizmos.color = Color.red;
            //Gizmos.DrawRay(transform.position, transform.forward * ray.distance);

            hitPos = ray.point;

            BulletHole.Instance.PlayHitEffect(ray.point, ray.normal, ray.transform);


            IDamage hitTarget = ray.collider.GetComponent<IDamage>();
            if (hitTarget != null)
            {
                hitTarget.IDamage(_data.Damage, ray.point, ray.normal);
            }
        }
        else
        {
            hitPos = _data.FriePos.position + _data.FriePos.forward * _data.MaxDis;
        }

        _CurAmmo--;
        // if (_currentAmmo <= 0)
        //     _state = State.Empty;
    }

    public virtual void Reload()
    {
        if (_currentAmmo <= _data.MaxAmmo)
        {
            StartCoroutine(ReloadRutin());
        }
    }

    IEnumerator ReloadRutin()
    {
        _state = State.Reloading;
        yield return new WaitForSeconds(_data.ReloadTime);
        _currentAmmo = _data.MaxAmmo;
        _state = State.Idle;
    }

    IEnumerator ToIdle()
    {
        _state = State.Shooting;
        yield return new WaitForSeconds(_data.TimebetweenShot);
        _state = State.Idle;
    }
}

