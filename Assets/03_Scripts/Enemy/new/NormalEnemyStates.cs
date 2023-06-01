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

            if (_entity._curTarget != null)
                _entity._navmesh.SetDestination(_entity.GetRandomPointOnNavMesh(_entity._curTarget.transform.position, 4.5f));

        }

        public override void Execute(NormalEnemy _entity)
        {

            if (_entity._curTarget == null || Vector3.Distance(_entity._curTarget.transform.position, _entity.transform.position) > 10)
                _entity.ChangeState(EnemyState.idle);

            if (_entity._navmesh.remainingDistance < 0.4f)
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
            Debug.Log($"{GetType().ToString()} : Attack");
        }

        public override void Execute(NormalEnemy _entity)
        {
            if (Vector3.Distance(_entity.transform.position, _entity._curTarget.transform.position) > 7)
                _entity.ChangeState(EnemyState.chase);

            if (_lastShotTime + _entity._timeToBtweenShot < Time.time)
            {
                _lastShotTime = Time.time;
                Debug.Log("ATTACK");
            }

            Vector3 dir = _entity._curTarget.transform.position - _entity.transform.position;

            _entity.transform.rotation = Quaternion.Lerp(_entity.transform.rotation, Quaternion.LookRotation(dir), Time.deltaTime * _entity._navmesh.acceleration);
        }

        public override void Exit(NormalEnemy _entity)
        {

        }
    }

    public class Die : State<NormalEnemy>
    {
        public override void Enter(NormalEnemy _entity)
        {
            Debug.Log($"{GetType().ToString()} : Die");
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
