using UnityEngine;

public class Level1Cat : MonoBehaviour
{
    public float upPower = 20f;
    public GameObject attackPrefab; 
    public Transform firePoint;     

    private Rigidbody2D rb;
    private bool canAttack = false;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        if (canAttack && Input.GetMouseButtonDown(0))
        {
            Vector3 direction = transform.localScale.x > 0 ? Vector3.right : Vector3.left;
            GameObject go = Instantiate(attackPrefab, firePoint.position, Quaternion.identity);
            Rigidbody2D goRb = go.GetComponent<Rigidbody2D>();
            if (goRb != null)
            {
                goRb.velocity = direction * 40;
            }
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("UpPower"))
        {
            rb.velocity = new Vector2(rb.velocity.x, 0f);
            rb.AddForce(Vector2.up * upPower, ForceMode2D.Impulse);
        }

        if (collision.CompareTag("AttackItem"))
        {
            canAttack = true;
            Destroy(collision.gameObject);
        }
    }
}
