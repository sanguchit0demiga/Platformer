using System.Collections;
using Unity.Cinemachine;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    public int health;
    public int weaponDamage;
    public Animator animator;
    public Transform target;
    public bool isAttacking;
    public float attackDistance = 4f;
    public float attackCooldown = 3f;
    private float lastAttackTime = 0f;
    private Vector3 initialPosition;
    private Quaternion initialRotation;
    private int initialHealth;
    private Color originalColor;

    void Start()
    {
        Renderer renderer = GetComponentInChildren<Renderer>();
        if (renderer != null)
        {
            originalColor = renderer.material.color;
        }
        initialPosition = transform.position;
        initialRotation = transform.rotation;
        initialHealth = health;
    }
    void Update()
    {
        if (target != null)
        {
            EnemyBehaviour();
        }
    }
    private IEnumerator HitVisualEffect()
    {
        Renderer renderer = GetComponentInChildren<Renderer>();
        if (renderer == null) yield break;

        Material mat = renderer.material;

        Vector3 originalScale = transform.localScale;

        transform.localScale = originalScale * 0.9f;
        mat.color = Color.red;
        yield return new WaitForSeconds(0.05f);

        transform.localScale = originalScale * 1.3f;
        yield return new WaitForSeconds(0.10f);

        transform.localScale = originalScale;
        mat.color = originalColor;
    }
    private void OnTriggerEnter(Collider other)
    {

        {
            if (other.CompareTag("WeaponImpact"))
            {
                health -= weaponDamage;
                StartCoroutine(HitVisualEffect());
                Debug.Log("Enemy hit! Current health: " + health);

                if (health <= 0)
                {
                    gameObject.SetActive(false);
                }
            }
        }
    }
    public void EnemyBehaviour()
    {
        if (Vector3.Distance(transform.position, target.position) <= attackDistance && !isAttacking)
        {
            if (Time.time - lastAttackTime > attackCooldown)
            {
                StartCoroutine(Attack());
                lastAttackTime = Time.time;
            }
        }
        else if (Vector3.Distance(transform.position, target.position) > attackDistance && isAttacking)
        {
          
            animator.SetBool("IsAttacking", false);
        }
        Vector3 direction = (target.position - transform.position).normalized;
        direction.y = 0; 

        if (direction != Vector3.zero)
        {
            Quaternion lookRotation = Quaternion.LookRotation(direction);
            transform.rotation = Quaternion.Slerp(transform.rotation, lookRotation, Time.deltaTime * 5f);
        }
    }

    private IEnumerator Attack()
    {
        isAttacking = true;
        animator.SetBool("IsAttacking", true);

        yield return new WaitForSeconds(1.2f);

        isAttacking = false;
        animator.SetBool("IsAttacking", false);
    }
    public void ResetEnemy()
    {
        transform.position = initialPosition;
        transform.rotation = initialRotation;
        health = initialHealth;
        gameObject.SetActive(true);

        if (animator != null)
        {
            animator.SetBool("IsAttacking", false);
        }
        Renderer renderer = GetComponentInChildren<Renderer>();
        if (renderer != null)
        {
            Material mat = renderer.material;
            mat.color = Color.white; 
        }
    }
}
