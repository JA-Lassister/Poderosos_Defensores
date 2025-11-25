using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering.Universal;

public class ControladorCRT: MonoBehaviour
{
    [SerializeField] 
    public Camera camera;

    private UniversalAdditionalCameraData _additionalCameraData;

    private void Start() {
        _additionalCameraData = camera.GetComponent<UniversalAdditionalCameraData>();
        
        Atualizar(Persistencia.CRT());
    }

    public void Atualizar(int crt) =>
        _additionalCameraData.SetRenderer(crt);
}