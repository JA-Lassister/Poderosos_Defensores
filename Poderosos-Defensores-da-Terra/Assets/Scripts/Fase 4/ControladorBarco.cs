using UnityEngine;
using UnityEngine.InputSystem;

public class ControladorBarco : Controlador
{
    public HealthBar_Fase4 vida;
    
    public float rotationSpeed;
    public float moveSpeed;
    public float accelerationRate;
    public float maxAcceleration;
    
    public float danoJogador = 20f;

    public float tempoInvulnerabilidade = 1f;
    public Restart textoReinicio;

    private InputAction _move;
    private float _acc = 0f;
    private bool _invulneravel;

    void Start() =>
        _move = InputSystem.actions.FindAction("Move");
    
    void FixedUpdate()
    {
        Vector2 inputVector = _move.ReadValue<Vector2>();
        Rotate(inputVector.x);
        Move(inputVector.y);
    }

    void Rotate(float input)
    {
        if (input == 0) return;
        transform.Rotate(input * rotationSpeed * Vector3.up * Time.deltaTime, Space.Self);
    }

    void Move(float input)
    {
        if (input > 0) _acc = Mathf.Clamp(_acc + accelerationRate, 0f, maxAcceleration);
        else if (input < 0) _acc /= 1.2f;
        else if(_acc > 0) _acc -= accelerationRate;

        float move = Mathf.Clamp(input, 0, 1);
        Vector3 vetMov = transform.position + moveSpeed * (move + _acc) * Time.deltaTime * transform.forward;
        transform.position = ParedesInvisiveis.PosicaoAjustada(vetMov);
    }

    public void EfetivarDano()
    {
        if (_invulneravel) return;
        
        vida.DecreaseHealthPercent(danoJogador / 100);
        
        _invulneravel = true;
        Invoke(nameof(HabilitarDano), tempoInvulnerabilidade);
    }
    
    public void HabilitarDano() => _invulneravel = false;

    public override void Morrer()
    {
        Invoke(nameof(Desaparecer), 1f);
        textoReinicio.ExibirMensagem();
    }

    public void Desaparecer() => transform.Find("Frigate").gameObject.SetActive(false);
}
