using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static BulletHole;

public class BulletHole : MonoBehaviour
{
    private static BulletHole m_Instance;
    [SerializeField] private LayerMask _lay;
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

    public void PlayHitEffect(Vector3 pos, Vector3 normal, Transform parent = null)
    {
        var targetEffect = commonHitEffect;

        var target = parent.gameObject.GetComponent<IDamage>();


        if (parent.gameObject.layer == LayerMask.NameToLayer("Enemy"))
        {
            targetEffect = bloodHitEffect;
        }
        // if (target != null)
        // {
        //     targetEffect = bloodHitEffect;
        // }

        // switch (parent.gameObject.layer)
        // {
        //     case  :
        //         targetEffect = commonHitEffect;
        //         break;
        //     case EffecType.Blood:
        //         targetEffect = bloodHitEffect;
        //         break;
        // }

        var effect = Instantiate(targetEffect, pos, Quaternion.LookRotation(normal));
        effect.Emit(1);

        if (parent != null) effect.transform.SetParent(parent);
    }
}
