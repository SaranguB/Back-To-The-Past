using System;
using UnityEngine;
using Wepons.Bomb;

public class BombDamageArea : MonoBehaviour
{
    private float bombDamage;
    private CapsuleCollider2D damageAreaCollider;


    public void SetDamageValues(float damageRadius, float bombDamage)
    {
        SetDamageRadius(damageRadius);
        SetBombDamage(bombDamage);
    }

    private void SetBombDamage(float bombDamage)
    {
        this.bombDamage = bombDamage;
    }

    private void SetDamageRadius(float damageRadius)
    {
        damageAreaCollider = GetComponent<CapsuleCollider2D>();

        Vector2 newSize = damageAreaCollider.size;
        newSize.x = damageRadius;
        damageAreaCollider.size = newSize;
    }


    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.TryGetComponent<IDamagableFromBomb>(out var damagable))
        {
            damagable.TakeDamageFromBomb(bombDamage);
        }
    }

    public void ChangeColliderState(bool value)
    {
        if (damageAreaCollider != null)
            damageAreaCollider.enabled = value;
    }
}
