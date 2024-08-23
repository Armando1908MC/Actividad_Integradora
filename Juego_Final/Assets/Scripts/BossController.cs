using UnityEngine;
using TMPro;

public class BossController : MonoBehaviour
{
    public GameObject bulletPrefab;
    public Transform firePoint;
    public TextMeshProUGUI bulletCounterText;

    // Contador de balas activas
    private int activeBulletCount = 0;

    // Configuraciones generales
    public float fireRatePattern1 = 0.2f;
    public float fireRatePattern2 = 0.1f;
    public float fireRatePattern3 = 0.1f;  // Nuevo patrón (Espiral de cinco puntas)
    public float fireRatePattern4 = 0.2f;

    public float bulletSpeedPattern1 = 10f;
    public float bulletSpeedPattern2 = 10f;
    public float bulletSpeedPattern3 = 5f;  // Nuevo patrón
    public float bulletSpeedPattern4 = 20f;

    // Configuraciones del primer patrón (estrella)
    public int numeroDePuntas = 6;
    public float rotationSpeed = 10f;
    public float baseSpeed = 0.3f;
    public float speedFactor = 0.1f;

    // Configuraciones del segundo patrón (círculo estático)
    public int bulletsPerCirclePattern2 = 25;
    public float circleRadiusPattern2 = 3f;

    // Configuraciones del tercer patrón (Espiral de cinco puntas)
    public int numberOfBulletsPattern3 = 5;
    public float angleIncreasePattern3 = 10f;
    public float spawnRadiusPattern3 = 1f;

    // Configuraciones del cuarto patrón (círculo rotatorio)
    public int bulletsPerCirclePattern4 = 18;
    public float circleRadiusPattern4 = 3f;
    public float rotationIncrementPattern4 = 2f;

    private float nextFire = 0f;
    private float currentAngle = 0f;
    private int currentPattern = 0;
    private float patternChangeTime = 10f;
    private float nextPatternChange = 0f;

    private int totalPatterns = 4;
    private int patternCycleCount = 0;
    private bool canFire = false; // Control para iniciar el disparo después de 30 segundos

    void Start()
    {
        nextPatternChange = Time.time + patternChangeTime;
        UpdateBulletCounter(); // Inicializa el contador en la UI

        // Iniciar el disparo después de 30 segundos
        Invoke("EnableFiring", 30f);
    }

    void Update()
    {
        if (canFire && Time.time > nextFire)
        {
            switch (currentPattern)
            {
                case 0:
                    FirePattern1();
                    nextFire = Time.time + fireRatePattern1;
                    break;
                case 1:
                    FirePattern2();
                    nextFire = Time.time + fireRatePattern2;
                    break;
                case 2:
                    FirePattern3();
                    nextFire = Time.time + fireRatePattern3;
                    break;
                case 3:
                    FirePattern4();
                    nextFire = Time.time + fireRatePattern4;
                    break;
            }
        }

        if (canFire && Time.time > nextPatternChange)
        {
            currentPattern = (currentPattern + 1) % totalPatterns;
            nextPatternChange = Time.time + patternChangeTime + 0.5f; // Agrega 0.5 segundos entre patrones

            if (currentPattern == 0)
            {
                patternCycleCount++;
            }

            if (patternCycleCount >= 1)
            {
                EndCycle();
            }
        }
    }

    void EnableFiring()
    {
        canFire = true; // Habilitar el disparo
        currentPattern = 0; // Reiniciar el patrón al primero
        nextPatternChange = Time.time + patternChangeTime; // Reiniciar el tiempo para el cambio de patrón
    }

    void FirePattern1()
    {
        float angleStep = 360f / numeroDePuntas;

        for (int i = 0; i < numeroDePuntas; i++)
        {
            float angle = i * angleStep;
            float radianAngle = angle * Mathf.Deg2Rad;
            Vector3 bulletDirection = new Vector3(Mathf.Cos(radianAngle), 0, Mathf.Sin(radianAngle));

            float velExtra = Mathf.Abs(Mathf.Sin(numeroDePuntas * transform.eulerAngles.z * Mathf.Deg2Rad)) * speedFactor;
            Vector3 spawnPosition = firePoint.position + bulletDirection * (baseSpeed - velExtra);

            GameObject bullet = Instantiate(bulletPrefab, spawnPosition, Quaternion.identity);
            Rigidbody rb = bullet.GetComponent<Rigidbody>();
            rb.velocity = bulletDirection * bulletSpeedPattern1;

            IncrementActiveBulletCount();

            Destroy(bullet, 6f);
            Invoke("DecrementActiveBulletCount", 6f);
        }

        transform.Rotate(0f, rotationSpeed * Time.deltaTime, 0f);
    }

    void FirePattern2()
    {
        for (int i = 0; i < bulletsPerCirclePattern2; i++)
        {
            float angle = currentAngle + i * (360f / bulletsPerCirclePattern2);
            float radianAngle = angle * Mathf.Deg2Rad;
            Vector3 bulletDirection = new Vector3(Mathf.Cos(radianAngle), 0, Mathf.Sin(radianAngle));

            GameObject bullet = Instantiate(bulletPrefab, firePoint.position + bulletDirection * circleRadiusPattern2, Quaternion.identity);
            Rigidbody rb = bullet.GetComponent<Rigidbody>();
            rb.velocity = bulletDirection * bulletSpeedPattern2;

            IncrementActiveBulletCount();

            Destroy(bullet, 6f);
            Invoke("DecrementActiveBulletCount", 6f);
        }
    }

    void FirePattern3() // Nuevo patrón: Espiral de cinco puntas
    {
        float angleStep = 360f / numberOfBulletsPattern3;

        for (int i = 0; i < numberOfBulletsPattern3; i++)
        {
            float angle = currentAngle + i * angleStep;
            float radianAngle = angle * Mathf.Deg2Rad;
            Vector3 bulletDirection = new Vector3(Mathf.Cos(radianAngle), 0, Mathf.Sin(radianAngle));

            Vector3 spawnPosition = firePoint.position + bulletDirection * spawnRadiusPattern3;

            GameObject bullet = Instantiate(bulletPrefab, spawnPosition, Quaternion.identity);
            Rigidbody rb = bullet.GetComponent<Rigidbody>();
            rb.velocity = bulletDirection * bulletSpeedPattern3;

            IncrementActiveBulletCount();

            Destroy(bullet, 5f);
            Invoke("DecrementActiveBulletCount", 5f);
        }

        currentAngle += angleIncreasePattern3;
    }

    void FirePattern4()
    {
        for (int i = 0; i < bulletsPerCirclePattern4; i++)
        {
            float angle = currentAngle + i * (360f / bulletsPerCirclePattern4);
            float radianAngle = angle * Mathf.Deg2Rad;
            Vector3 bulletDirection = new Vector3(Mathf.Cos(radianAngle), 0, Mathf.Sin(radianAngle));

            GameObject bullet = Instantiate(bulletPrefab, firePoint.position + bulletDirection * circleRadiusPattern4, Quaternion.identity);
            Rigidbody rb = bullet.GetComponent<Rigidbody>();
            rb.velocity = bulletDirection * bulletSpeedPattern4;

            IncrementActiveBulletCount();

            Destroy(bullet, 6f);
            Invoke("DecrementActiveBulletCount", 6f);
        }

        currentAngle += rotationIncrementPattern4;
    }

    void EndCycle()
    {
        Destroy(gameObject);
    }

    public void IncrementActiveBulletCount()
    {
        activeBulletCount++;
        UpdateBulletCounter();
    }

    public void DecrementActiveBulletCount()
    {
        activeBulletCount--;
        UpdateBulletCounter();
    }

    void UpdateBulletCounter()
    {
        bulletCounterText.text = "Active Bullets: " + activeBulletCount.ToString();
    }
}
