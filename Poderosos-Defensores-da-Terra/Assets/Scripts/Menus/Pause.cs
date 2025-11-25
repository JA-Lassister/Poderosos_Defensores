using UnityEngine;
using UnityEngine.InputSystem;

public class Pause : MonoBehaviour
{
    private InputAction _pause;
    private static Pause singleton;
    public GameObject telaPause;

    private bool _pausado;
    
    private void Awake()
    {
        if (DestruirReplica()) return;
        
        telaPause.SetActive(false);
        _pause = InputSystem.actions.FindAction("Pause");
        _pause.started += Pausar;
    }

    private void Pausar(InputAction.CallbackContext ctx) {
        
        if (_pausado) {
            Time.timeScale = 1f;
            telaPause.SetActive(false);
        }
        else {
            Time.timeScale = 0f;
            telaPause.SetActive(true);
        }

        _pausado = !_pausado;
    }

    private bool DestruirReplica()
    {
        if (singleton != null && singleton != this) {
            Destroy(gameObject);
            return true;
        }

        singleton = this;
        DontDestroyOnLoad(this);
        
        return false;
    }

    public void VoltarAoMenuInicial()
    {
        Time.timeScale = 1f;
        _pause.started -= Pausar;
        singleton = null;
        Destroy(gameObject);
        SelecaoFase.CarregarMenuInicial();
    }
}