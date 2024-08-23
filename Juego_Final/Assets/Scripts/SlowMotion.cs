using UnityEngine;

public class SlowMotion : MonoBehaviour
{
    public float slowMotionScale = 0.2f; // Escala de tiempo para cámara lenta
    public float normalTimeScale = 1f;   // Escala de tiempo normal
    public KeyCode slowMotionKey = KeyCode.LeftShift; // Tecla para activar la cámara lenta

    void Update()
    {
        // Verifica si la tecla para cámara lenta está siendo presionada
        if (Input.GetKey(slowMotionKey))
        {
            Time.timeScale = slowMotionScale;  // Ralentiza el tiempo
            Time.fixedDeltaTime = 0.02f * Time.timeScale; // Ajusta el tiempo fijo para mantener consistencia en la física
        }
        else
        {
            Time.timeScale = normalTimeScale;  // Restaura el tiempo normal
            Time.fixedDeltaTime = 0.02f * Time.timeScale; // Ajusta el tiempo fijo para mantener consistencia en la física
        }
    }
}
