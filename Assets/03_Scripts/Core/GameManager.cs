using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using Random = UnityEngine.Random;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;
    [SerializeField] private PollingListSO _poolingList;
    [SerializeField] private GameObject _player;


    private void Awake()
    {
        if (Instance != null)
        {
            Debug.LogError("Multiple GameManager is running! Check!");
        }
        Instance = this;

        MakePool();

        StartCoroutine(SpawnEnemy());
    }

    private void Update()
    {

    }

    private IEnumerator SpawnEnemy()
    {
        float _currentTime = 0;
        while (true)
        {
            _currentTime += Time.deltaTime;

            if (_currentTime < 4)
                continue;

            NormalEnemy enemy = PoolManager.Instance.Pop("NormalEnemy") as NormalEnemy;
            //enemy.transform.position = GetRandomPointOnNavMesh(GetRandomPlayerPos(), 5);
            enemy.transform.position = _player.transform.position;

            yield return new WaitForSeconds(2.5f);
        }
    }

    private Vector3 GetRandomPlayerPos()
    {
        float x = Random.Range(16, 20) * GetRandomSign();
        float z = Random.Range(16, 20) * GetRandomSign();

        return _player.transform.position + new Vector3(x, 0, z);
    }


    public int GetRandomSign()
    {
        int a = Random.Range(0, 2);
        return a == 0 ? -1 : 1;
    }

    private Vector3 GetRandomPointOnNavMesh(Vector3 center, float distance)
    {
        Vector3 randomPos = Random.insideUnitSphere * distance + center;

        NavMeshHit hit;

        NavMesh.SamplePosition(randomPos, out hit, distance, NavMesh.AllAreas);

        return hit.position;
    }

    private void MakePool()
    {
        PoolManager.Instance = new PoolManager(transform);

        _poolingList.lis.ForEach(p => PoolManager.Instance.CreatePool(p.prefab, p.poolCount));
    }
}
