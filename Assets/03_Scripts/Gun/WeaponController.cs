using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponController : MonoBehaviour
{
    private GunBase[] _weapons;
    public GunBase _gunBase;
    private GunBase _lastBase;

    void Start()
    {
        _weapons = GetComponentsInChildren<GunBase>();
        WeaponChange(0);

    }

    // Update is called once per frame
    void Update()
    {

    }

    private void WeaponChange(int index)
    {
        for (int i = 0; i < _weapons.Length; i++)
        {
            _weapons[i].gameObject.SetActive(false);
        }
        _weapons[index].gameObject.SetActive(true);

        PlayerInput.Instance.OnShot += _weapons[index].shot;
    }
}
