using UnityEngine;

public interface IDamage
{
    void Damage(float damage, Vector3 hitPoint, Vector3 hitNormal);
}
