using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    //Variables de componentes
    Rigidbody2D rb2d;
    //Variables de movimiento
    public float speed;
    //Variables de salto
    private bool _isGround;
    public float jumpForce;
    public float fallMultiplier = 2.5f;
    public float lowJumpMultiplier = 2f;
    float coyoteTimer = 0;
    public float maxCoyoteTime = 0.25f;
    bool _isCoyoteTime = false;
    private bool _jumpRequest = false;

    void Start()
    {
        rb2d = GetComponent<Rigidbody2D>();
    }

    void Update()
    {

        if (Input.GetButtonDown("Up") && _isGround == true)
        {
            _jumpRequest = true;
            _isGround = false;
        }

        //contador Coyote Time
        if (_isCoyoteTime)
        {
            coyoteTimer += Time.deltaTime;
        }

        //Fin Coyote Time
        if (coyoteTimer >= maxCoyoteTime)
        {
            _isCoyoteTime = false;
            coyoteTimer = 0;
        }
    }

    void FixedUpdate()
    {

        float axisH = Input.GetAxisRaw("Horizontal");
        transform.position += new Vector3(axisH * speed, 0) * Time.deltaTime;

        //salto basico
        if (_jumpRequest)
        {
            _jumpRequest = false;
            rb2d.AddForce(Vector2.up * jumpForce, ForceMode2D.Impulse);
        }

        //salto mejorado
        if (rb2d.velocity.y < 0)
        {
            rb2d.velocity += Vector2.up * Physics2D.gravity.y * (fallMultiplier - 1) * Time.deltaTime;
        }
        else if (rb2d.velocity.y > 0 && !Input.GetButton("Up"))
        {
            rb2d.velocity += Vector2.up * Physics2D.gravity.y * (lowJumpMultiplier - 1) * Time.deltaTime;
        }
    }

    void OnCollisionExit2D(Collision2D collision)
    {
        _isCoyoteTime = true;

    }

    void OnCollisionStay2D(Collision2D other) 
    {
        if (other.collider.tag == "Ground")
        {
            _isGround = true;
        }
    }
}
