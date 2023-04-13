using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponController : MonoBehaviour
{
    public GunBase[] _weapons;
    public GunBase _gunBase;
    private GunBase _lastBase;

    void Start()
    {
        _gunBase = _weapons[0];
        _lastBase = _gunBase;

    }

    // Update is called once per frame
    void Update()
    {
        PlayerInput.Instance.OnShot += _gunBase.shot;
    }

    int temp;
    private void WeaponChange()
    {
        if (Input.GetKeyDown(KeyCode.Q))
        {
            temp--;
        }
        Mathf.Clamp(temp, 0, _weapons.Length);
    }
}
