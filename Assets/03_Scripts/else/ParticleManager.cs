using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class ParticleManager : PoolableMono
{
    private ParticleSystem _par;
    private ParticleSystem[] _particles;

    protected virtual void Awake()
    {
        _par = GetComponent<ParticleSystem>();
        _particles = GetComponentsInChildren<ParticleSystem>();
    }

    public override void Reset()
    {
        //gameObject.transform.SetParent(null);
        Play();
        StartCoroutine(SetActive());
    }

    public virtual void Play()
    {
        //_par.Play();
        _par.Emit(1);
        // foreach (ParticleSystem _par in _particles)
        // {
        //     _par.Emit(1);
        // }
    }

    IEnumerator SetActive()
    {
        yield return new WaitForSeconds(_par.duration);
        gameObject.transform.SetParent(gameObject.transform.Find("GameManager"));
        PoolManager.Instance.Push(this);
    }

    private void OnDisable()
    {
        // gameObject.transform.SetParent(gameObject.transform.Find("GameManager"));
    }
}
