using UnityEngine;
using UnityEngine.AI;
using UnityEngine.Audio;
using UnityEngine.Splines;

public class Monster : LivingEntity, IDamage
{
    public Animator animator;

    public AudioSource audioSource;
    public AudioClip hitClip;
    public AudioClip deathClip;

    public Transform target;            // 플레이어의 Transform을 할당할 변수
    public float updateInterval = 0.5f; // 목적지 갱신 주기


    public new Collider collider;
    public NavMeshAgent agent;
    private bool isSinking = false;     // 몬스터가 침몰 중인지 여부
    public float sinkSpeed = 2.5f;      // 가라앉는 속도


    public float damage = 20f;          // 몬스터의 공격력
    public float attackInterval = 0.3f; // 공격 간격
    private float attackTimer = 0f;     // 공격 타이머
    private bool isAttack = true;       // 공격

    public int score;

    public ParticleSystem hitEffect;    // 피격 이펙트

    private GameManager gameManager;

    public void Awake()
    {
        target = GameObject.FindWithTag(Defines.Player).transform;
    }
    protected override void OnEnable()
    {
        base.OnEnable();
        isAttack = true;
    }

    private void Start()
    {
        var findGo = GameObject.FindWithTag(Defines.GameManager);
        gameManager = findGo.GetComponent<GameManager>();
    }

    private void Update()
    {
        if (!isSinking)
        {
            animator.SetFloat("MonsterMove", agent.velocity.magnitude);
            agent.SetDestination(target.position);
        }
        if (isSinking)
        {
            transform.Translate(-Vector3.up * 2f * sinkSpeed * Time.deltaTime);
        }
    }

    public override void Damage(float damage, Vector3 hitPoint, Vector3 hitNoraml)
    {
        if (IsDead) return;
        base.Damage(damage, hitPoint, hitNoraml);
        audioSource.PlayOneShot(hitClip);
        Debug.Log($"좀비가 {damage} 데미지를 받음: {Health}/P{maxHealth}");

        hitEffect.transform.position = hitPoint;
        hitEffect.transform.forward = hitNoraml;
        hitEffect.Play();
    }

    protected override void Die()
    {
        base.Die();
        audioSource.PlayOneShot(deathClip);
        animator.SetTrigger(Defines.MonsterDie);
        gameManager.AddScore(score);
        StartSinking();
        Debug.Log("몬스터 사망 애니메이션 실행");
    }

    public void StartSinking()
    {
        if (agent)
        {
            agent.enabled = false; // NavMeshAgent 비활성화
        }
        if (collider)
        {
            collider.enabled = false; // 충돌체 비활성화
        }
        isSinking = true;

        Destroy(gameObject, 2f); // 2초 후에 오브젝트 제거
    }

    public void OnCollisionStay(Collision collision)
    {
        if (collision.gameObject.CompareTag(Defines.Player))
        {
            player player = collision.gameObject.GetComponent<player>();
            if (player != null)
            {
                attackTimer += Time.deltaTime;
                if (attackTimer >= attackInterval || isAttack)
                {
                    player.Damage(damage, Vector3.zero, Vector3.zero);
                    Debug.Log("플레이어에게 데미지 입힘");
                    isAttack = false;
                    attackTimer = 0f;
                }
                
            }
        }
    }
}
