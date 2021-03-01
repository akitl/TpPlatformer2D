using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    // partie singleton
    #region Singleton
    public static GameManager Instance;
    void Awake()
    {
        if (Instance != null)
        {
            Debug.LogWarning("More than one instance of GameManager found!");
            return;
        }
        Init();
        Instance = this;

    }
    #endregion

    // propriété player du gamemanger, reference du player accecible partout
    public Player p { get; private set; }

    // methode lancer a la création du singleton
    void Init()
    {
        // aplle du constructeur de player
        p = new Player();
    }


}
