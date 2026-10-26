using UnityEngine;

public class MovimientoNave : MonoBehaviour
{
        public float fuerza = 5f;
    private SpriteRenderer spriteRenderer;
    private Rigidbody2D rigidbody2D;

    void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        rigidbody2D = GetComponent<Rigidbody2D>();
    }


    // Update is called once per frame
    void Update()
    {
        if (Input.GetKey(KeyCode.UpArrow) || Input.GetKey(KeyCode.W))
        {
            rigidbody2D.AddForce(Vector2.up*fuerza );
            spriteRenderer.color = Color.red;
        };

        if (Input.GetKey(KeyCode.DownArrow) || Input.GetKey(KeyCode.S))
        {
            rigidbody2D.AddForce(Vector2.down*fuerza);
            spriteRenderer.color = Color.yellow;
        };

        if (Input.GetKey(KeyCode.LeftArrow) || Input.GetKey(KeyCode.A))
        {
            rigidbody2D.AddForce(Vector2.left*fuerza);
            spriteRenderer.color = Color.orange;
        };

        if (Input.GetKey(KeyCode.RightArrow) || Input.GetKey(KeyCode.D))
        {
            rigidbody2D.AddForce(Vector2.right*fuerza);
            spriteRenderer.color = Color.orangeRed;
        };
    }
}
