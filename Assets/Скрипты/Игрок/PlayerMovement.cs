using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public float speed;
    public float smoothRot;
    public LayerMask layerGround;
    [HideInInspector] public float horizontal;
    [HideInInspector] public bool facingRight;

    public float jumpForce;
    public PlayerCheckGround checkGround;

    [HideInInspector] public bool isWork = true;
    float directorHorizontal = 0;

    Rigidbody2D rb;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();

        facingRight = transform.localScale.x >= 0 ? true : false;
    }

    void FixedUpdate()
    {
        horizontal = isWork ? Input.GetAxis("Horizontal") : directorHorizontal;
        rb.velocity = new Vector2(horizontal * speed, rb.velocity.y);
    }
 
    void Update()
    {
        RaycastHit2D hit = Physics2D.Raycast(transform.position, Vector2.down, 2.5f, layerGround);
        if (hit.collider != null)
        {
            float slopeAngel = Mathf.Rad2Deg * Mathf.Atan2(hit.normal.x, hit.normal.y);
            transform.rotation = Quaternion.Lerp(transform.rotation, Quaternion.Euler(0, 0, -slopeAngel), smoothRot * Time.deltaTime);
        }

        if (Input.GetKey(KeyCode.W) && checkGround.isGround && isWork)
        {
            rb.velocity = Vector2.up * jumpForce;
        }

        if (horizontal > 0 && facingRight != true)
        {
            Flip();
        }
        else if (horizontal < 0 && facingRight != false)
        {
            Flip();
        }
    }

    void Flip()
    {
        facingRight = !facingRight;
        transform.localScale = new Vector2(-transform.localScale.x, transform.localScale.y);
    }

    public void DirectorLeftRun()
    {
        directorHorizontal = -0.56f;
    }

    public void DirectorRightRun()
    {
        directorHorizontal = 0.56f;
    }

    public void DirectorStopRun ()
    {
        directorHorizontal = 0;
    }

    public void SwitchStateWork()
    {
        isWork = !isWork;
    }
}
