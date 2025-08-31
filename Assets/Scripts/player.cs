using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Audio;

public class player : LivingEntity
{
    public AudioSource audioSource;
    public AudioClip hitClip;
    public AudioClip deathClip;
    public Animator animator;

    public Slider healthSlider;

    private GameManager gameManager;

    protected override void OnEnable()
    {
        base.OnEnable();
        healthSlider.value = Health / maxHealth;
    }

    private void Start()
    {
        var findGo = GameObject.FindWithTag(Defines.GameManager);
        gameManager = findGo.GetComponent<GameManager>();
    }

    private void Update()
    {

    }

    public override void Damage(float damage, Vector3 hitPoint, Vector3 hitNoraml)
    {
        if (IsDead) return;  
        base.Damage(damage, hitPoint, hitNoraml);
        audioSource.PlayOneShot(hitClip);

        if (maxHealth == 0 || Health <= 0)
        {
            Health = 0;
        }
        healthSlider.value = Health / maxHealth;
        Debug.Log($"{damage}데미지를 받음: {Health}/P{maxHealth}");
    }

    protected override void Die()
    {
        base.Die();
        audioSource.PlayOneShot(deathClip);
        animator.SetTrigger(Defines.PlayerDie);
        gameManager.EndGame();
        Debug.Log("플레이어 사망 애니메이션 실행");
    }

    
}
