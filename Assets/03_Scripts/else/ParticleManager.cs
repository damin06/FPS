using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class ParticleManager : PoolableMono
{
    private ParticleSystem _par;

    protected virtual void Awake()
    {
        _par = GetComponent<ParticleSystem>();
    }

    public override void Reset()
    {
        gameObject.transform.SetParent(null);
        StartCoroutine(SetActive());
        Play();
    }

    public virtual void Play()
    {
        _par.Play();
    }

    IEnumerator SetActive()
    {
        yield return new WaitForSeconds(_par.duration);
        gameObject.transform.SetParent(gameObject.transform.Find("GameManager"));
        PoolManager.Instance.Push(this);
    }
}
