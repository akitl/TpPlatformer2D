using System.Collections;
using System.Collections.Generic;
using UnityEngine;


// class player, elle sert a decrire les statistique du joueur, cette class fait partie du game systeme
public class Player
{
    // constructeur
    public Player()
    {
        blueDiamond = false;
        life = 3;
    }

    // propriété blueDiamond pour indiqué si on as récupéré le colectible ou non
    public bool blueDiamond { get; set; }

    // porpiété life pour indiqué le nombre de coeur qu'il reste
    public int life { get; set; }

}
