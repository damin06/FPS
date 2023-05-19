using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class GunController : MonoBehaviour
{
    enum State
    {
        idle,
        Reloading,
        Shot,
        Empty
    };

    [Header("Reference")]
    [SerializeField] private ParticleSystem[] gunParticle;
    [SerializeField] private Transform FriePos;
    [SerializeField] private float TimebetweenShot;
    [SerializeField] private float GunEffectTime;
    [SerializeField] private float ReloadTime;
    [SerializeField] public int _maxAmmo;
    [SerializeField] private float Damage;

    public UnityEvent OnShot;
    public UnityEvent OnReload;
    public UnityEvent<int, int> OnChangeAmmo;

    State _gunState;
    private LineRenderer line;
    private float _lastShot;
    private int _curAmmo;
    public int _CurAmmo
    {
        get
        {
            return _curAmmo;
        }
        set
        {
            _curAmmo = Mathf.Clamp(value, 0, _maxAmmo);
            if (_curAmmo <= 0)
            {
                //StopAllCoroutines();
                //StopCoroutine("WaitShot");
                _gunState = State.Empty;
            }
        }
    }

    private void Awake()
    {

        GameObject.FindObjectOfType<PlayerInput>().OnShot += Shoot;
        GameObject.FindObjectOfType<PlayerInput>().OnReload += Reload;


        line = GetComponent<LineRenderer>();

        _CurAmmo = _maxAmmo;
        _gunState = State.idle;

        //PlayerUI.Instance.SetAmmo(_CurAmmo, _maxAmmo);
    }

    // Update is called once per frame
    void Update()
    {

    }

    private void Reload()
    {
        Debug.Log("Reloading");
        OnReload?.Invoke();
        StartCoroutine(Reloading());
    }

    private IEnumerator Reloading()
    {
        _gunState = State.Reloading;
        yield return new WaitForSeconds(ReloadTime);
        _CurAmmo = _maxAmmo;
        _gunState = State.idle;
        OnChangeAmmo?.Invoke(_CurAmmo, _maxAmmo);
    }


    private void Shoot()
    {
        if (_gunState == State.idle && _lastShot + TimebetweenShot < Time.time)
        {
            OnShot?.Invoke();

            _CurAmmo--;
            OnChangeAmmo?.Invoke(_CurAmmo, _maxAmmo);

            _lastShot = Time.time;
            //Debug.Log("shoting!");
            //StartCoroutine("WaitShot");

            RaycastHit ray;
            Vector3 hitPos = Vector3.zero;
            Vector3 offset = new Vector3(50, 0, 0);
            foreach (ParticleSystem par in gunParticle)
            {
                par.Play();
            }

            if (Physics.Raycast(FriePos.position, FriePos.forward * offset.x, out ray, 50))
            {
                if (ray.collider)
                {
                    Gizmos.color = Color.red;
                    //Gizmos.DrawRay(transform.position, transform.forward * ray.distance);

                    hitPos = ray.point;
                    //BulletHole.Instance.PlayHitEffect(ray.point, ray.normal, ray.transform);


                    IDamage hitTarget = ray.collider.GetComponent<IDamage>();
                    if (hitTarget != null)
                    {
                        hitTarget.IDamage(Damage, ray.point, ray.normal);
                    }
                }

                if (ray.collider.gameObject.layer == LayerMask.NameToLayer("Enemy"))
                {
                    BloodParticle _blood = PoolManager.Instance.Pop("BloodParticle") as BloodParticle;
                    //_blood.transform.SetParent(null);
                    _blood.transform.SetPositionAndRotation(ray.point, Quaternion.LookRotation(ray.normal));

                    if (ray.transform != null) _blood.transform.SetParent(ray.transform);
                }
                else
                {
                    BulletHole2Particle _bulletPar = PoolManager.Instance.Pop("BulletHole2Particle") as BulletHole2Particle;
                    //_bulletPar.transform.SetParent(null);
                    _bulletPar.transform.SetPositionAndRotation(ray.point, Quaternion.LookRotation(ray.normal));

                    if (ray.transform != null) _bulletPar.transform.SetParent(ray.transform);
                }

            }
            else
            {
                hitPos = FriePos.position + FriePos.forward * 50;
            }


            StartCoroutine(ShotEffect(hitPos));
        }
    }

    private IEnumerator ShotEffect(Vector3 hitPos)
    {
        line.enabled = true;
        line.SetPosition(0, FriePos.position);
        line.SetPosition(1, hitPos);

        yield return new WaitForSeconds(GunEffectTime);

        line.enabled = false;
    }
}
