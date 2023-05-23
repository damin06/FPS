using System.Security.Cryptography.X509Certificates;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public enum EnemyState { idle = 0, chase, attack, die }
public class NormalEnemy : PoolableMono
{
    private State<NormalEnemy>[] _states;
    private StateMachine<NormalEnemy> _stateMachine;

    public EnemyState _curState { private set; get; }

    [HideInInspector]
    public NavMeshAgent _navmesh;
    public Transform _curTarget { private set; get; }

    [SerializeField] private LayerMask _whatIsEnemy;

    public override void Reset()
    {
        _states = new State<NormalEnemy>[4];
        _states[(int)EnemyState.idle] = new NormalEnemyStates.Idle();
        _states[(int)EnemyState.chase] = new NormalEnemyStates.Chase();
        _states[(int)EnemyState.attack] = new NormalEnemyStates.Attack();
        _states[(int)EnemyState.die] = new NormalEnemyStates.Die();

        _stateMachine = new StateMachine<NormalEnemy>();
        _stateMachine.Setup(this, _states[(int)EnemyState.idle]);
    }
    private void Awake()
    {
        _navmesh = GetComponent<NavMeshAgent>();
    }

    // Update is called once per frame
    private void Update()
    {
        _stateMachine.Execute();
    }

    public void ChangeState(EnemyState _state)
    {
        _curState = _state;

        _stateMachine.ChangeState(_states[(int)_state]);
    }

    public void Transform(Transform _target)
    {
        Collider[] _cols = Physics.OverlapSphere(transform.position, float.MaxValue, _whatIsEnemy);

        float minDistance = Mathf.Infinity;

        for (int i = 0; i < _cols.Length; i++)
        {
            float distance = Vector3.Distance(transform.position, _cols[i].transform.position);

            if (distance < minDistance)
            {
                minDistance = distance;
                _target = _cols[i].transform;
            }
        }
    }
}
