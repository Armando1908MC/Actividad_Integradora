using UnityEngine;
using UnityEngine.SceneManagement;  // Necesario para cargar escenas

public class PlayerHealth : MonoBehaviour
{
    void OnCollisionEnter(Collision collision)
    {
        // Verifica si la colisi�n es con una bala del enemigo
        if (collision.gameObject.CompareTag("Bullet"))
        {
            EndGame();
        }
    }

    void OnTriggerEnter(Collider other)
    {
        // Si est�s usando un Collider con "Is Trigger" en las balas
        if (other.CompareTag("Bullet"))
        {
            EndGame();
        }
    }

    void EndGame()
    {
        // Aqu� puedes implementar la l�gica para finalizar el juego
        // Por ejemplo, recargar la escena actual o cargar una pantalla de Game Over
        Debug.Log("Game Over! Player has been hit by an enemy bullet.");
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);  // Recargar la escena actual
    }
}
