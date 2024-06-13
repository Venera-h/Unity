using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DamageComponent : MonoBehaviour
{
    [SerializeField] float damage;

    // посылает урон (пытается взять HealtComponent и дает через него урон)
    public void DealDamage(GameObject obj)
    {
        if(obj.TryGetComponent(out HealtComponent hp))

        hp.GetDamage(damage);
    }
}
