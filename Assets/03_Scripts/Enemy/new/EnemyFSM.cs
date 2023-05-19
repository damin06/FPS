using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class EnemyFSM : MonoBehaviour
{
    public enum EnemyState
    {
        idle,
        move,
        attack,
        die
    };

    public EnemyState _state;
    public IState[] _states;
    private IState currentState;

    protected virtual void Awake()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (currentState != null)
        {
            currentState.OnStateUpdate(this);
        }
    }

    public void ChangeState(EnemyState newState)
    {
        if (_states[(int)newState] == null) return;

        if (currentState != null)
        {
            currentState.OnStateExit(this);
        }

        currentState = _states[(int)newState];
        currentState.OnStateEnter(this);
    }
}
