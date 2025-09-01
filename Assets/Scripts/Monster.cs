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

    public Transform target;            // �÷��̾��� Transform�� �Ҵ��� ����
    public float updateInterval = 0.5f; // ������ ���� �ֱ�


    public new Collider collider;
    public NavMeshAgent agent;
    private bool isSinking = false;     // ���Ͱ� ħ�� ������ ����
    public float sinkSpeed = 2.5f;      // ����ɴ� �ӵ�


    public float damage = 20f;          // ������ ���ݷ�
    public float attackInterval = 0.3f; // ���� ����
    private float attackTimer = 0f;     // ���� Ÿ�̸�
    private bool isAttack = true;       // ����

    public int score;

    public ParticleSystem hitEffect;    // �ǰ� ����Ʈ

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
        Debug.Log($"���� {damage} �������� ����: {Health}/P{maxHealth}");

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
        Debug.Log("���� ��� �ִϸ��̼� ����");
    }

    public void StartSinking()
    {
        if (agent)
        {
            agent.enabled = false; // NavMeshAgent ��Ȱ��ȭ
        }
        if (collider)
        {
            collider.enabled = false; // �浹ü ��Ȱ��ȭ
        }
        isSinking = true;

        Destroy(gameObject, 2f); // 2�� �Ŀ� ������Ʈ ����
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
                    Debug.Log("�÷��̾�� ������ ����");
                    isAttack = false;
                    attackTimer = 0f;
                }
                
            }
        }
    }
}
