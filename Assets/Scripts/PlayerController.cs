using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.SceneManagement;

// le script vien comme je vous l'avez indiqué en grande partie de la video de brackeys "https://www.youtube.com/watch?v=dwcT-Dch0bA" c'est un version simplifié que j'ai réaliser
// je vais commenter les partie ajouter pour le tp 
public class PlayerController : MonoBehaviour
{
    [SerializeField] private float m_JumpForce = 400f;                          // Amount of force added when the player jumps.
    [Range(0, .3f)] [SerializeField] private float m_MovementSmoothing = .05f;  // How much to smooth out the movement
    [SerializeField] private LayerMask m_WhatIsGround;                          // A mask determining what is ground to the character
    [SerializeField] private Transform m_GroundCheck;                           // A position marking where to check if the player is grounded.

    const float k_GroundedRadius = .2f; // Radius of the overlap circle to determine if grounded
    private bool m_Grounded;            // Whether or not the player is grounded.
    private Rigidbody2D m_Rigidbody2D;
    private bool m_FacingRight = true;  // For determining which way the player is currently facing.
    private Vector3 m_Velocity = Vector3.zero;

    private void Awake()
    {
        m_Rigidbody2D = GetComponent<Rigidbody2D>();
    }

    private void FixedUpdate()
    {
        m_Grounded = false;

        // The player is grounded if a circlecast to the groundcheck position hits anything designated as ground
        // This can be done using layers instead but Sample Assets will not overwrite your project settings.
        Collider2D[] colliders = Physics2D.OverlapCircleAll(m_GroundCheck.position, k_GroundedRadius, m_WhatIsGround);
        for (int i = 0; i < colliders.Length; i++)
        {
            if (colliders[i].gameObject != gameObject)
            {
                m_Grounded = true;
            }
        }
    }

    // ici on gére les colision avec les ennemies 
    void OnCollisionEnter2D(Collision2D col)
    {
        // si on rentre en colision avec un collider tagé Ennemie
        if (col.gameObject.tag == "Ennemie")
        {
            // on récupére tout les collider present a proximité imediate de notre groundCheck ( pied du joueur) dans un rayon de 0.2f
            Collider2D[] colliders = Physics2D.OverlapCircleAll(m_GroundCheck.position, k_GroundedRadius, m_WhatIsGround);
            // pour chaquun de ces collider
            for (int i = 0; i < colliders.Length; i++)
            {
                // si on trouve au pied de notre joueur un ennemie
                if (colliders[i].gameObject.tag == "Ennemie")
                {
                    // on detruit l'ennemie
                    Destroy(col.gameObject);
                }
                else
                {
                    // si l'ennemie n'est pas au nivaux de notre ground check

                    //si on as plus que 1 point de vie
                    if (GameManager.Instance.p.life == 1)
                    {
                        // on réduit la vie
                        GameManager.Instance.p.life--;

                        // on detruit le joueur
                        Destroy(gameObject);

                        // on renvoi au main menu
                        SceneManager.LoadScene("MainMenu");
                        
                    }
                    else
                    {
                        // on réduit la vie
                        GameManager.Instance.p.life--;
                    }
                }
            }
        }
    }

    public void Move(float move, bool jump)
    {

        // Move the character by finding the target velocity
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

        // If the player should jump...
        if (m_Grounded && jump)
        {
            // Add a vertical force to the player.
            m_Grounded = false;
            m_Rigidbody2D.AddForce(new Vector2(0f, m_JumpForce));
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
