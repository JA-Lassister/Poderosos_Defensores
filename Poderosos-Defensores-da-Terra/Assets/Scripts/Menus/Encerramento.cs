using UnityEngine;
using UnityEngine.SceneManagement;

public class Encerramento : Menu
{
    public void RetornarAoMenu() => SceneManager.LoadScene(0);
    
}