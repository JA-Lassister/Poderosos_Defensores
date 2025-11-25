using UnityEngine.InputSystem;
using UnityEngine;

public class ControladorEstrada : MonoBehaviour
{
    public float velocidadeFrente;
    public float velocidadeLateral;
    public Transform chao1, chao2;

    private int _travaMov;
    private float _deslocamento;
    private InputAction _move;
    
    private void Start()
    {
        // Trava de movimentos desabilitada
        _travaMov = 0;
        
        _move = InputSystem.actions.FindAction("MoveHorizontal");
        
        // Calcula quanto um chão deve se deslocar para colocar-se "atrás" do outro
        _deslocamento = 2 * (chao2.position.z - chao1.position.z);
    }
    
    private void FixedUpdate()
    {
        float input = _move.ReadValue<float>();
        
        // Libera os movimentos somente se a trava não estiver em um sentido oposto
        // à movimentação solicitada
        float movLiberado = _travaMov * input >= 0.0f ? 1.0f : 0.0f;
        float movimentacaoLateral = input * velocidadeLateral * movLiberado;
        
        // Captura as teclas "A" e "D" para mover a estrada, causando o efeito de que o veículo
        // está se deslocando
        transform.Translate(movimentacaoLateral, 0.0f, 0.0f);
        
        // Desloca ambos os chãos, no sentido da esteira da estrada
        chao1.Translate(0.0f, 0.0f, - velocidadeFrente);
        chao2.Translate(0.0f, 0.0f, - velocidadeFrente);
    }

    // Chão passado como parâmetro se coloca para trás de seu chão complementar
    public void MoverChao(Transform chao) => chao.Translate(0.0f, 0.0f, _deslocamento);
    
    public void BloquearMovimentos(int x) => _travaMov = x; 

    public void LiberarMovimentos()  => _travaMov = 0;
}
