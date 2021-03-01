using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// le script vien comme je vous l'avez indiqué en grande partie de la video de brackeys "https://www.youtube.com/watch?v=dwcT-Dch0bA" c'est un version simplifié que j'ai réaliser
public class PlayerMovment : MonoBehaviour
{

    public PlayerController controller;
    public float runSpeed = 40f;
    public Animator animator;

    float horizontalMove = 0f;
    bool jump = false;
    void Update()
    {
       horizontalMove = Input.GetAxisRaw("Horizontal") * runSpeed;
       animator.SetFloat("PlayerSpeed", Mathf.Abs(horizontalMove));

       if (Input.GetButtonDown("Jump")){
           jump = true;
       }
    }

    void FixedUpdate() {
        controller.Move(horizontalMove * Time.fixedDeltaTime,jump);
        jump = false;
    }


}
