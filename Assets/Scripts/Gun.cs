using System.Collections;
using UnityEngine;

public class Gun : MonoBehaviour
{
    private LineRenderer lineRenderer;
    public Transform firePosition;

    // �Ҹ�
    public AudioSource audioSource;
    public AudioClip fireSound;

    // �� ����
    public float fireDistance = 50f;
    public float timeBetFire = 0.12f;   // ���� �ӵ�
    public float beamDuration = 0.1f;   // ���� ǥ�� �ð�
    public float damage = 10f;

    public Transform gunParticle; 
    public Transform gunBarrelEnd; 

    // �� ����Ʈ
    public ParticleSystem fireEffect;

    private float lastFireTime;
    private void Awake()
    {
        lineRenderer = GetComponent<LineRenderer>();
        lineRenderer.enabled = false;
        lineRenderer.positionCount = 2; // �������� ���� �� ��

        lastFireTime = 0f;
    }


    public void Update()
    {
        if (Input.GetMouseButton(0)) // ��Ŭ��
        {
            if (Time.time > (lastFireTime + timeBetFire))
            {
                if (Time.timeScale == 0f)
                {
                    return; // ���� �Ͻ����� ���¿����� �߻����� ����
                }
                Fire();
                lastFireTime = Time.time;
            }
        }
    }


    private IEnumerator CoGunEffect(Vector3 hitPosition)
    {
        audioSource.PlayOneShot(fireSound);
        gunParticle.position = gunBarrelEnd.position;
        fireEffect.Play();
        lineRenderer.enabled = true;

        float t = 0f;
        while (t < beamDuration)
        {
            lineRenderer.SetPosition(0, firePosition.position);
            lineRenderer.SetPosition(1, hitPosition);
            t += Time.deltaTime;
            yield return null;
        }
        lineRenderer.enabled = false;
    }

    public void Fire()
    {
        Vector3 hitPosition = Vector3.zero;
        RaycastHit hit;
        if (Physics.Raycast(firePosition.position, firePosition.forward, out hit, fireDistance))
        {
            hitPosition = hit.point;
            IDamage target = hit.collider.GetComponent<IDamage>();
            if (target != null)
            {
                target.Damage(damage, hitPosition, hit.normal);
            }
        }
        else
        {
            hitPosition = firePosition.position + firePosition.forward * fireDistance;
        }
        StartCoroutine(CoGunEffect(hitPosition));
    }
}

