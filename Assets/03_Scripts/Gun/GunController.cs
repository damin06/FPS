using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public struct GunSet
{

}
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
    [SerializeField] private Transform FriePos;
    [SerializeField] private float TimebetweenShot;
    [SerializeField] private float GunEffectTime;
    [SerializeField] private float ReloadTime;
    [SerializeField] private int MaxAmmo;
    [SerializeField] private float Damage;

    private LineRenderer line;


    private float lastShot;
    State _gunState;
    private int _curAmmo;
    public int _CurAmmo
    {
        get
        {
            return _curAmmo;
        }
        set
        {
            if (_curAmmo <= 0)
            {
                _gunState = State.Empty;
            }
            else
            {
                _curAmmo = value;
            }
        }
    }

    void Awake()
    {
        GameObject.FindObjectOfType<PlayerInput>().OnShot += Shoot;
        GameObject.FindObjectOfType<PlayerInput>().OnReload += Reload;


        line = GetComponent<LineRenderer>();

        _CurAmmo = MaxAmmo;
        _gunState = State.idle;
    }

    // Update is called once per frame
    void Update()
    {

    }

    private void Reload()
    {
        Debug.Log("Reloading");
        StartCoroutine(Reloading());
    }
    private IEnumerator Reloading()
    {
        _gunState = State.Reloading;
        yield return new WaitForSeconds(ReloadTime);
        _CurAmmo = MaxAmmo;
        _gunState = State.idle;

    }

    private IEnumerator WaitShot()
    {
        _gunState = State.Shot;
        yield return new WaitForSeconds(TimebetweenShot);
        _gunState = State.idle;
    }

    private void Shoot()
    {
        if (_gunState == State.idle)
        {
            Debug.Log("shoting!");
            StartCoroutine(WaitShot());
            _CurAmmo--;

            RaycastHit ray;

            if (Physics.Raycast(FriePos.position, FriePos.forward, out ray, 50))
            {
                if (ray.collider)
                {
                    Gizmos.color = Color.red;
                    //                    Gizmos.DrawRay(transform.position, transform.forward * ray.distance);
                    StartCoroutine(ShotEffect(ray.point));


                    IDamage hitTarget = ray.collider.GetComponent<IDamage>();
                    if (hitTarget != null)
                    {
                        hitTarget.IDamage(Damage, ray.point, ray.normal);
                    }
                }
            }
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
