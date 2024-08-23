using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public float speed = 10f;

    void Update()
    {
        // Obtén la entrada del jugador (teclas WASD o flechas)
        float moveHorizontal = Input.GetAxis("Horizontal");
        float moveVertical = Input.GetAxis("Vertical");

        // Calcula el vector de movimiento
        Vector3 movement = new Vector3(moveHorizontal, 0.0f, moveVertical);

        // Mueve el jugador basado en la entrada
        transform.Translate(movement * speed * Time.deltaTime, Space.World);
    }
}
