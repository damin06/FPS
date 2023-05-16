using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;

    [SerializeField] private PollingListSO _poolingList;



    //private List<Transform> _spawnPointList;

    private void Awake()
    {
        if (Instance != null)
        {
            Debug.LogError("Multiple GameManager is running! Check!");
        }
        Instance = this;


        MakePool();

        //_spawnPointList = new List<Transform>();
        //_SpawnPointParent.GetComponentsInChildren<Transform>(_spawnPointList);
        //_spawnPointList.RemoveAt(0);

        //_spawnWeights = _spawnList._spawnPair.Select(s => s.spawnPercent).ToArray();
    }

    private void MakePool()
    {
        PoolManager.Instance = new PoolManager(transform);

        _poolingList.lis.ForEach(p => PoolManager.Instance.CreatePool(p.prefab, p.poolCount));
    }

    private void Start()
    {
    }

    // private IEnumerator SpawnEnemies()
    // {


    //                 //EnemyBrain enemy = PoolManager.Instance.Pop(_spawnList._spawnPair[sIndex].ToString()) as EnemyBrain;


    //                 // enemy.transform.position = _spawnPointList[idx].position + (Vector3)positionOffseot;
    //                 // enemy.ShowEnemy();



    // }


}
