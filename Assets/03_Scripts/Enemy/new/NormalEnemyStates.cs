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
            if (_entity._curTarget != null && Vector3.Distance(_entity.transform.position, _entity._curTarget.transform.position) < 50)
                _entity.ChangeState(EnemyState.chase);
            else
                _entity.GetTarget();
        }

        public override void Exit(NormalEnemy _entity)
        {

        }
    }

    public class Chase : State<NormalEnemy>
    {
        Vector3 pos;
        public override void Enter(NormalEnemy _entity)
        {
            if (_entity._curTarget != null)
            {
                pos = _entity.GetRandomPointOnNavMesh(_entity._curTarget.transform.position, 5f);
                _entity._navmesh.SetDestination(pos);
            }
        }

        public override void Execute(NormalEnemy _entity)
        {
            if (_entity._curTarget == null)
                _entity.ChangeState(EnemyState.idle);


            if (Vector3.Distance(pos, _entity.transform.position) < 0.5f)
                _entity.ChangeState(EnemyState.attack);
        }

        public override void Exit(NormalEnemy _entity)
        {

        }
    }

    public class Attack : State<NormalEnemy>
    {
        private float _lastShotTime;
        public override void Enter(NormalEnemy _entity)
        {

        }

        public override void Execute(NormalEnemy _entity)
        {
            if (Vector3.Distance(_entity.transform.position, _entity._curTarget.transform.position) > 6)
                _entity.ChangeState(EnemyState.chase);

            if (_lastShotTime + _entity._timeToBtweenShot < Time.time)
            {
                _lastShotTime = Time.time;
                Debug.Log("ATTACK");
            }
        }

        public override void Exit(NormalEnemy _entity)
        {

        }
    }

    public class Die : State<NormalEnemy>
    {
        public override void Enter(NormalEnemy _entity)
        {

        }

        public override void Execute(NormalEnemy _entity)
        {

        }

        public override void Exit(NormalEnemy _entity)
        {

        }
    }
}
