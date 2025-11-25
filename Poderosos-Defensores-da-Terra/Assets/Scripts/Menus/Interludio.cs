using UnityEngine;
using UnityEngine.SceneManagement;

public class Interludio : MonoBehaviour
{
    public void IniciarFase() => 
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
}
