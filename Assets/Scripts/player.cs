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

    private void Start()
    {
    }

    private void Update()
    {
        // �׽�Ʈ�� ������ �ޱ�
        if (Input.GetKeyDown(KeyCode.Space))
        {
            TakeDamage(10f);
        }
    }

    public override void TakeDamage(float damage)
    {
        if (IsDead) return;  
        base.TakeDamage(damage);
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
