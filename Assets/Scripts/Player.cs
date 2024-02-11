using System.Diagnostics;
using Photon.Pun;
using UnityEngine;

public class Player : MonoBehaviour
{
    // Variables desplazamiento
    public float speed;
    private Rigidbody2D rig;

    // Variables disparo
    [SerializeField] private Transform shootHandler;
    [SerializeField] private GameObject bullet;

    // Variables animacion
    private Animator animator;
    public bool facingRight = true;

    // Variables salto
    public float jumpForce;
    private bool isGrounded = true;
    public float velocity;

    void Start()
    {
        if (GetComponent<PhotonView>().IsMine)
        {
            rig = GetComponent<Rigidbody2D>();
            animator = GetComponent<Animator>();
            Camera.main.transform.SetParent(transform);
            Camera.main.transform.position = transform.position + (Vector3.up) + transform.forward * -10;
        }
    }

    void Update()
    {
        if (GetComponent<PhotonView>().IsMine)
        {
            movePlayer();
            Jump();
            Fall();
            Shoot();
        }
    }

    void movePlayer()
    {
        float moveInput = Input.GetAxisRaw("Horizontal");
        rig.velocity = (transform.right * speed * moveInput * Time.deltaTime) + (transform.up * rig.velocity.y);

        bool isRunning = Mathf.Abs(rig.velocity.x) > 0.1f;
        animator.SetBool("Running", isRunning);

        if (moveInput > 0 && !facingRight)
        {
            Flip();
        }
        else if (moveInput < 0 && facingRight)
        {
            Flip();
        }
    }

    void Flip()
    {
        facingRight = !facingRight;
        GetComponent<PhotonView>().RPC("RotateSprite", RpcTarget.All, facingRight);
    }

    [PunRPC]
    private void RotateSprite(bool facingRight)
    {
        GetComponent<SpriteRenderer>().flipX = !facingRight;
    }


    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (GetComponent<PhotonView>().IsMine)
        {
            if (collision.gameObject.CompareTag("Ground"))
            {
                isGrounded = true;
                animator.SetBool("Falling", false);
                animator.SetBool("Jumping", false);

            }
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
        if (rig.velocity.y < 0 && !isGrounded)
        {
            animator.SetBool("Jumping", false);
            animator.SetBool("Falling", true);
        }
        else
        {
            animator.SetBool("Falling", false);
        }
    }

    public void Die()
    {
        Destroy(gameObject);
    }

    void Shoot()
    {
        if (Input.GetKeyDown(KeyCode.N))
        {
            Destroy(PhotonNetwork.Instantiate(bullet.name, shootHandler.position, shootHandler.rotation), 3f);
        }
    }
}
