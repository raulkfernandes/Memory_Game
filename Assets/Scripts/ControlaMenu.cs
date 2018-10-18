using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ControlaMenu : MonoBehaviour {

    public void IniciarJogo()
    {
        SceneManager.LoadScene("Jogo da Memória");
    }
}
