using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBullet : PoolableMono
{
    [SerializeField] private float _damage;
    public Rigidbody _rb;
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
        if (other.gameObject.layer == LayerMask.NameToLayer("Enemy"))
        {
            IDamage _hit = other.gameObject.GetComponent<IDamage>();

            if (_hit != null)
            {
                var collisionPoint = other.ClosestPoint(transform.position);
                var collisionNormal = transform.position - collisionPoint;

                _hit.IDamage(_damage, collisionPoint, collisionNormal);
            }
        }
    }

    public override void Reset()
    {

    }
}
