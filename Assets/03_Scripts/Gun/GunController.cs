// |이 코드는 총을 조작하는 GunController 클래스를 구현한 것입니다.
// |
// |좋은 점:
// |- 코드가 깔끔하고 가독성이 좋습니다.
// |- 변수와 함수의 이름이 명확하게 지어져 있어 코드를 이해하기 쉽습니다.
// |- 총 발사, 재장전, 탄약 소진 등의 상태를 enum으로 정의하여 코드를 간결하게 작성하였습니다.
// |- 총 발사 시 라인 렌더러를 이용하여 총알 궤적을 그리는 효과를 구현하였습니다.
// |
// |나쁜 점:
// |- 총 발사 시 레이캐스트를 이용하여 충돌 검사를 하고 있지만, 레이캐스트의 범위가 고정되어 있어 적이 가까이 있을 때에도 충돌 검사가 제대로 이루어지지 않을 수 있습니다. 따라서 레이캐스트의 범위를 동적으로 조절하거나 다른 충돌 검사 방법을 사용하는 것이 좋을 것입니다.
// |- 총 발사 시 총알 궤적을 그리는 효과를 구현하고 있지만, 총알이 맞은 지점에서 파티클 효과를 재생하는 등 더욱 다양한 효과를 추가할 수 있을 것입니다.
// |
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
    [SerializeField] private ParticleSystem[] gunParticle;
    [SerializeField] private Transform FriePos;
    [SerializeField] private float TimebetweenShot;
    [SerializeField] private float GunEffectTime;
    [SerializeField] private float ReloadTime;
    [SerializeField] private int MaxAmmo;
    [SerializeField] private float Damage;

    private LineRenderer line;
    private bool CandShot = true;

    State _gunState;
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
            _curAmmo = value;
            if (_curAmmo <= 0)
            {
                //StopAllCoroutines();
                //StopCoroutine("WaitShot");
                _gunState = State.Empty;
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
        CandShot = true;

    }

    private IEnumerator WaitShot()
    {
        //_gunState = State.Shot;
        CandShot = false;
        yield return new WaitForSeconds(TimebetweenShot);
        //_gunState = State.idle;
        CandShot = true;
    }

    private void Shoot()
    {
        if (_gunState == State.idle && _lastShot + TimebetweenShot < Time.time)
        {
            _lastShot = Time.time;
            Debug.Log("shoting!");
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
                    BulletHole.Instance.PlayHitEffect(ray.point, ray.normal, ray.transform);


                    IDamage hitTarget = ray.collider.GetComponent<IDamage>();
                    if (hitTarget != null)
                    {
                        hitTarget.IDamage(Damage, ray.point, ray.normal);
                    }
                }
            }
            else
            {
                hitPos = FriePos.position + FriePos.forward * 50;
            }


            StartCoroutine(ShotEffect(hitPos));
            _CurAmmo--;
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
