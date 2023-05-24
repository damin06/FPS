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


    [HideInInspector] public NavMeshAgent _navmesh;
    [HideInInspector] public Animator _ani;


    public EnemyState _curState { private set; get; }
    public Transform _curTarget { private set; get; }


    [Header("AI Setting")]
    [Range(0.1f, 50f)][SerializeField] private float _moveSpeed;
    [Range(0, 500)][Tooltip("0은 무한")][SerializeField] private float _range = 0;
    [SerializeField] private LayerMask _whatIsEnemy;

    [Space]

    [Header("Gun Setting")]
    [Range(0.1f, 50f)] public float _damage;
    [Range(0.01f, 10f)] public float _timeToBtweenShot;
    [Range(1f, 100f)] public int _maxAmmo;

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
        Reset();
        _navmesh = GetComponent<NavMeshAgent>();
        _ani = GetComponent<Animator>();

        _navmesh.speed = _moveSpeed;
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

    public void GetTarget()
    {
        Collider[] _cols = Physics.OverlapSphere(transform.position, _range == 0 ? Mathf.Infinity : _range, _whatIsEnemy);
        Transform Target = transform;

        float minDistance = Mathf.Infinity;

        for (int i = 0; i < _cols.Length; i++)
        {
            float distance = Vector3.Distance(transform.position, _cols[i].transform.position);

            if (distance < minDistance)
            {
                minDistance = distance;
                Target = _cols[i].transform;
            }
        }
        _curTarget = Target;
    }

    public Vector3 GetRandomPointOnNavMesh(Vector3 center, float distance)
    {
        Vector3 randomPos = Random.insideUnitSphere * distance + center;

        NavMeshHit hit;

        NavMesh.SamplePosition(randomPos, out hit, distance, NavMesh.AllAreas);

        return hit.position;
    }
}
