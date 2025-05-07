
using System.Collections;
using UnityEngine;
using UnityEngine.Rendering;

public class Player : MonoBehaviour
{
    public float speed;
    public float jumpForce;
    private Rigidbody rb;
    private Animator animator;
    private bool isGrounded;
    public int health = 100;
    public GameController gameController;
    public HealthBar healthbar;
    public GameObject weapon;
    private bool isAttacking;
    private Vector3 respawnPoint;



    void Start()
    {
        rb = GetComponent<Rigidbody>();
        animator = GetComponent<Animator>();
        respawnPoint = transform.position;

        if (healthbar != null)
        {
            healthbar.maxHealth = health;
            healthbar.currentHealth = health;
            healthbar.UpdateBar();
        }
        if (weapon != null)
        {
            weapon.SetActive(false);
        }
    }

    void Update()
    {
        Movement();
        Rotation();
        Jump();
        Attack();
    }

    private void Movement()
    {

        float horizontal = Input.GetAxisRaw("Horizontal");
        float vertical = Input.GetAxisRaw("Vertical");
        Vector3 movement = new Vector3(horizontal, 0, vertical).normalized * speed;
        Vector3 newVelocity = new Vector3(movement.x, rb.linearVelocity.y, movement.z);
        rb.linearVelocity = newVelocity;
        bool isMoving = movement.magnitude > 0.1f;
        animator.SetBool("IsRunning", isMoving);
    }

    private void Rotation()
    {
        float horizontal = Input.GetAxisRaw("Horizontal");
        float vertical = Input.GetAxisRaw("Vertical");
        Vector3 movement = new Vector3(horizontal, 0, vertical);
        if (movement.magnitude > 0.1f)
        {
            Quaternion toRotation = Quaternion.LookRotation(movement, Vector3.up);
            transform.rotation = Quaternion.Slerp(transform.rotation, toRotation, 10f * Time.deltaTime);
        }

    }
    private void Jump()
    {
        ;
        if (Input.GetKeyDown(KeyCode.Space) && isGrounded)
        {
            animator.SetTrigger("IsJumping");
            rb.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);


            isGrounded = false;
        }
    }
    private void Attack()
    {



        if (Input.GetMouseButtonDown(0))
        {
            animator.SetTrigger("Attack");
            animator.SetBool("IsRunning", false);
            StartCoroutine(ActivateWeapon());
        }

    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Ground"))
        {
            isGrounded = true;

        }

        if (collision.gameObject.CompareTag("Enemy"))
        {
            health -= 25;
        }
        if (collision.gameObject.CompareTag("Heart"))
        {
            health += 25;


            if (health > 100)
            {
                health = 100;
            }

            collision.gameObject.SetActive(false);
        }
        if (healthbar != null)
        {
            healthbar.currentHealth = health;
            healthbar.UpdateBar();
        }
        if (health <= 0)
        {
            Die();
        }


    }
    private IEnumerator ActivateWeapon()
    {
        if (weapon != null)
        {
            isAttacking = true;
            weapon.SetActive(true);
            yield return new WaitForSeconds(2f);
            weapon.SetActive(false);
            isAttacking = false;


        }

    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Die"))
        {
            Debug.Log("Zona de muerte tocada.");
            Die();
        } 
        if (other.CompareTag("Start"))
        {
            Debug.Log("Colisi�n con Start detectada desde el jugador.");
            Destroy(other.gameObject);
            if (gameController != null)
            {
                gameController.StartCountdown();
            }

        }
    }
    public void UpdateCheckpoint(Vector3 newCheckpoint)
    {
        respawnPoint = newCheckpoint;
    }
    private void Die()
    {
        if (gameController != null && gameController.GetTimeRemaining() > 0f)
        {
          
            transform.position = respawnPoint;
            rb.linearVelocity = Vector3.zero;
            health = 100;

            if (healthbar != null)
            {
                healthbar.currentHealth = health;
                healthbar.UpdateBar();
            }
            gameController.ResetCollectibles();
        }
        else
        {
            
            gameController.PlayerDefeated();
        }
    }

}



