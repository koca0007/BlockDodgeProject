using UnityEngine;

public class Player : MonoBehaviour
{
    public float speed = 15f;
    public float mapWidth = 5f;

    private Rigidbody2D rb;

   
    private AudioSource audioSource2;
    public AudioClip explosionSound;


    private void Awake()
    {
        audioSource2 = GetComponent<AudioSource>();
    }
    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }
    private void FixedUpdate()
    {
        float a = Input.GetAxis("Horizontal") * Time.fixedDeltaTime * speed;

        Vector2 newPosition = rb.position + Vector2.right * a;

        newPosition.x = Mathf.Clamp(newPosition.x, -mapWidth, mapWidth);

        rb.MovePosition(newPosition);
    }

    public void OnCollisionEnter2D(Collision2D collision)
    {
        audioSource2.PlayOneShot(explosionSound, 0.1f);
        speed = 0;
        FindObjectOfType<Score>().StartCoroutine("EndGame");
    }
}
