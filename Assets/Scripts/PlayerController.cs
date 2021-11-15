using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    //----VARIABLES DE COMPONENTES----
    Rigidbody2D rb2d;
    
    //----VARIABLES DE MOVIMIENTO----

    //Velocidad del jugador
    [SerializeField]
    float speed;
    
    //----VARIABLES DE SALTO----
    
    //Variable que comprueba si el jugador está en el suelo
    public bool _isGround;
    //Fuerza del salto
    [SerializeField]
    float jumpForce;
    //Fuerza que tiene el jugador al caer en el suelo
    public float fallMultiplier = 2.5f;
    //Fuerza pequeña para saltar poco
    public float lowJumpMultiplier = 2f;
    //Contador del Coyote Time
    float coyoteTimer = 0;
    //Tiempo maximo del Coyote Time
    public float maxCoyoteTime = 0.25f;
    //Variable que comprueba si esta el jugador en el Coyote Time
    private bool _isCoyoteTime = false;
    //Variable para ver si el jugador quiere saltar
    public bool _jumpRequest = false;

    void Start()
    {
        rb2d = GetComponent<Rigidbody2D>();
    }
    void Update()
    {
        //Comprobamos si puede saltar
        if (Input.GetButtonDown("Up") && _isGround == true)
        {
            _jumpRequest = true;
            _isGround = false;
        }

        //Contador Coyote Time
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
        //Registramos los ejes horizontales y movemos al jugador
        float axisH = Input.GetAxisRaw("Horizontal");
        transform.position += new Vector3(axisH * speed, 0) * Time.deltaTime;

        //Salto basico
        if (_jumpRequest == true)
        {
            rb2d.AddForce(Vector2.up * jumpForce, ForceMode2D.Impulse);
            _jumpRequest = false;
        }

        //Salto mejorado
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
