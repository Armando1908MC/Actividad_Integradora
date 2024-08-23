using UnityEngine;

public class Bullet : MonoBehaviour
{
    void OnCollisionEnter(Collision collision)
    {
        // Destruye la bala cuando impacta con cualquier objeto
        Destroy(gameObject);
    }

    void OnTriggerEnter(Collider other)
    {
        // Si la bala tiene 'Is Trigger' activado en el Collider, usa este método en su lugar
        Destroy(gameObject);
    }
}
