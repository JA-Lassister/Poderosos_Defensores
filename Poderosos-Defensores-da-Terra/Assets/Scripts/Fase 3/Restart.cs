using TMPro;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;

public class Restart : MonoBehaviour
{
    public TMP_Text textoReinicio;

    private InputAction _anyKey;

    void Start()
    {
        _anyKey = GetComponent<PlayerInput>().actions["Any"];
        textoReinicio.gameObject.SetActive(false);
    }

    public void ExibirMensagem()
    {
        textoReinicio.gameObject.SetActive(true);
        _anyKey.started += OnAny;
    }

    private void OnAny(InputAction.CallbackContext ctx)
    {
        //Reinicia a cena
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        
        // Para de escutar o teclado
        _anyKey.started -= OnAny;
    }
}
