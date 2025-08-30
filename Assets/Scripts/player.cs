using UnityEngine;
using UnityEngine.Audio;

public class player : LivingEntity
{
    public AudioSource audioSource;
    public AudioClip hitClip;
    public AudioClip deathClip;
    public Animator animator;

    protected override void OnEnable()
    {
        base.OnEnable();
        
    }


    private void Update()
    {
        // �׽�Ʈ�� ������ �ޱ�
        if (Input.GetKeyDown(KeyCode.Space))
        {
            Damage(10f, Vector3.zero, Vector3.zero);
        }
    }

    public override void Damage(float damage, Vector3 hitPoint, Vector3 hitNoraml)
    {
        if (IsDead) return;  
        base.Damage(damage, hitPoint, hitNoraml);
        audioSource.PlayOneShot(hitClip);
        Debug.Log($"{damage}�������� ����: {Health}/P{maxHealth}");
        
    }

    protected override void Die()
    {
        base.Die();
        audioSource.PlayOneShot(deathClip);
        animator.SetTrigger(Defines.PlayerDie);
        Debug.Log("�÷��̾� ��� �ִϸ��̼� ����");
    }

    
}
