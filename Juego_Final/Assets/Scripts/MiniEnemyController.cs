using UnityEngine;

public class MiniEnemyController : MonoBehaviour
{
    public GameObject bulletPrefab;
    public Transform firePoint;
    public float initialBulletSpeed = 10f; // Velocidad inicial de las balas
    public float bulletSpeedIncrement = 5f; // Incremento de velocidad cada 5 segundos
    public Transform player; // Referencia al jugador
    public float fireRate = 1f; // Tiempo entre disparos

    private float nextFire = 0f;
    private bool canFire = true;
    private float currentBulletSpeed;

    // Referencia al controlador de balas (puede ser el BossController u otro objeto central)
    public BossController bulletCounterController;

    void Start()
    {
        currentBulletSpeed = initialBulletSpeed;

        // Aumentar la velocidad cada 5 segundos
        InvokeRepeating("IncreaseBulletSpeed", 5f, 5f);

        // Destruir el mini enemigo después de 30 segundos
        Invoke("DestroyMiniEnemy", 30f);
    }

    void Update()
    {
        if (canFire && Time.time > nextFire)
        {
            FireAtPlayer();
            nextFire = Time.time + fireRate;
        }
    }

    void FireAtPlayer()
    {
        if (player != null)
        {
            // Calcular la dirección hacia el jugador
            Vector3 directionToPlayer = (player.position - firePoint.position).normalized;

            // Instanciar la bala y darle velocidad en dirección al jugador
            GameObject bullet = Instantiate(bulletPrefab, firePoint.position, Quaternion.identity);
            Rigidbody rb = bullet.GetComponent<Rigidbody>();
            rb.velocity = directionToPlayer * currentBulletSpeed;

            // Incrementar el contador de balas activas
            if (bulletCounterController != null)
            {
                bulletCounterController.IncrementActiveBulletCount();
            }

            Destroy(bullet, 6f);
            Invoke("DecrementBulletCount", 6f); // Decrementar el contador después de destruir la bala
        }
    }

    void DecrementBulletCount()
    {
        if (bulletCounterController != null)
        {
            bulletCounterController.DecrementActiveBulletCount();
        }
    }

    void IncreaseBulletSpeed()
    {
        currentBulletSpeed += bulletSpeedIncrement;
    }

    void DestroyMiniEnemy()
    {
        Destroy(gameObject); // Destruir el mini enemigo después de 30 segundos
    }
}
