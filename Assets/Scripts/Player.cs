using System.Diagnostics;
using Photon.Pun;
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
            velocity = rig.velocity.y;
        }

    }

    void movePlayer()
    {
        rig.velocity = (transform.right * speed * Input.GetAxisRaw("Horizontal")) + (transform.up * rig.velocity.y);

        bool isRunning = Mathf.Abs(rig.velocity.x) > 0.1f;
        animator.SetBool("Running", isRunning);

        if (rig.velocity.x > 0.1f)
            GetComponent<PhotonView>().RPC("RotateSprite", RpcTarget.All, false);
        else if (rig.velocity.x < 0.1f)
            GetComponent<PhotonView>().RPC("RotateSprite", RpcTarget.All, true);
    }

    [PunRPC]
    private void RotateSprite(bool rotate)
    {
        GetComponent<SpriteRenderer>().flipX = rotate;
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
        // Verifica si el personaje esta cayendo
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
}
