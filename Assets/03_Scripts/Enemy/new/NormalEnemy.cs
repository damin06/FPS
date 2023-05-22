using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NormalEnemy : EnemyFSM
{
    protected override void Awake()
    {
        base.Awake();
        _states = new IState[4];

        _states[(int)EnemyState.idle] = new NormalEnemyState.Idle();
        _states[(int)EnemyState.move] = new NormalEnemyState.Move();
        _states[(int)EnemyState.move] = new NormalEnemyState.Move();
        _states[(int)EnemyState.die] = new NormalEnemyState.Die();

        ChangeState((int)EnemyState.idle);
    }
}
