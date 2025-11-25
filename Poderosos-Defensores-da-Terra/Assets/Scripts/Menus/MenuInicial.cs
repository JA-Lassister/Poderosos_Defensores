using UnityEngine;

public class MenuInicial : MonoBehaviour
{
    public void Awake() => Persistencia.CarregarPrefs();
    public void NovoJogo() => SelecaoFase.CarregarFase(1);
    public void SelecionarFase() => SelecaoFase.CarregarSelecaoFase();
    public void Opcoes() => SelecaoFase.CarregarOpcoes();
    public void Sair()
    {
        Application.Quit();
        
        #if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
        #endif
    }
}
