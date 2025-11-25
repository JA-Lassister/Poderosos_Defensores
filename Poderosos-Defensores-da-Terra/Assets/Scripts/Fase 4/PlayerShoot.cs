using UnityEngine;

public class PlayerShoot : MonoBehaviour
{
    public Transform firePoint; //ponto de onde sai o tiro
    public GameObject bulletPrefab; //prefab da bala
    public float fireRate = 0.5f; //intervalo entre tiros
    private float nextTimeToFire = 0f;

    public AudioSource shootSound; //som do tiro

    void Update()
    {
        if (!Input.GetButton("Fire1") || !(Time.time >= nextTimeToFire)) return; 
        
        nextTimeToFire = Time.time + fireRate; //cooldown
        shootSound.Play();
        Instantiate(bulletPrefab, firePoint.position, firePoint.rotation);
        
    }
}