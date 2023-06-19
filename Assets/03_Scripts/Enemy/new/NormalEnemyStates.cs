using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace NormalEnemyStates
{
    public class Idle : State<NormalEnemy>
    {
        public override void Enter(NormalEnemy _entity)
        {
            Debug.Log($"{GetType().ToString()} : Idle");
            _entity.GetTarget();
        }

        public override void Execute(NormalEnemy _entity)
        {
            if (_entity._curTarget != null)
                _entity.ChangeState(EnemyState.chase);

            _entity.GetTarget();
        }

        public override void Exit(NormalEnemy _entity)
        {

        }
    }

    public class Chase : State<NormalEnemy>
    {
        public override void Enter(NormalEnemy _entity)
        {
            Debug.Log($"{GetType().ToString()} : Chase");

            //_entity._navmesh.ResetPath();

            if (_entity._curTarget != null)
                _entity._navmesh.SetDestination(_entity.GetRandomPointOnNavMesh(_entity._curTarget.transform.position, 5));
        }

        public override void Execute(NormalEnemy _entity)
        {

            // if (_entity._curTarget == null || Vector3.Distance(_entity._curTarget.transform.position, _entity.transform.position) > 14)
            //     _entity.ChangeState(EnemyState.idle);

            // if (Vector3.Distance(_entity.transform.position, _entity._curTarget.transform.position) > 20)
            //     _entity.ChangeState(EnemyState.idle);


            if (_entity._navmesh.remainingDistance < 0.2f)
                _entity.ChangeState(EnemyState.attack);
        }

        public override void Exit(NormalEnemy _entity)
        {
            _entity._navmesh.ResetPath();
        }
    }

    public class Attack : State<NormalEnemy>
    {
        private float _lastShotTime;
        public override void Enter(NormalEnemy _entity)
        {
            _entity._navmesh.ResetPath();
            Debug.Log($"{GetType().ToString()} : Attack");
        }

        public override void Execute(NormalEnemy _entity)
        {
            if (Vector3.Distance(_entity.transform.position, _entity._curTarget.transform.position) > 10)
            {
                _entity.ChangeState(EnemyState.chase);

            }

            if (_lastShotTime + _entity._timeToBtweenShot < Time.time)
            {
                _lastShotTime = Time.time;
                Debug.Log("ATTACK");

                // RaycastHit hit;
                // if (Physics.Raycast(_entity._shotPoint.transform.position, _entity._shotPoint.forward, out hit, int.MaxValue))
                // {
                //     var _blood = PoolManager.Instance.Pop("EnemyBullet") as EnemyBullet;

                //     _blood.transform.position = _entity._shotPoint.transform.position;
                //     //_blood.transform.SetPositionAndRotation(_entity._shotPoint.transform.position, _entity._shotPoint.rotation);

                //     _blood.GetComponent<Rigidbody>().AddForce(_entity._shotPoint.forward * 5, ForceMode.Impulse);

                // }

                //_blood.GetComponent<EnemyBullet>().ShootBullet(_entity.transform);

                //Ray ray = _entity._shotPoint.position;
                RaycastHit hit;
                int rand;

                Vector3 targetPoint = Vector3.zero;
                if (Physics.Raycast(_entity._shotPoint.transform.position, _entity._shotPoint.transform.rotation * Vector3.forward, out hit, 15))
                {
                    rand = Random.Range(0, 5);
                    if (rand > 1)
                    {
                        float x = Random.Range(-0.8f, 0.8f);

                        Vector3 directionWithoutSpread = hit.point;
                        Vector3 directionWithSpread = directionWithoutSpread + new Vector3(x, _entity._shotPoint.position.y, 0);
                        CoroutineHelper.StartCoroutine(shot(_entity, directionWithSpread));
                        return;
                    }

                    CoroutineHelper.StartCoroutine(shot(_entity, hit.point));
                    //Debug.Log(hit.transform.name);
                    targetPoint = hit.point;
                    // Vector3 direction = _entity._shotPoint.position - targetPoint;
                    // var _blood = PoolManager.Instance.Pop("EnemyBullet") as EnemyBullet;
                    // //_blood.transform.SetPositionAndRotation(_entity._shotPoint.position, _entity._shotPoint.rotation);
                    // _blood.transform.position = _entity._shotPoint.position;

                    // //_blood.transform.forward = direction.normalized;

                    // _blood.GetComponent<Rigidbody>().AddForce(_entity._curTarget.position.normalized, ForceMode.Impulse);
                    if (hit.transform.name == "Player")
                    {
                        IDamage _hit = hit.transform.GetComponent<IDamage>();

                        if (_hit != null)
                            _hit.IDamage(_entity._damage, hit.point, hit.normal);
                    }

                }
                // else
                //     targetPoint = ray.GetPoint(75);
            }
            Vector3 dir = _entity._curTarget.transform.position - _entity.transform.position;
            _entity.transform.rotation = Quaternion.Lerp(_entity.transform.rotation, Quaternion.LookRotation(dir), Time.deltaTime * _entity._navmesh.acceleration);
        }

        public override void Exit(NormalEnemy _entity)
        {

        }

        IEnumerator shot(NormalEnemy _entity, Vector3 hitpos)
        {
            _entity._line.enabled = true;
            _entity._line.SetPosition(0, _entity._shotPoint.position);
            _entity._line.SetPosition(1, hitpos);
            yield return new WaitForSeconds(0.1f);
            _entity._line.enabled = false;
        }
    }

    public class Die : State<NormalEnemy>
    {
        public override void Enter(NormalEnemy _entity)
        {
            //Debug.Log($"{GetType().ToString()} : Die");
        }

        public override void Execute(NormalEnemy _entity)
        {

        }

        public override void Exit(NormalEnemy _entity)
        {

        }
    }

    public class globalState : State<NormalEnemy>
    {
        Vector3 _worldDeltaPosition;
        Vector3 _groundDeltaPosition;
        Vector2 _velocity = Vector2.zero;
        public override void Enter(NormalEnemy _entity)
        {

        }

        public override void Execute(NormalEnemy _entity)
        {
            _worldDeltaPosition = _entity._navmesh.nextPosition - _entity.transform.position;
            _groundDeltaPosition.x = Vector3.Dot(_entity.transform.right, _worldDeltaPosition);
            _groundDeltaPosition.y = Vector3.Dot(_entity.transform.forward, _worldDeltaPosition);

            _velocity = (Time.deltaTime > 1e-5f) ? (Vector2)_groundDeltaPosition / Time.deltaTime : _velocity = Vector2.zero;
            bool _shouldMove = _velocity.magnitude > 0.025f && _entity._navmesh.remainingDistance > _entity._navmesh.radius;

            _entity._ani.SetBool("IsMove", _shouldMove);
            _entity._ani.SetFloat("InputX", _velocity.x);
            _entity._ani.SetFloat("InputY", _velocity.y);
        }

        public override void Exit(NormalEnemy _entity)
        {

        }
    }
}
