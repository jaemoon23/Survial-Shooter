using UnityEngine;
using UnityEngine.Events;

public class LivingEntity : MonoBehaviour
{
    public float maxHealth = 100f;
    public float Health { get; protected set; }
    public bool IsDead { get; private set; }


    protected virtual void OnEnable()
    {
        IsDead = false;
        Health = maxHealth;
    }

    public virtual void TakeDamage(float damage)
    {
        Health -= damage;
        if (Health <= 0f && !IsDead)
        {
            Die();
        }
    }

    protected virtual void Die()
    {
        IsDead = true;
    }
}
