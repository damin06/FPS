using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static BulletHole;

public class BulletHole : MonoBehaviour
{
    private static BulletHole m_Instance;

    public static BulletHole Instance
    {
        get
        {
            if (m_Instance == null) m_Instance = FindObjectOfType<BulletHole>();
            return m_Instance;
        }
    }

    public enum EffecType
    {
        Common,
        Blood
    }
    [SerializeField] private ParticleSystem commonHitEffect;
    [SerializeField] private ParticleSystem bloodHitEffect;

    private EffecType effecType;

    public void PlayHitEffect(Vector3 pos, Vector3 normal, Transform parent = null, EffecType effectType = EffecType.Common)
    {
        var targetEffect = commonHitEffect;


        switch (effecType)
        {
            case EffecType.Blood:
                targetEffect = bloodHitEffect;
                break;
        }

        var effect = Instantiate(targetEffect, pos, Quaternion.LookRotation(normal));
        effect.Emit(1);

        if (parent != null) effect.transform.SetParent(parent);
    }
}
