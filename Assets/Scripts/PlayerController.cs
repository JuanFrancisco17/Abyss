using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    //Variables componentes
    private Rigidbody2D _rb2d;
    private SpriteRenderer sprite;

    //Variables movimiento

    [SerializeField]
    float speed;

    //Variables salto
    public bool jumpRequest;
    [SerializeField]
    float jumpForce;
    [SerializeField]
    float fallMultiplier, lowJumpMultiplier;

    void Start()
    {
        _rb2d = GetComponent<Rigidbody2D>();
        sprite = GetComponent<SpriteRenderer>();
    }

    void Update()
    {
        if (Input.GetButtonDown("Vertical"))
        {
            jumpRequest = true;
        }
    }

    void FixedUpdate()
    {
        float axisH = Input.GetAxisRaw("Horizontal");
        _rb2d.velocity = new Vector2(axisH * speed, _rb2d.velocity.y);

        if (axisH > 0)
        {
            sprite.flipX = false;
        }
        else if (axisH < 0)
        {
            sprite.flipX = true;
        }

        //salto basico
        if (jumpRequest)
        {
            //rb2d.velocity += Vector2.up * jumpForce;
            _rb2d.AddForce(Vector2.up * jumpForce, ForceMode2D.Impulse);
            jumpRequest = false;
        }

        //salto mejorado
        if (_rb2d.velocity.y < 0)
        {
            _rb2d.velocity += Vector2.up * Physics2D.gravity.y * (fallMultiplier - 1) * Time.deltaTime;
        }
        else if (_rb2d.velocity.y > 0 && !Input.GetButton("Up"))
        {
            _rb2d.velocity += Vector2.up * Physics2D.gravity.y * (lowJumpMultiplier - 1) * Time.deltaTime;
        }
    }

    void OnCollisionEnter(Collision other) {
        
    }
}
