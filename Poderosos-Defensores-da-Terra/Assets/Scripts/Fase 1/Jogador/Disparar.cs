using UnityEngine;
using UnityEngine.UI;

public class Disparar : MonoBehaviour
{
    [Header("Referências")]
    public Animator animator;
    public Transform pontoDeSaida;
    public Camera cameraJogador;
    public GameObject impactoPrefab;
    public AudioSource somDisparo;
    public Image miraUI; // Efeito visual de impacto
    public GameObject projetilPrefab;
    public float velocidadeProjetil = 60f;

    [Header("Configurações de Disparo")]
    public float dano = 20f;
    public float alcance = 100f;
    public float cadencia = 0.3f;
    private float proximoDisparo = 0f;

    [Header("Configurações de Zoom")]
    public float fovNormal = 60f;
    public float fovMira = 40f;
    public float velocidadeZoom = 10f;

    private bool estaMirando = false;
    public float tamanho = 10f;     // tamanho do quadrado
    public Color cor = Color.white; // cor da mira

    void Start()
    {
        if (cameraJogador == null)
            cameraJogador = Camera.main;

        if (miraUI != null)
            miraUI.enabled = false;
    }

    void Update()
    {
        if (!estaMirando) return; // só permite mirar/disparar se o modo estiver ativo

        ControlarZoom();

        if (Input.GetButton("Fire1") && Time.time >= proximoDisparo)
        {
            ControlarDisparo();
        }
    }

    void ControlarZoom()
    {
        // Segura botão direito -> zoom suave
        if (Input.GetMouseButton(1))
        {
            cameraJogador.fieldOfView = Mathf.Lerp(cameraJogador.fieldOfView, fovMira, Time.deltaTime * velocidadeZoom);
            miraUI.enabled = true;
        }
        else
        {
            cameraJogador.fieldOfView = Mathf.Lerp(cameraJogador.fieldOfView, fovNormal, Time.deltaTime * velocidadeZoom);
            
            miraUI.enabled = false;
        }
    }

    public void AtualizarZoomInstantaneo(bool ativarMira)
    {
        // Função chamada pelo Combate.cs ao alternar de modo
        estaMirando = ativarMira;

        if (cameraJogador == null) return;

        if (ativarMira)
        {
            cameraJogador.fieldOfView = fovMira;
            if (miraUI != null)
                miraUI.enabled = true;
        }
        else
        {
            cameraJogador.fieldOfView = fovNormal;
            if (miraUI != null)
                miraUI.enabled = false;
        }
    }
    void Atirar()
    {
        // Aqui fica seu código de tiro...
        if (somDisparo != null)
            somDisparo.Play();

        Ray ray = cameraJogador.ViewportPointToRay(new Vector3(0.5f, 0.5f, 0));
        RaycastHit hit;
        Vector3 pontoAlvo;

        if (Physics.Raycast(ray, out hit, alcance))
        {
            pontoAlvo = hit.point;
        }
        else
        {
            pontoAlvo = ray.origin + ray.direction * alcance;
        }

        Vector3 direcaoDisparo = (pontoAlvo - pontoDeSaida.position).normalized;

        if (projetilPrefab != null)
        {
            GameObject proj = Instantiate(projetilPrefab, pontoDeSaida.position, Quaternion.LookRotation(direcaoDisparo));
            proj.AddComponent<ProjetilVisual>().Iniciar(direcaoDisparo, velocidadeProjetil);
        }

        if (hit.collider != null)
        {
            VidaInimigo inimigo = hit.collider.GetComponent<VidaInimigo>();
            if (inimigo != null)
                inimigo.ReceberDano(dano);

            if (impactoPrefab != null)
            {
                Quaternion rot = Quaternion.LookRotation(hit.normal);
                GameObject impacto = Instantiate(impactoPrefab, hit.point, rot);
                Destroy(impacto, 2f);
            }
        }
    }
    void ControlarDisparo()
    {
        if (Input.GetMouseButton(0) && Time.time >= proximoDisparo)
        {
            Atirar();
            proximoDisparo = Time.time + cadencia;
        }
    }
}