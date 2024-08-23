using UnityEngine;

public class SpiralFivePointPattern : MonoBehaviour
{
    public GameObject bulletPrefab;
    public Transform firePoint;
    public float fireRate = 0.1f; // Frecuencia de disparo
    public float bulletSpeed = 5f; // Velocidad de las balas
    public int numberOfBullets = 5; // Número de balas en cada disparo
    public float angleIncrease = 10f; // Incremento del ángulo después de cada disparo
    public float spawnRadius = 1f; // Radio desde el cual las balas se originan

    private float nextFire = 0f;
    private float currentAngle = 0f; // Ángulo actual de lanzamiento

    void Update()
    {
        if (Time.time > nextFire)
        {
            Fire();
            nextFire = Time.time + fireRate;
        }
    }

    void Fire()
    {
        float angleStep = 360f / numberOfBullets;

        for (int i = 0; i < numberOfBullets; i++)
        {
            float angle = currentAngle + i * angleStep;
            float radianAngle = angle * Mathf.Deg2Rad;
            Vector3 bulletDirection = new Vector3(Mathf.Cos(radianAngle), 0, Mathf.Sin(radianAngle));

            // Ajustar la posición inicial de la bala para que salga desde un radio alrededor del firePoint
            Vector3 spawnPosition = firePoint.position + bulletDirection * spawnRadius;

            GameObject bullet = Instantiate(bulletPrefab, spawnPosition, Quaternion.identity);
            Rigidbody rb = bullet.GetComponent<Rigidbody>();
            rb.velocity = bulletDirection * bulletSpeed;

            Destroy(bullet, 5f); // Destruir la bala después de 5 segundos
        }

        // Incrementar el ángulo base para el siguiente disparo en espiral
        currentAngle += angleIncrease;
    }
}
