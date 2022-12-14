using UnityEngine;

public class Bullet : MonoBehaviour
{
    [SerializeField] private float speed = 25f;
    private Rigidbody2D _rb;
    private EntityFacing _facing;

    private void Start()
    {
        _rb = GetComponent<Rigidbody2D>();
        _rb.velocity = transform.right * speed;
    }

    private void OnCollisionEnter2D(Collision2D col)
    {
        if (!col.gameObject.CompareTag("Player"))
        {
            Destroy(gameObject);
        }
        if (col.gameObject.tag.Equals("Enemy"))
        {
            var enemyObject = FindObjectOfType<EnemyController>();
            enemyObject.Damage(2);
            Debug.Log("Enemy damaged");
            Destroy(gameObject);
        }
    }
}
