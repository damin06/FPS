using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class plane : MonoBehaviour
{
    [SerializeField] private Transform _mid;
    [SerializeField] private Transform a;
    private void Awake()
    {

    }

    // Update is called once per frame
    private void Update()
    {
        a.transform.Rotate(Vector3.forward * Time.deltaTime * 10);
        transform.RotateAround(_mid.position, Vector3.down, 20 * Time.deltaTime);
    }
}
