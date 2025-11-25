using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SelecaoFase : MonoBehaviour
{
    public const int SELECIONAR_FASE = 13;
    public const int OPCOES = 12;

    public static void CarregarFase(int fase) => SceneManager.LoadScene(2 * fase - 1);

    public static IEnumerator CarregarFase(int fase, float delay) {
        yield return new WaitForSeconds(delay);
        CarregarFase(fase);
    }

    public static void CarregarOpcoes() => SceneManager.LoadScene(OPCOES);
    public static void CarregarSelecaoFase() => SceneManager.LoadScene(SELECIONAR_FASE);
    public static void CarregarMenuInicial() => SceneManager.LoadScene(0);
}
