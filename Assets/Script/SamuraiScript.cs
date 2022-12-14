using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SamuraiScript : MonoBehaviour
{
    public Animator anim;
    Rigidbody2D rb;
    public GameObject AttackBox, trunk;

    public Transform groundCheckCollider;

    public LayerMask groundLayer;

    public bool attack = false;

    public float dash;

    const float groundCheckRadius = 0.01f;
    int availableJumps;
    [SerializeField] float jumpPower = 500;
    public int totalJumps;
    public int limiar;


    public bool isGrounded = true;
    bool coyoteJump;
    bool multipleJump;

    private float speed = 10.0f;
    // Start is called before the first frame update
    void Start()
    {
        availableJumps = totalJumps;

        limiar = Screen.width;

        rb = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();

    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.W))
        {
            Jump();
        }
       
        if(Input.touchCount > 0 && Input.GetTouch(0).phase == TouchPhase.Began)
        {
            var touch = Input.GetTouch(0);
            if(touch.position.x > Screen.width / 2)
            {
                Jump();
            }
            else if(isGrounded == true)
            {
                anim.SetBool("IsDashing", true);
                attack = true;
            }

        }
        
        if(anim.GetCurrentAnimatorStateInfo(0).IsName("Attack") && attack == true)
        {
            StartCoroutine(endAnimation());
        }
        
        anim.SetFloat("yVelocity", rb.velocity.y);
    }

    IEnumerator endAnimation()
    {
        yield return new WaitForSeconds(0.1f);
        attack = false;
        anim.SetBool("IsDashing", false);

    }

    void FixedUpdate()
    {
        GroundCheck();
    }

    void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.yellow;
        Gizmos.DrawSphere(groundCheckCollider.position, groundCheckRadius);
    }

    void GroundCheck()
    {
        bool wasGrounded = isGrounded;
        isGrounded = false;
        Collider2D[] colliders = Physics2D.OverlapCircleAll(groundCheckCollider.position, groundCheckRadius, groundLayer);
        if (colliders.Length > 0)
        {
            isGrounded = true;

            if (!wasGrounded)
            {
                availableJumps = totalJumps;
                multipleJump = false;
            }
        }
        anim.SetBool("Jump", !isGrounded);
    }


    #region Jump
    IEnumerator CoyoteJumpDelay()
    {
        coyoteJump = true;
        yield return new WaitForSeconds(0.2f);
        coyoteJump = false;
    }

    void Jump()
    {
        if (isGrounded)
        {
            multipleJump = true;
            availableJumps--;

            rb.velocity = Vector2.up * jumpPower;
            anim.SetBool("Jump", true);
        }
        else
        {
            if (coyoteJump)
            {
                multipleJump = true;
                availableJumps--;

                rb.velocity = Vector2.up * jumpPower;
                anim.SetBool("Jump", true);
            }

            if (multipleJump && availableJumps > 0)
            {
                availableJumps--;

                rb.velocity = Vector2.up * jumpPower;
                anim.SetBool("Jump", true);
            }
        }
    }
    #endregion

    #region Colliders

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.gameObject.tag == "Tronco")
        {
            Destroy(this.gameObject);
        }
    }

    #endregion

}
