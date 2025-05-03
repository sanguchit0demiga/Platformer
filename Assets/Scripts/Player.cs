using JetBrains.Annotations;
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
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        animator = GetComponent<Animator>();

        if (healthbar != null)
        {
            healthbar.maxHealth = health;
            healthbar.currentHealth = health;
            healthbar.UpdateBar();
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
            health-=10;
        }
            if (healthbar != null)
            {
                healthbar.currentHealth = health;
                healthbar.UpdateBar();
            }
            if (health <= 0)
            {
                gameController.PlayerDefeated();
            }


        }
    }



