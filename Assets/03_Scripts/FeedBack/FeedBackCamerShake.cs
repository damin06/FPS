using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public class FeedBackCamerShake : FeedBack
{
    [SerializeField] private float _intensity;
    [SerializeField] private float _time;
    private float _shakeTime;
    private float __shakeTimeTotal;
    private float _startInensity;

    private CinemachineVirtualCamera _cinemach;

    private void Awake()
    {
        _cinemach = GameObject.Find("CM vcam1").GetComponent<CinemachineVirtualCamera>();
    }

    private void Update()
    {
        if (_shakeTime > 0)
        {
            _shakeTime -= Time.deltaTime;

            CinemachineBasicMultiChannelPerlin _cinemachineBasicMultiChannelPerlin = _cinemach.GetCinemachineComponent<CinemachineBasicMultiChannelPerlin>();
            _cinemachineBasicMultiChannelPerlin.m_AmplitudeGain = Mathf.Lerp(_startInensity, 0f, _shakeTime / __shakeTimeTotal);

        }
    }

    public override void CompleteFeedBack()
    {

    }

    public override void CreateFeedBack()
    {
        CinemachineBasicMultiChannelPerlin _cinemachineBasicMultiChannelPerlin = _cinemach.GetCinemachineComponent<CinemachineBasicMultiChannelPerlin>();

        _cinemachineBasicMultiChannelPerlin.m_AmplitudeGain = _intensity;

        _startInensity = _intensity;
        __shakeTimeTotal = _time;
        _shakeTime = _time;
    }
}
