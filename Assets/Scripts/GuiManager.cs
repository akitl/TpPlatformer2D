using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

// script servant a géré les diferente UI du jeu
public class GuiManager : MonoBehaviour
{
    // Composant image pour l'interface du colectible
    public Image img;

    // sprite alternatif pour le colectible
    public Sprite altSprite;

    // sprite de base pour le colectible
    Sprite baseSprite;


    // panel contenant les coeur de vie
    public GameObject lifePanel;

    // sprite pour de coeur vide
    public Sprite emptyHeart;

    // sprite pour de coeur plein
    public Sprite fullHeart;

    // Start is called before the first frame update
    void Start()
    {
        // on récupére l'image de base du colectible et on la sauvegarde
        baseSprite = img.sprite;
    }

    // Update is called once per frame
    void Update()
    {
        // si on posséde le blue diamond ( dans le player du game manager)
        if (GameManager.Instance.p.blueDiamond)
        {
            // on change le sprite
            img.sprite = altSprite;
        }
        else
        {
            // si non on met le sprite par defaut
            img.sprite = baseSprite;
        }

        // on récupére la liste des image qui compose le lifePanel
        // cela retourne un tableau de 4 car par defaut il y as une image associer au panel donc le prmier coeur est a [1] et non [0]
        var hearts = lifePanel.GetComponentsInChildren<Image>();
        switch (GameManager.Instance.p.life)
        {
            // si on as plus qu'un point de vie alors on met les image a empty heart hormis le premier que l'on indique a full
            case 1:
                hearts[1].sprite = fullHeart;
                hearts[2].sprite = emptyHeart;
                hearts[3].sprite = emptyHeart;
                break;
            // les deux premier a full et le dernier a empty
            case 2:
                hearts[1].sprite = fullHeart;
                hearts[2].sprite = fullHeart;
                hearts[3].sprite = emptyHeart;
                break;
            //  les 3 coeur a full
            case 3:
                hearts[1].sprite = fullHeart;
                hearts[2].sprite = fullHeart;
                hearts[3].sprite = fullHeart;
                break;
            // par defaut tout a empty
            default:
                hearts[1].sprite = emptyHeart;
                hearts[2].sprite = emptyHeart;
                hearts[3].sprite = emptyHeart;
                break;
        }
    }
}
