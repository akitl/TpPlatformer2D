using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// script de l'ennemi slime
// pour ce script j'ai repris ce qui etais fait pour le player controller
public class SlimeScript : MonoBehaviour
{
    // l'ennemie regarde a gauche par defaut
    private bool m_FacingRight = false;
    private Rigidbody2D m_Rigidbody2D;
    private Animator animator;

    [Range(0, .3f)] [SerializeField] private float m_MovementSmoothing = .05f;  // How much to smooth out the movement
    private Vector3 m_Velocity = Vector3.zero;


    public float runSpeed = 10f;

    // comme il regarde a gauche on indique dont la direction a -1
    private int direction = -1;


    void Start()
    {
        m_Rigidbody2D = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {

        // dans le player controller + player movement on utilise "Input.GetAxisRaw("Horizontal")" ici c'est direction qui le remplace
        var horizontalMove = direction * runSpeed;
        var move = horizontalMove * Time.fixedDeltaTime;
        animator.SetFloat("SlimeSpeed", Mathf.Abs(horizontalMove));

        Vector3 targetVelocity = new Vector2(move * 10f, m_Rigidbody2D.velocity.y);
        // And then smoothing it out and applying it to the character
        m_Rigidbody2D.velocity = Vector3.SmoothDamp(m_Rigidbody2D.velocity, targetVelocity, ref m_Velocity, m_MovementSmoothing);

        // If the input is moving the player right and the player is facing left...
        if (move > 0 && !m_FacingRight)
        {
            // ... flip the player.
            Flip();
        }
        // Otherwise if the input is moving the player left and the player is facing right...
        else if (move < 0 && m_FacingRight)
        {
            // ... flip the player.
            Flip();
        }
    }


    // quand le slime renonctre un triger 
    private void OnTriggerEnter2D(Collider2D other)
    {
        // si c'est un trigger limite
        if (other.tag == "Limite")
        {
            // alors on inverse la direction
            direction *= -1;
        }
    }

    private void Flip()
    {
        // Switch the way the player is labelled as facing.
        m_FacingRight = !m_FacingRight;

        // Multiply the player's x local scale by -1.
        Vector3 theScale = transform.localScale;
        theScale.x *= -1;
        transform.localScale = theScale;
    }
}
