using UnityEngine;

public class Bullet : MonoBehaviour
{
    public float bulletLifetime = 1f;
    public float speed = 20f;
    public Rigidbody rb;

    void Start()
    {
        rb.AddForce(speed * transform.forward);
        Invoke(nameof(SelfDestroy), bulletLifetime);
    }

    private void SelfDestroy() => Destroy(gameObject);
}
