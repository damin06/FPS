using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using Random = UnityEngine.Random;
using UnityEngine.SceneManagement;
using TMPro;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;
    [SerializeField] private PollingListSO _poolingList;
    [SerializeField] private GameObject _player;
    [SerializeField] private GameObject _img;
    [SerializeField] private TextMeshProUGUI _tmpro;
    [SerializeField] private TextMeshProUGUI _endtmpro;
    private float currentTime;

    private void Awake()
    {
        if (Instance != null)
        {
            Debug.LogError("Multiple GameManager is running! Check!");
        }
        Instance = this;

        MakePool();

    }

    private void Start()
    {

    }

    private void Update()
    {
        currentTime += Time.deltaTime;
        _tmpro.text = currentTime.ToString("F2");
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
            //enemy.transform.position = _player.transform.position;
            //enemy.transform.position = Vector3.zero;

            yield return new WaitForSeconds(2.5f);
        }
    }

    private Vector3 GetRandomPlayerPos()
    {
        float x = Random.Range(12, 15) * GetRandomSign();
        float z = Random.Range(12, 15) * GetRandomSign();

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

    public void RestartSceen()
    {
        SceneManager.LoadScene(0);
    }

    public void OnEndUI()
    {
        Invoke("aaa", 1);
    }

    public void aaa()
    {
        _img.SetActive(true);
        _endtmpro.text = currentTime.ToString("F2");
    }

    public void ONSTArTSpawnEnemy()
    {
        StartCoroutine(SpawnEnemy());
    }
}
