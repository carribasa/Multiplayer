using UnityEngine;

public class Player : MonoBehaviour
{
    // Variables desplazamiento
    public float speed;
    private Rigidbody2D rig;

    // Variables animacion
    private Animator animator;

    // Variables salto
    public float jumpForce;
    private bool isGrounded = true;
    public float velocity;

    void Start()
    {
        //if (GetComponent<PhotonView>().IsMine)
        //{
        //    rig = GetComponent<Rigidbody2D>();
        //}

        rig = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
    }

    void Update()
    {
        //if (GetComponent<PhotonView>().IsMine)
        //{
        //    rig.velocity = new Vector2(Input.GetAxis("Horizontal") * speed, rig.velocity.y);
        //}

        movePlayer();
        Jump();
        Fall();

        velocity = rig.velocity.y;
    }

    void movePlayer()
    {
        float inputValue = Input.GetAxisRaw("Horizontal");

        switch (inputValue)
        {
            case -1:
                rig.AddForce(Vector2.left * speed);
                animator.SetBool("Running", true);
                FlipSprite(true);
                break;
            case 0:
                animator.SetBool("Running", false);
                break;
            case 1:
                rig.AddForce(Vector2.right * speed);
                animator.SetBool("Running", true);
                FlipSprite(false);
                break;
        }
    }
    private void FlipSprite(bool flipX)
    {
        Vector3 scale = transform.localScale;
        scale.x = flipX ? -2.5f : 2.5f;
        transform.localScale = scale;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Ground"))
        {
            isGrounded = true;
            animator.SetBool("Falling", false);
            animator.SetBool("Jumping", false);
            
        }
    }

    private void Jump()
    {
        if (Input.GetKeyDown(KeyCode.Space) && isGrounded)
        {
            isGrounded = false;
            animator.SetBool("Jumping", true);
            rig.AddForce(Vector2.up * jumpForce, ForceMode2D.Impulse);
        }
    }

    void Fall()
    {
        // Verifica si el personaje está cayendo
        if (rig.velocity.y < 0)
        {
            animator.SetBool("Jumping", false);
            animator.SetBool("Falling", true);
        }
        else
        {
            animator.SetBool("Falling", false);
        }
    }
}
