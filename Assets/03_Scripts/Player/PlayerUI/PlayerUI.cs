using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerUI : MonoBehaviour
{
    [SerializeField] private Transform _UiPos;
    [SerializeField] private Vector3 _UiPivot;
    private Camera _cam;
    void Start()
    {
        _cam = Camera.main;
    }

    // Update is called once per frame
    void Update()
    {
        transform.position = _UiPos.position + _UiPivot;
        transform.rotation = Quaternion.LookRotation(transform.position - _cam.transform.position);
    }
}
