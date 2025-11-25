using UnityEngine;
using UnityEngine.InputSystem;


public class EnemyMover : MonoBehaviour
{

    private Vector2[] coordenadas = new Vector2[10];
    public float smoothSpeed = 5f;

    float screenW;
    float screenH;

    float tempo = 0f;
    public float intervalo = 5f; // segundos

    int posicaoEscolhida;
    public float depthDistance;

    void Start()
    {
        atualizacaoTela();

        posicaoEscolhida = Random.Range(0, coordenadas.Length - 1);

        depthDistance = Camera.main.WorldToScreenPoint(transform.position).z;

    }

    void atualizacaoTela()
    {
        // Inicializa o tamanho da tela
        screenW = Screen.width;
        screenH = Screen.height;

        // Iniciar o vetor de posições para a movimentação do inimigo
        coordenadas[0] = new Vector2(screenW * 0.25f, screenH * 0.25f);
        coordenadas[1] = new Vector2(screenW * 0.25f, screenH * 0.50f);
        coordenadas[2] = new Vector2(screenW * 0.25f, screenH * 0.75f);
        coordenadas[3] = new Vector2(screenW * 0.50f, screenH * 0.25f);
        coordenadas[4] = new Vector2(screenW * 0.50f, screenH * 0.50f);
        coordenadas[5] = new Vector2(screenW * 0.50f, screenH * 0.75f);
        coordenadas[6] = new Vector2(screenW * 0.75f, screenH * 0.25f);
        coordenadas[7] = new Vector2(screenW * 0.75f, screenH * 0.50f);
        coordenadas[8] = new Vector2(screenW * 0.75f, screenH * 0.75f);

        coordenadas[9] = new Vector2(screenW * 0.50f, screenH * 0f);
    }

    public void movimentacaoNormal()
    {
        atualizacaoTela();

        // Mudar a posição a cada intervalo de tempo
        if (tempo >= intervalo)
        {
            int posicaoAnterior = posicaoEscolhida;

            do
            {
                posicaoEscolhida = Random.Range(0, coordenadas.Length-1);
            } while (posicaoEscolhida == posicaoAnterior);

            tempo = 0f; // reseta o contador
        }

        realizarMovimento();

        tempo += Time.deltaTime;
    }

    public void inimigoFugiu()
    {
        atualizacaoTela();
        posicaoEscolhida = 4;
        depthDistance += 2;

        realizarMovimento();
        
    }

    public void inimigoMorreu()
    {
        atualizacaoTela();
        posicaoEscolhida = 9;
        smoothSpeed = 0.8f;

        realizarMovimento();
    }

    void realizarMovimento()
    {
        // Obter a posição da tela
        Vector3 pontoTela = new Vector3(
            coordenadas[posicaoEscolhida].x,
            coordenadas[posicaoEscolhida].y,
            Camera.main.nearClipPlane + 1f
        );

        // Transformar a posição da tela para o mundo

        Vector3 posicaoMundo = Camera.main.ScreenToWorldPoint(pontoTela);
        posicaoMundo = Camera.main.transform.position + (posicaoMundo - Camera.main.transform.position).normalized * depthDistance;

        transform.position = Vector3.Lerp(
            transform.position,     // Início
            posicaoMundo,     // Fim
            smoothSpeed * Time.deltaTime   // Fator
        );
    }


}


