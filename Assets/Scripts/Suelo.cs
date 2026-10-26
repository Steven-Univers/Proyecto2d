using UnityEngine;

public class MecanicaExplosion : MonoBehaviour
{
    // Arrastra aquí tu prefab de partículas de explosión desde el Inspector
    public GameObject efectoExplosion; 

    // CAMBIO AQUÍ: Se añade "2D" tanto al método como al tipo de variable
    private void OnCollisionEnter2D(Collision2D collision)
    {
        // CAMBIO AQUÍ: Asegúrate de escribir "SUELO" en mayúsculas como en tu Inspector
        if (collision.gameObject.CompareTag("SUELO"))
        {
            Explotar();
        }
    }

    void Explotar()
    {
        // 1. Instanciar (crear) el efecto visual en la posición actual del objeto
        if (efectoExplosion != null)
        {
            Instantiate(efectoExplosion, transform.position, transform.rotation);
        }

        // 2. Destruir este objeto que acaba de colisionar
        Destroy(gameObject);
    }
}
