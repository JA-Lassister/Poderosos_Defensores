using UnityEngine;
using UnityEngine.InputSystem;


public class ShootMechanics : MonoBehaviour
{
    // Variável para guardar o prefab do projétil
    public GameObject laserPrefab;

    // Ponto de origem do tiro
    public Transform[] pontosTiro;


    // Velocidade do projétil
    public float velocidadeLaser = 30f;

    float tempo = 0f;
    public float cadencia = 0.3f; // segundos

    public AudioSource shootSound; //som do tiro

    void Update()
    {

        if(Mouse.current.leftButton.isPressed && tempo >= cadencia)
        {
            if (shootSound != null)
            {
                shootSound.Play();
            }
            // Chamar funçãio de disparo
            realizarDisparo();
            tempo = 0f;
        }
        tempo += Time.deltaTime;

    }

    void realizarDisparo()
    {

        Vector2 mousePos = Mouse.current.position.ReadValue();

        // Cria um raio que sai da câmera passando pelo cursor
        Ray ray = Camera.main.ScreenPointToRay(mousePos);

        foreach (Transform pontoTiro in pontosTiro)
        {

            Vector3 direcao = ray.direction;

            // Rotação principal alinhando o eixo do projétil (Z+) à direção do tiro
            Quaternion rotacaoBase = Quaternion.FromToRotation(Vector3.forward, direcao);

            // Offset visual caso o modelo não esteja apontando pra frente
            Quaternion offsetVisual = Quaternion.Euler(-90, 0, 0); // ajuste conforme seu modelo

            // Rotação final combinando base + offset
            Quaternion rotacaoFinal = rotacaoBase * offsetVisual;

            // Instancia o projétil com a rotação final
            GameObject tiro = Instantiate(laserPrefab, pontoTiro.position, rotacaoFinal);

            // Pega o componente Rigidbody do laser
            Rigidbody rb = tiro.GetComponent<Rigidbody>();

            // Usa o Rigidbody para adicionar uma força para frente
            rb.AddForce(ray.direction * velocidadeLaser, ForceMode.Impulse);
        }
    }
}