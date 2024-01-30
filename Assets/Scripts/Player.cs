using UnityEngine;

public class Player : MonoBehaviour
{
    // Variables desplazamiento
    public float speed;
    private Rigidbody2D rig;
    private bool flipX = false;

    void Start()
    {
        //if (GetComponent<PhotonView>().IsMine)
        //{
        //    rig = GetComponent<Rigidbody2D>();
        //}

        rig = GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        //if (GetComponent<PhotonView>().IsMine)
        //{
        //    rig.velocity = new Vector2(Input.GetAxis("Horizontal") * speed, rig.velocity.y);
        //}

        movePlayer();
    }

    void movePlayer()
    {
        float inputValue = Input.GetAxisRaw("Horizontal");

        switch (inputValue)
        {
            case -1:
                rig.AddForce(Vector2.left * speed);
                FlipSprite(true);
                break;
            case 0:
                break;
            case 1:
                rig.AddForce(Vector2.right * speed);
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
}
