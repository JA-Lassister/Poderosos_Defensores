using UnityEngine;

public class GerenciadorObstaculos : MonoBehaviour
{
    public Renderer superficie;
    
    // Índice do vetor _obst que contém os obstáculos atualmente ativos no chão
    private const int ATIVOS = 0;
    
    // Quantidade de obstáculos de largura 1 disponíveis
    private const int QTD_OBSTACULOS_1 = 4;
    
    // Quantidade de obstáculos de largura 2 disponíveis 
    private const int QTD_OBSTACULOS_2 = 4;
    
    // Máximo de obstáculos que podem estar ativos simultaneamente em um chão
    private const int MAX_OBSTACULOS = 2;
    
    private Lista<Obstaculo>[] _obst = new Lista<Obstaculo>[4];
    private float[] _trilhas = new float[4];
    private float _meiaTrilha;

    private void Start()
    {
        // Alocação dos vetores que armazenam os obstáculos indisponíveis (ATIVOS)
        // e os disponíveis (índices 1 e 2).
        _obst[0] = new Lista<Obstaculo>(MAX_OBSTACULOS);
        _obst[1] = new Lista<Obstaculo>(QTD_OBSTACULOS_1);
        _obst[2] = new Lista<Obstaculo>(QTD_OBSTACULOS_2);
        
        foreach (Transform t in transform) {
            var o = t.GetComponent<Obstaculo>();
            
            // Insere todos os obstáculos de acordo com sua largura
            _obst[o.largura].Inserir(o);
            
            // Inicia o obstáculo como inativo
            o.Desabilitar();
        }
        
        // Atualiza atributos privados com a posição do "centro" de cada trilha da estrada
        CalcularExtensaoTrilhas();
    }
    
    private void CalcularExtensaoTrilhas()
    {
        _meiaTrilha = superficie.bounds.size.x / 8.0f;
        
        _trilhas[0] = 3 * _meiaTrilha;
        _trilhas[1] = _meiaTrilha;
        _trilhas[2] = -_trilhas[1];
        _trilhas[3] = -_trilhas[0];
    }
    
    // Atualiza os obstáculos atualmente ativos em cada posição do chão
    // Retorna uma máscara informando quais trilhas estão ocupadas
    public bool[] AtualizarObstaculos()
    {
        // Restaura as listas de obstáculos ao estado inicial
        RestaurarObstaculos();
        
        // Determina aleatoriamente se será gerado ou não obstáculo de largura 2
        return Random.Range(0,2) == 0 ? GerarObstaculosTam1() : GerarObstaculoTam2();
    }
    
    private void RestaurarObstaculos()
    {
        foreach (Obstaculo o in _obst[ATIVOS]) {
            // Faz obstáculo ativo desaparecer
            o.Desabilitar();
            
            // Insere-o na lista de obstáculos inativos de acordo com sua largura
            _obst[o.largura].Inserir(o);
        }

        // Esvazia a lista de obstáculos ativos
        _obst[ATIVOS].Esvaziar();
    }

    // Gera dois obstáculos de largura 1 em duas trilhas
    private bool[] GerarObstaculosTam1()
    {
        // Todas as posições da máscara são iniciadas com "falso"
        var temObstaculo = new bool[4];

        // A trilha para comportar o primeiro objeto é selecionada aleatoriamente 
        int trilhaOb1 = Random.Range(0, 4);
        
        // A trilha para comportar o segundo objeto é obtida por um deslocamento circular
        // aleatório de 3 posições a partir da trilha gerada anteriormente
        int trilhaOb2 = (trilhaOb1 + Random.Range(1, 4)) % 4;
        
        // Exibe dois obstáculos de largura 1 selecionados aleatoriamente
        // nas trilhas determinadas anteriormente 
        ExibirObstaculo(_obst[1].RemoverAleatorio(), _trilhas[trilhaOb1]);
        ExibirObstaculo(_obst[1].RemoverAleatorio(), _trilhas[trilhaOb2]);

        // Troca o valor das posições da máscara referentes a trilhas ocupadas para "verdadeiro"
        temObstaculo[trilhaOb1] = temObstaculo[trilhaOb2] = true;
        
        return temObstaculo;
    }

    // Gera um obstáculo de largura 2 e TALVEZ mais um obstáculo de largura 1
    private bool[] GerarObstaculoTam2()
    {
        // Todas as posições da máscara são iniciadas com "falso"
        var temObstaculo = new bool[4];
        
        // Escolhe aleatoriamente uma das 3 faixas entre trilhas da estrada
        int trilhaOb2 = Random.Range(0, 3);
        
        // Exibe obstáculo aleatório na faixa entre-trilhas determinada
        ExibirObstaculo(_obst[2].RemoverAleatorio(), _trilhas[trilhaOb2] - _meiaTrilha);
        
        // Marca as duas trilhas que compartilham a faixa como ocupadas
        temObstaculo[trilhaOb2] = temObstaculo[trilhaOb2 + 1] = true;
        
        // Determina aleatoriamente se gerará um obstáculo de largura 1
        if (Random.Range(0, 2) == 0) {
            
            // Seleciona uma trilha por um deslocamento circular aleatório entre 2 e 3
            // da trilha selecionada anteriormente
            int trilhaOb1 = (trilhaOb2 + Random.Range(2, 4)) % 4;
            
            // Marca trilha como ocupada
            temObstaculo[trilhaOb1] = true;
            
            // Exibe obstáculo aleatório de largura 1 
            ExibirObstaculo(_obst[1].RemoverAleatorio(), _trilhas[trilhaOb1]);
        }

        return temObstaculo;
    }
    
    // Posiciona o obstáculo "o" na posição "coordX" da estrada
    private void ExibirObstaculo(Obstaculo o, float coordX)
    {
        // Coloca-o na posição correta
        o.Posicionar(coordX);
        
        // Exibe-o
        o.Habilitar();
        
        // Insere-o na lista de obstáculos ativos
        _obst[ATIVOS].Inserir(o);
    }
    
}
