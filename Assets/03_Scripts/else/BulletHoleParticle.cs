using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletHoleParticle : ParticleManager
{
    private ParticleSystem[] _particles;

    protected override void Awake()
    {
        base.Awake();
        _particles = GetComponentsInChildren<ParticleSystem>();
    }

    public override void Play()
    {
        base.Play();
        foreach (ParticleSystem _par in _particles)
        {
            _par.Play();
        }
    }
}
