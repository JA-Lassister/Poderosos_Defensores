using System.Collections;

// Implementação de uma lista, feita de modo que todas as operações,
// inserção, remoção (somente aleatória) e seleção tenham complexidade
// de tempo constante.

// IMPORTANTE: o tamanho da lista é determinado em seu construtor e não pode ser
// atualizado. Quaisquer tentativas de inserções além do limite da lista gerarão
// exceções genéricas que não são tratadas pela classe.
public class Lista<TQ> : IEnumerable
{
    private readonly TQ[] _elem;
    public int Fim { get; private set; }
    
    public Lista(int tam)
    {
        _elem = new TQ[tam];
        Fim = 0;
    }

    public void Inserir(TQ x) => _elem[Fim++] = x;

    public TQ RemoverAleatorio()
    {
        int i = UnityEngine.Random.Range(0, Fim);
        TQ x = _elem[i];
        _elem[i] = _elem[--Fim];
        return x;
    }

    public void Esvaziar() => Fim = 0;

    public TQ Selecionar(int i) => _elem[i];

    public IEnumerator GetEnumerator() => new ListaEnum<TQ>(this);
}

// Implementação da interface IEnumerator
public class ListaEnum<TQ> : IEnumerator
{
    private readonly Lista<TQ> _lista;
    private int _pos = -1;
    
    public ListaEnum(Lista<TQ> lista) => _lista = lista;
    
    object IEnumerator.Current => Current;
    private TQ Current => _lista.Selecionar(_pos);
    
    public void Reset() => _pos = -1;
    public bool MoveNext() => ++_pos < _lista.Fim;
}