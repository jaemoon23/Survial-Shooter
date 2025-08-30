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
        // 테스트용 데미지 받기
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
        Debug.Log($"{damage}데미지를 받음: {Health}/P{maxHealth}");
        
    }

    protected override void Die()
    {
        base.Die();
        audioSource.PlayOneShot(deathClip);
        animator.SetTrigger(Defines.PlayerDie);
        Debug.Log("플레이어 사망 애니메이션 실행");
    }

    
}
