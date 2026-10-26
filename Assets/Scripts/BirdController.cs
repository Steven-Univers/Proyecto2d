using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class BirdController : MonoBehaviour
{
    [Header("Movimiento")]
    public float flapForce = 6f;      // Fuerza del salto
    public float moveSpeed = 4f;      // Velocidad horizontal automática
    private Rigidbody2D rb;
    private int direction = 1;        // 1 = derecha, -1 = izquierda
    
    [Header("Referencias UI y Entorno")]
    public Camera mainCamera;
    public TextMeshProUGUI scoreText;
    public GameObject gameOverPanel;

    [Header("Configuración de Pinchos")]
    public GameObject spikePrefab;    // El Prefab del pincho que se instanciará
    public float minY = -3.5f;        // Altura mínima vertical donde puede aparecer el pincho
    public float maxY = 3.5f;         // Altura máxima vertical donde puede aparecer el pincho
    public float wallXPosition = 7f;  // Coordenada X aproximada donde se ubican las paredes laterales
    
    private int score = 0;
    private bool isDead = false;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        if (mainCamera == null) mainCamera = Camera.main;
        if (gameOverPanel != null) gameOverPanel.SetActive(false);
    }

    void Update()
    {
        if (isDead) return;

        // Movimiento horizontal constante y salto al hacer clic o presionar Espacio
        rb.linearVelocity = new Vector2(moveSpeed * direction, rb.linearVelocity.y);

        if (Input.GetMouseButtonDown(0) || Input.GetKeyDown(KeyCode.Space))
        {
            rb.linearVelocity = new Vector2(rb.linearVelocity.x, flapForce);
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (isDead) return;

        // Si choca con una pared lateral
        if (collision.gameObject.CompareTag("wall"))
        {
            // Cambiar dirección horizontal
            direction *= -1;

            // Cambiar el color de fondo de la cámara de forma aleatoria
            mainCamera.backgroundColor = new Color(Random.value, Random.value, Random.value);

            // Incrementar contador
            score++;
            if (scoreText != null)
            {
                scoreText.text = "Puntos: " + score;
            }

            // Llamamos a la función para crear un pincho aleatorio en la pared opuesta
            SpawnRandomSpike();
        }
        // Si cae al suelo
        else if (collision.gameObject.CompareTag("floor"))
        {
            Die();
        }
    }

    // NUEVO: Método para instanciar pinchos en posiciones aleatorias de la pared
    void SpawnRandomSpike()
    {
        // Verificamos que se haya asignado un prefab en el inspector para evitar errores
        if (spikePrefab != null)
        {
            // 1. Elegimos una altura (Y) aleatoria dentro de los límites establecidos
            float randomY = Random.Range(minY, maxY);
            
            // 2. Determinamos en qué pared aparecerá el pincho dependiendo hacia dónde va el pájaro.
            // Si el pájaro va hacia la derecha (direction = 1), el pincho se coloca en la pared izquierda (-wallXPosition).
            // Si va hacia la izquierda (direction = -1), se coloca en la pared derecha (wallXPosition).
            float spawnX = (direction > 0) ? -wallXPosition : wallXPosition;
            
            // 3. Construimos la posición final en 2D (X, Y)
            Vector3 spawnPosition = new Vector3(spawnX, randomY, 0f);

            // 4. Instanciamos (creamos) el pincho en la escena con la rotación por defecto
            Instantiate(spikePrefab, spawnPosition, Quaternion.identity);
        }
    }

    // NUEVO: Detección si el pájaro toca un pincho (requiere que el pincho tenga un Collider con "Is Trigger" activado y Tag "spike")
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (isDead) return;

        if (collision.CompareTag("spike"))
        {
            Die(); // Si toca el pincho, el jugador pierde
        }
    }

    void Die()
    {
        isDead = true;
        rb.linearVelocity = Vector2.zero;
        rb.simulated = false; // Desactiva las físicas para que se quede quieto

        if (gameOverPanel != null)
        {
            gameOverPanel.SetActive(true); // Muestra el menú de Game Over
        }
    }

    // Método para conectar con el botón de reiniciar
    public void RestartGame()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
}