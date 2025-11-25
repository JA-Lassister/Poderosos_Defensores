using UnityEngine;

public class OrbitSimple : MonoBehaviour
{
    public float rotationSpeed = 20f;
    private Transform parent;
    private Vector3 offset;

    void Start()
    {
        parent = transform.parent;
        offset = transform.position - parent.position;
        offset.y = 0f; // ignora diferença em Y
    }

    void Update()
    {
        // Gira o offset no plano XZ
        offset = Quaternion.Euler(0, rotationSpeed * Time.deltaTime, 0) * offset;

        // Atualiza posição (mantendo Y atual)
        transform.position = new Vector3(parent.position.x + offset.x, transform.position.y, parent.position.z + offset.z);

        // Calcula direção horizontal para o centro e rotaciona localmente
        Vector3 dir = parent.position - transform.position;
        dir.y = 0f;
        transform.rotation = Quaternion.Euler(25, Quaternion.LookRotation(dir).eulerAngles.y, 0);
    }
}