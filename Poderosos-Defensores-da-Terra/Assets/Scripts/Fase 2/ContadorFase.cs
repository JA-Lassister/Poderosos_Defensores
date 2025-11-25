using System;
using TMPro;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.Controls;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class ContadorFase : MonoBehaviour
{
    public ControladorCarro veiculo;
    public ControladorCamera camera;
    public Image bateria;
    public Image carga0, carga1, carga2, carga3;
    public TMP_Text mensagem;
    
    public float tempoCargaInicial;
    public float diferencialTempoCarga;
    public float tempoReinicio = 0.3f;
    
    private int _proxCarga;
    private const int QTD_CARGAS = 4;
    private Image[] _cargas = new Image[QTD_CARGAS];
    private InputAction _anyKey;
    
    void Start()
    {
        // Recuperar a InputAction customizada "Any"
        _anyKey = GetComponent<PlayerInput>().actions["Any"];
        mensagem.gameObject.SetActive(false);
        
        // Código ruim inserido para contornar um bug no Unity Editor
        _cargas[0] = carga0; carga0.gameObject.SetActive(false);
        _cargas[1] = carga1; carga1.gameObject.SetActive(false);
        _cargas[2] = carga2; carga2.gameObject.SetActive(false);
        _cargas[3] = carga3; carga3.gameObject.SetActive(false);
        
        // Inicia um timer para carregar a primeira barra da bateria
        Invoke(nameof(Carregar),tempoCargaInicial);
    }

    private void Carregar()
    {
        // Aumenta uma barra de carga da bateria no HUD
        _cargas[_proxCarga++].gameObject.SetActive(true);

        // Se a bateria não estiver cheia, inicia um timer para encher a próxima barra
        if (_proxCarga < QTD_CARGAS) {
            tempoCargaInicial += diferencialTempoCarga;
            Invoke(nameof(Carregar), tempoCargaInicial);
        }
        // Se a bateria estiver cheia, encerra a fase por vitória
        else EncerrarVitoria();
    }

    private void OnAny(InputAction.CallbackContext ctx)
    {
        // Para de escutar o teclado
        _anyKey.started -= OnAny;
        
        //Reinicia a cena
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
    
    // Trata o encerramento da fase por derrota
    public void EncerrarDerrota()
    {
        // Impede que a bateria continue a carregar
        CancelInvoke(nameof(Carregar));
        
        // Exibe mensagem de morte
        mensagem.gameObject.SetActive(true);
        
        // Começa a escutar o pressionamento de qualquer tecla do teclado após um dado tempo
        Invoke(nameof(EscutarTeclado),tempoReinicio);
    }

    // Trata o encerramento da fase por vitória
    private void EncerrarVitoria()
    {
        // Inicia "cutscene" de encerramento
        camera.Encerrar(veiculo.transform);
        OcultarBateria();
        
        // Veículo atira no inimigo
        Invoke(nameof(IniciarDisparo), camera.atrasoInicial);
        
        // Vai para a próxima fase em 5s
        StartCoroutine(SelecaoFase.CarregarFase(3,5f));
    }

    private void EscutarTeclado() => _anyKey.started += OnAny;

    private void IniciarDisparo() => veiculo.Atirar();

    private void OcultarBateria()
    {
        bateria.gameObject.SetActive(false);
        foreach(var carga in _cargas) carga.gameObject.SetActive(false); 
    }
}
