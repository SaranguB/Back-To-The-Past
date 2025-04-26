using UnityEngine;
using Wepons.Bomb;

public class BombDamageArea : MonoBehaviour
{
    private int bombDamage;
    private CapsuleCollider2D damageAreaCollider;

    public void SetDamageValues(float damageRadius, int bombDamage)
    {
        SetDamageRadius(damageRadius);
        SetBombDamage(bombDamage);
    }

    private void SetBombDamage(int bombDamage)
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
            damagable.TakeDamage(bombDamage);
        }
    }

    public void ChangeColliderState(bool value)
    {
        if (damageAreaCollider != null)
            damageAreaCollider.enabled = value;
    }
}
