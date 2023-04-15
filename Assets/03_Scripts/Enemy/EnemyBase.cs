using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBase : MonoBehaviour
{

    [Header("State")]
    [Range(0, 50)] public float _followRange = 10f;
    [Range(0, 30)] public float _attackRange = 5f;
    [Range(1, 30)] public float _speed = 4f;
    [Range(0, 5)] public float _shotTime = 1f;
    [Space]


    [Header("Reference")]
    [SerializeField] private GameObject _gunLight;
    [SerializeField] private Transform _firePos;



    public GameObject _player;
    private LineRenderer _line;
    private float _lastShot = -1000f;

    void Awake()
    {
        //_player = GameObject.Find("Player").GetComponent<Player_Controller>();
        _line = GetComponent<LineRenderer>();
    }

    public float Distance() => Vector3.Distance(transform.position, _player.transform.position);

    public bool CandShot() => (Time.time > _lastShot + _shotTime);

    public IEnumerator OnShot()
    {
        if (!CandShot())
            yield return null;


        _lastShot = Time.time;

        _gunLight.gameObject.SetActive(true);

        _line.enabled = true;
        _line.SetPosition(0, _firePos.position);
        _line.SetPosition(1, _player.transform.position);

        yield return new WaitForSeconds(0.1f);
        _gunLight.gameObject.SetActive(false);
        _line.enabled = false;
    }

    public void FollowPlayer()
    {
        transform.position = Vector3.MoveTowards(transform.position, _player.transform.position, Time.deltaTime * _speed);
    }

    public void Rotate()
    {
        Quaternion toRotation = Quaternion.LookRotation(_player.transform.position, Vector3.up);
        transform.rotation = Quaternion.Lerp(transform.rotation, toRotation, 4 * Time.deltaTime);
    }
}
