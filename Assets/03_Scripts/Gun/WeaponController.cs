using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponController : MonoBehaviour
{
    [SerializeField] private Transform _weaponList;
    private List<GunBase> _weapons;
    private GunBase _gunBase;
    private GunBase _lastBase;

    void Start()
    {
        //_weapons = GetComponentsInChildren<GunBase>();

        foreach (GunBase guns in _weaponList)
        {
            _weapons.Add(guns);
        }
        //WeaponChange(0);

    }

    // Update is called once per frame
    void Update()
    {

    }

    private void WeaponChange(int index)
    {
        for (int i = 0; i < _weapons.Count; i++)
        {
            _weapons[i].gameObject.SetActive(false);
        }
        _weapons[index].gameObject.SetActive(true);
        if (_gunBase != _weapons[index])
        {

        }
        PlayerInput.Instance.OnShot -= _gunBase.shot;
        PlayerInput.Instance.OnShot += _weapons[index].shot;
    }
}
