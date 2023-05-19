using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class IState
{
    public abstract void OnStateEnter(EnemyFSM _enemy);
    public abstract void OnStateUpdate(EnemyFSM _enemy);
    public abstract void OnStateExit(EnemyFSM _enemy);
}

