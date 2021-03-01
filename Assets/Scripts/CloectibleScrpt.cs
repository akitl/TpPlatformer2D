using System.Collections;
using System.Collections.Generic;
using UnityEngine;


// script attaché au object a récupéré, le gameobject doit avoir un collider2D attaché a lui en mode trigger
public class CloectibleScrpt : MonoBehaviour
{

    // on detecte la colision
    private void OnTriggerEnter2D(Collider2D col)
    {
        // si la colision est bien avec un player
        if (col.tag == "Player")
        {
            // on indique qu'on as ramassé le colectible
            GameManager.Instance.p.blueDiamond = true;
            
            // on détruit le colectible 
            Destroy(gameObject);
        }
    }

}
