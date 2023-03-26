using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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

    private int CurAmmo;
    void Start()
    {
        line = GetComponent<LineRenderer>();
    }

    // Update is called once per frame
    void Update()
    {

    }

    private IEnumerator Reload()
    {

        yield return new WaitForSeconds(ReloadTime);
    }

    private void Shoot()
    {
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
