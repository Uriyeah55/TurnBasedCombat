using UnityEngine;

public class MusicNote : MonoBehaviour
{
    public RectTransform noteRectTransform;  // Referencia al RectTransform de la nota
    public Vector2 startPos;                 // Punto A (inicio)
    public Vector2 endPos;                   // Punto B (final)
    public float duration = 5f;              // Duración del movimiento de A a B

    private float elapsedTime = 0f;

    void Start()
    {
        // Inicializa la posición de la nota en el punto A
        noteRectTransform.anchoredPosition = startPos;
    }

    void Update()
    {
        // Calcula el tiempo transcurrido
        elapsedTime += Time.deltaTime;

        // Interpola la posición entre A y B usando Lerp
        noteRectTransform.anchoredPosition = Vector2.Lerp(startPos, endPos, elapsedTime / duration);

        // Destruye la nota cuando llega al punto B
        if (elapsedTime >= duration)
        {
            Destroy(gameObject);
        }
    }
}
