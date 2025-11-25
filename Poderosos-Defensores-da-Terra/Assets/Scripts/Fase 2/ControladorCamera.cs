using UnityEngine;

public class ControladorCamera : MonoBehaviour
{
    private bool _encerramento;

    public Transform foco;
    public Transform focoFinal;
    public float atrasoInicial;

    public float tempoEntreFocos = 2f;
    public float velocidadeZoom;
    public float quantidadeZoom = 30f;

    public Camera camera;

    private Transform _foco;

    public void Encerrar(Transform focoInicial)
    {
        // Coloca câmera no estado de encerramento
        _encerramento = true;
        
        // Determina o tempo e ordem de cada foco da câmera para essa "cutscene" de encerramento
        _foco = focoInicial;
        Invoke(nameof(TrocarFoco1), atrasoInicial);
        Invoke(nameof(TrocarFoco2), atrasoInicial + tempoEntreFocos);
    }
    
    private void FixedUpdate()
    {
        // Ignorar se não estiver no estado de encerramento
        if (!_encerramento) return;
        
        // Dá zoom
        camera.fieldOfView = Mathf.Clamp(camera.fieldOfView - velocidadeZoom, quantidadeZoom, 60f);
        
        // Segue seu foco atual
        Quaternion alvo = Quaternion.LookRotation(_foco.position - transform.position);
        transform.rotation = Quaternion.Slerp(transform.rotation, alvo, velocidadeZoom * Time.deltaTime);
    }

    private void TrocarFoco1() => _foco = foco;
    private void TrocarFoco2() => _foco = focoFinal;
}
