using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenuScript : MonoBehaviour
{
    // on créé un fonction que l'on va apeler a l'evenement onClick d'un boutton, on lie directement la fonction a l'evenement dans l'editeur
    public void StartNewGame()
    {
        SceneManager.LoadScene("SampleScene");
    }
}
