using System;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Interludio : Menu
{
    public GameObject telaDescritiva;
    public GameObject telaInstrucoes;

    public void Start() {
        telaDescritiva.SetActive(true);
        telaInstrucoes.SetActive(false);
    }

    public void Avancar() {
        telaDescritiva.SetActive(false);
        telaInstrucoes.SetActive(true);
    }

    public void IniciarFase() => 
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
}
