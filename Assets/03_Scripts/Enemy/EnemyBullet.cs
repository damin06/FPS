using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBullet : PoolableMono
{
    [SerializeField] private float _damage;
    private Rigidbody _rb;
    private void Awake()
    {
        _rb = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {

    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.layer == LayerMask.NameToLayer("Player"))
        {
            IDamage _hit = other.gameObject.GetComponent<IDamage>();

            if (_hit != null)
            {
                var collisionPoint = other.ClosestPoint(transform.position);
                var collisionNormal = transform.position - collisionPoint;

                _hit.IDamage(_damage, collisionPoint, collisionNormal);
            }
        }

        Pool();
    }

    public override void Reset()
    {
        _rb.velocity = Vector3.zero;
        //_rb.AddForce(Vector3.forward, ForceMode.Impulse);
        Invoke("Pool", 4f);
    }

    private void Pool()
    {
        PoolManager.Instance.Push(this);
    }

    public void ShootBullet(Transform _shotPoint)
    {
        _rb.AddForce(_shotPoint.forward, ForceMode.Impulse);
    }
}
