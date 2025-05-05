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
        Material mat = renderer.material;

        Vector3 originalScale = transform.localScale;
        Color originalColor = mat.color;


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
                    Destroy(gameObject);
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
    }

    private IEnumerator Attack()
    {
        isAttacking = true;
        animator.SetBool("IsAttacking", true);

        yield return new WaitForSeconds(1.2f);

        isAttacking = false;
        animator.SetBool("IsAttacking", false);
    }
}
