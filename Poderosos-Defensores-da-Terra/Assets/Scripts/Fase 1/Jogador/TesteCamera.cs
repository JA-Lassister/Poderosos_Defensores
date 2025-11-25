using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TesteCamera : MonoBehaviour
{
    public Transform player;
    public Transform pontoCamera;
    public float sensibilidade = 200f;
    public float limiteVertical = 60f;
    public float suavizacao = 2f;

    private float rotacaoY = 0f;  // Rotação horizontal (player + câmera)
    private float inclinacaoX = 0f; // Inclinação vertical da câmera
    
    public float y = 1.6f;
    public float z = 1.8f;
    public float x = 1.2f;

    void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
        y=1.6f;
        z=1.8f;
        x = 1.2f;
    }

    void Update()
    {
        float mouseX = Input.GetAxis("Mouse X") * sensibilidade * Time.deltaTime;
        float mouseY = Input.GetAxis("Mouse Y") * sensibilidade * Time.deltaTime;
        
        rotacaoY += mouseX;
        player.rotation = Quaternion.Euler(0f, rotacaoY, 0f);
        
        inclinacaoX -= mouseY;
        inclinacaoX = Mathf.Clamp(inclinacaoX, -limiteVertical, limiteVertical);
    }
    void LateUpdate()
    {
        
        // --- Calcula a posição da câmera atrás do player ---
        Vector3 largura = Quaternion.Euler(0f, rotacaoY, 0f) * Vector3.right;
        Vector3 direcao = Quaternion.Euler(0f, rotacaoY, 0f) * Vector3.back ;
        Vector3 posDesejada = player.position + Vector3.up * y + direcao * z + largura * x;

        // --- Move suavemente ---
        transform.position = Vector3.Lerp(transform.position, posDesejada, Time.deltaTime * suavizacao);

        // --- Faz a câmera olhar para o ponto alvo com inclinação ---
        if (pontoCamera != null)
        {
            // Inclina a câmera verticalmente sem mudar a rotação do player
            Quaternion inclinacao = Quaternion.Euler(inclinacaoX, rotacaoY, 0f);
            transform.rotation = Quaternion.Lerp(transform.rotation, inclinacao, Time.deltaTime * suavizacao);
        }
        if (pontoCamera != null)
        {
            Vector3 direcao2 = (pontoCamera.position - transform.position).normalized;
            Quaternion rotDesejada = Quaternion.LookRotation(direcao2);
            transform.rotation = Quaternion.Slerp(transform.rotation, rotDesejada, Time.deltaTime * suavizacao);
        }

    }
}
