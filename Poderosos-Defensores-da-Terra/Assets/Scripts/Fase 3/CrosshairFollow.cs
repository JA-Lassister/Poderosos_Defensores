using UnityEngine;
using UnityEngine.InputSystem; 


public class FollowCrosshairY : MonoBehaviour
{
    public RectTransform crosshair;
    public float smoothSpeed = 1.5f; 
    public float depthDistance = 15f;

    public float followDownY = -0.7f;  // Deslocamento para o aviao ficar abaixo da mira

    float angleRangeMinZ = +30.0f;
    float angleRangeMaxZ = -30.0f; 

    float angleRangeMinX = 20f;
    float angleRangeMaxX = -20f; 

    float naturalRotation;

    float sideLimit= 110.0f;

    void Start()
    {
        // Esconde o cursor do sistema ao iniciar o jogo.
        Cursor.visible = false;
        naturalRotation = transform.rotation.eulerAngles.x;

        crosshair.position = new Vector2(Screen.width/2, Screen.height/2);
    }

    void Update()
    {
        // === LÓGICA DO CONTROLE DA MIRA (CrosshairController) ===
        
        // Obter a posição bruta do mouse (em pixels da tela).
        Vector2 mousePos = Mouse.current.position.ReadValue();

        // Definir os limites da tela (valores de clamping).
        float screenW = Screen.width;
        float screenH = Screen.height;

        // Limitar a posição do mouse aos limites da tela.
        float mouseX_Limited = Mathf.Clamp(mousePos.x, sideLimit, screenW-sideLimit);
        float mouseY_Limited = Mathf.Clamp(mousePos.y, sideLimit, screenH);

        // Aplicar a posição limitada ao RectTransform da mira.
        crosshair.position = new Vector2(mouseX_Limited, mouseY_Limited);

        // === LÓGICA DE SEGUIMENTO DO OBJETO 3D === 

        // Obtém a posição da mira na tela
        Vector3 screenPos = crosshair.position;

        // Definir a profundidade do objeto 
        screenPos.z = depthDistance;

        // Converter a posição da tela para o mundo
        Vector3 targetPosition = Camera.main.ScreenToWorldPoint(screenPos);

        targetPosition.y += followDownY;

        // Mover o objeto de forma suave para a posição alvo
        transform.position = Vector3.Lerp(
            transform.position,     // Início
            targetPosition,     // Fim
            smoothSpeed * Time.deltaTime   // Fator
        );

        // === LÓGICA DE ROTAÇÃO DO OBJETO 3D ===

        // Posição atual da mira no eixo X
        float posX = crosshair.position.x;

        // Normalizar a posição X da tela (0 a screenW) para um valor entre 0 (Esquerda) e 1 (Direita)
        float tX = Mathf.InverseLerp(0.0f, screenW, posX);
        float tY = Mathf.InverseLerp(0.0f, screenH, crosshair.position.y);
        

        // Calcula o ângulo final variando entre o mínimo e o máximo,
        // de acordo com a posição horizontal normalizada da mira
        float finalAngleZ = Mathf.Lerp(angleRangeMinZ + naturalRotation, angleRangeMaxZ + naturalRotation, tX);
        
        float finalAngleX = Mathf.Lerp(angleRangeMinX+naturalRotation, angleRangeMaxX+naturalRotation, tY);

        transform.rotation = Quaternion.Euler(finalAngleX, 0.0f, finalAngleZ);

    }
}