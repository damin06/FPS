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
    [SerializeField] private float GunEffectTime;
    [SerializeField] private float ReloadTime;
    private LineRenderer line;
<<<<<<< HEAD
<<<<<<< HEAD
<<<<<<< HEAD
=======
=======

>>>>>>> parent of ef2fe2b (paricle)

>>>>>>> parent of ef2fe2b (paricle)
=======
    private bool CandShot = true;
>>>>>>> parent of 449987b (Revert "paricle")

    private int CurAmmo;
    void Start()
    {
<<<<<<< HEAD
        line = GetComponent<LineRenderer>();
=======
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
<<<<<<< HEAD
<<<<<<< HEAD
>>>>>>> parent of ef2fe2b (paricle)
=======

>>>>>>> parent of 449987b (Revert "paricle")
=======
>>>>>>> parent of ef2fe2b (paricle)
    }

    // Update is called once per frame
    void Update()
    {

    }

    private IEnumerator Reload()
    {

        yield return new WaitForSeconds(ReloadTime);
<<<<<<< HEAD
<<<<<<< HEAD
<<<<<<< HEAD
=======
        _CurAmmo = MaxAmmo;
=======
        _curAmmo = MaxAmmo;
>>>>>>> parent of 449987b (Revert "paricle")
=======
        _CurAmmo = MaxAmmo;
>>>>>>> parent of ef2fe2b (paricle)
        _gunState = State.idle;

    }

    private IEnumerator WaitShot()
    {
        _gunState = State.Shot;
        yield return new WaitForSeconds(TimebetweenShot);
<<<<<<< HEAD
<<<<<<< HEAD
        _gunState = State.idle;
>>>>>>> parent of ef2fe2b (paricle)
=======
        //_gunState = State.idle;
        CandShot = true;
>>>>>>> parent of 449987b (Revert "paricle")
=======
        _gunState = State.idle;
>>>>>>> parent of ef2fe2b (paricle)
    }

    private void Shoot()
    {
<<<<<<< HEAD
<<<<<<< HEAD
<<<<<<< HEAD
        RaycastHit ray;

        if (Physics.Raycast(FriePos.position, FriePos.forward, out ray, 50))
        {
            if (ray.collider)
            {
                Gizmos.color = Color.red;
                Gizmos.DrawRay(transform.position, transform.forward * ray.distance);
                StartCoroutine(ShotEffect(ray.point));

                IDamage hitTarget = ray.collider.GetComponent<IDamage>();
                if (hitTarget != null)
                {
                    hitTarget.IDamage(4, ray.normal);
=======
        if (_gunState == State.idle)
=======
        if (_gunState == State.idle && CandShot)
>>>>>>> parent of 449987b (Revert "paricle")
=======
        if (_gunState == State.idle)
>>>>>>> parent of ef2fe2b (paricle)
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
>>>>>>> parent of ef2fe2b (paricle)
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
