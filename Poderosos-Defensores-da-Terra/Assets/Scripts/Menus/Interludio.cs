using UnityEngine;
using UnityEngine.SceneManagement;

public class Interludio : Menu
{
    public void IniciarFase() => 
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
}
