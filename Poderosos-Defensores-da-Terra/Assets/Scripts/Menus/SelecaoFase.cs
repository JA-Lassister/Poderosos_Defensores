using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SelecaoFase : Menu
{
    public const int MENU_INICIAL = 0;
    public const int AGRADECIMENTOS = 11;
    public const int OPCOES = 12;
    public const int SELECIONAR_FASE = 13;

    public static void CarregarFase(int fase) => SceneManager.LoadScene(2 * fase - 1);

    public static IEnumerator CarregarFase(int fase, float delay) {
        yield return new WaitForSeconds(delay);
        CarregarFase(fase);
    }

    public static void CarregarOpcoes() => SceneManager.LoadScene(OPCOES);
    public static void CarregarSelecaoFase() => SceneManager.LoadScene(SELECIONAR_FASE);
    public static void CarregarMenuInicial() => SceneManager.LoadScene(MENU_INICIAL);
    public static void CarregarTelaFinal() => SceneManager.LoadScene(AGRADECIMENTOS);
}
