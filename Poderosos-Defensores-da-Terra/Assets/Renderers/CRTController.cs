using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.Rendering.Universal;

public class CameraRendererSwitcher : MonoBehaviour
{
    [Header("Renderizadores configurados no URP Asset")]
    public int rendererIndex1 = 1; // CRT Render
    public int rendererIndex2 = 0; // Default Render

    [Header("Tecla para alternar")]
    public Key switchKey = Key.C; // Tecla para alterar

    private UniversalAdditionalCameraData cameraData;
    private bool usingFirstRenderer = true;

    void Start()
    {
        cameraData = GetComponent<UniversalAdditionalCameraData>();

        if (cameraData == null)
        {
            Debug.LogError("Nenhum UniversalAdditionalCameraData encontrado! Verifique se a câmera usa o URP.");
        }
        else
        {
            // Define o renderer inicial (index 1)
            cameraData.SetRenderer(rendererIndex1);
            usingFirstRenderer = true;
        }
    }

    void Update()
    {
        // Novo Input System
        if (Keyboard.current != null && Keyboard.current[switchKey].wasPressedThisFrame && cameraData != null)
        {
            if (usingFirstRenderer)
                cameraData.SetRenderer(rendererIndex2);
            else
                cameraData.SetRenderer(rendererIndex1);

            usingFirstRenderer = !usingFirstRenderer;

            Debug.Log($"Renderer da câmera alterado para: {(usingFirstRenderer ? rendererIndex1 : rendererIndex2)}");
        }
    }
}
