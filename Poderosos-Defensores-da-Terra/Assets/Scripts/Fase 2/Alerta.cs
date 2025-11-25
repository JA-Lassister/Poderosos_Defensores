using UnityEngine;

public class Alerta : MonoBehaviour
{
    public float tempoAparicao;
    private void OnEnable() => Invoke(nameof(Desaparecer), tempoAparicao);
    private void Desaparecer() => gameObject.SetActive(false);
}
