using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class PlayerUI : MonoBehaviour
{
    public static PlayerUI Instance { get; private set; }

    private Transform _UiPos;
    [SerializeField] private Vector3 _UiPivot;
    private TextMeshProUGUI _ammorTXT;
    private Image _hpBar;

    private PlayerHealth _playerHealth;
    private Camera _cam;
    void Start()
    {
        _UiPos = GameObject.Find("Player").transform;
        _ammorTXT = GameObject.Find("AmmorTXT").GetComponent<TextMeshProUGUI>();
        _hpBar = GameObject.Find("HPBar").GetComponent<Image>();

        _cam = Camera.main;
    }

    // Update is called once per frame
    void Update()
    {
        transform.position = _UiPos.position + _UiPivot;
        transform.rotation = Quaternion.LookRotation(transform.position - _cam.transform.position);
    }

    public void SetAmmo(int current, int max)
    {
        _ammorTXT.text = $"{current} / {max}";
    }

    public void SetHpBar(float current, float max)
    {
        _hpBar.fillAmount = current / max;
    }
}
