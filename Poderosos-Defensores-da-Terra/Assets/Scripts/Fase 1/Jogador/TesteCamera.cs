using UnityEngine;

public class TesteCamera : MonoBehaviour
{
    [Header("Alvo")]
    public Transform player;
    public Transform pontoPivoCamera; 

    [Header("Configurações de Sensibilidade")]
    public float sensibilidadeHorizontal = 2.0f; // Eixo X (Olhar para os lados)
    public float sensibilidadeVertical = 2.0f;   // Eixo Y (Olhar para cima/baixo)

    [Header("Limites")]
    public float limiteBaixo = -60f;
    public float limiteAlto = 60f;

    [Header("Offset")]
    public Vector3 offset = new Vector3(0.5f, 0, -2.5f); 

    // Acumuladores de rotação
    private float rotX = 0f; 
    private float rotY = 0f; 

    void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;

        if (player != null)
        {
            rotY = player.eulerAngles.y;
        }
    }

    void LateUpdate()
    {
        if (player == null || pontoPivoCamera == null) return;

        // --- AQUI ESTÁ A MUDANÇA ---
        // Usamos uma sensibilidade para cada eixo
        float mouseX = Input.GetAxisRaw("Mouse X") * sensibilidadeHorizontal;
        float mouseY = Input.GetAxisRaw("Mouse Y") * sensibilidadeVertical;

        // Acumular os valores
        rotY += mouseX;
        rotX -= mouseY;
        rotX = Mathf.Clamp(rotX, limiteBaixo, limiteAlto);

        // Criar a Rotação Final
        Quaternion rotacaoCamera = Quaternion.Euler(rotX, rotY, 0f);
        Quaternion rotacaoDoCorpo = Quaternion.Euler(0f, rotY, 0f);

        // Aplicar ao Player (Gira o corpo horizontalmente)
        player.rotation = rotacaoDoCorpo;

        // Aplicar à Câmera (Gira a visão total)
        transform.rotation = rotacaoCamera;

        // Calcular Posição
        Vector3 posicaoDesejada = pontoPivoCamera.position + (rotacaoCamera * offset);
        transform.position = posicaoDesejada;
    }
}