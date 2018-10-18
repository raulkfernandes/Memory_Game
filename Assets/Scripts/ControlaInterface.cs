using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class ControlaInterface : MonoBehaviour {

    [SerializeField]
    private Text cronometro;
    [SerializeField]
    private Text pontuacao;
    [SerializeField]
    private GameObject painelFimDoJogo;
    [SerializeField]
    private Text tempoFinal;
    [SerializeField]
    private Text pontosFinal;
    private ControlaPontuacao controlaPontuacao;
    private PostPontuacao postPontuacao;
    private string temporizador = "";

    private void Start()
    {
        controlaPontuacao = FindObjectOfType<ControlaPontuacao>() as ControlaPontuacao;
        postPontuacao = FindObjectOfType<PostPontuacao>() as PostPontuacao;
        Time.timeScale = 1;

    }

    private void Update()
    {
        if (controlaPontuacao.numAcertos == 10)
        {
            StartCoroutine(EncerraJogo());
        }
    }

    IEnumerator EncerraJogo ()
    {
        yield return new WaitForSeconds(2);
        Time.timeScale = 0;
        painelFimDoJogo.SetActive(true);
        tempoFinal.text = this.cronometro.text;
        pontosFinal.text = this.pontuacao.text;
        postPontuacao.AdicionarPontucaoFinal(this.temporizador, controlaPontuacao.pontos);
    }

    public void AtualizaCronometro(float tempo)
    {
        int minutos = Mathf.FloorToInt(tempo / 60F);
        int segundos = Mathf.FloorToInt(tempo - minutos * 60);
        temporizador = string.Format("{0:0}:{1:00}", minutos, segundos);

        this.cronometro.text = "Tempo: " + temporizador + " s";
    }

    public void AtualizaPontuacao(int pontos)
    {
        this.pontuacao.text = "Pontos: " + pontos;
    }

    public void VoltarProMenu ()
    {
        SceneManager.LoadScene("Menu do Jogo");
    }

    public void ReiniciarJogo ()
    {
        SceneManager.LoadScene("Jogo da Memória");
        painelFimDoJogo.SetActive(false);
    }
}
