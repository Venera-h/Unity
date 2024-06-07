using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class HealtComponent : MonoBehaviour
{
    [SerializeField] float healt;
    [SerializeField] UnityEvent Dieaction;
    public void GetDamage(float damage)
    {
        healt -= damage;
        if (healt <= 0) Die();
    }

    public void Die()
    {
        Dieaction?.Invoke();
        Invoke(nameof(DestroyObject), 1f);
    }

    void DestroyObject()
    {
        Destroy(gameObject);
    }
}
