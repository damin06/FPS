using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public class FeedBackCamerShake : FeedBack
{
    private CinemachineImpulseSource _impulseSource;

    private void Awake()
    {

        _impulseSource = GetComponent<CinemachineImpulseSource>();
    }


    public override void CompleteFeedBack()
    {

    }

    public override void CreateFeedBack()
    {
        _impulseSource.GenerateImpulse();
        // CinemachineBasicMultiChannelPerlin _cinemachineBasicMultiChannelPerlin = _cinemach.GetCinemachineComponent<CinemachineBasicMultiChannelPerlin>();

        // _cinemachineBasicMultiChannelPerlin.m_AmplitudeGain = _intensity;

        // _startInensity = _intensity;
        // __shakeTimeTotal = _time;
        // _shakeTime = _time;
    }
}
