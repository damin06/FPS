using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

[Serializable]
struct Sounds
{
    public string name;
    public AudioClip clip;
}

public class AudioPlayer : MonoBehaviour
{
    [SerializeField][Range(0f, 5f)] private float _pitchRandomness = 0.2f;
    [SerializeField] private Sounds[] _clips;
    private AudioSource _audioSource;

    private float _basePitch;

    private void Awake()
    {
        _audioSource = GetComponent<AudioSource>();
        _basePitch = _audioSource.pitch;
    }

    public void StopAudio()
    {
        _audioSource.Stop();
    }

    public void SimplePlay(string _name)
    {
        if (_clips.Length < 1)
            return;

        AudioClip tartgetClip = null;

        for (int i = 0; i < _clips.Length; i++)
        {
            if (_clips[i].name == _name)
            {
                tartgetClip = _clips[i].clip;
                break;
            }
        }

        _audioSource.Stop();
        _audioSource.clip = tartgetClip;
        _audioSource.Play();
    }

    public void PlayerClipWithVariablePitch(string _name)
    {
        float randomPitch = Random.Range(-_pitchRandomness, _pitchRandomness);
        _audioSource.pitch = _basePitch + randomPitch;

        SimplePlay(_name);
    }

    public void RandomPlay(bool VariavblePitch)
    {
        int rand = Random.Range(0, _clips.Length);

        if (VariavblePitch)
            PlayerClipWithVariablePitch(_clips[rand].name.ToString());
        else
            SimplePlay(_clips[rand].name.ToString());
    }
}
