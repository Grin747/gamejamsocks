using DefaultNamespace;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    private float speed = 25f;
    private Rigidbody2D rb;
    private EntityFacing _facing;

    private void Start()
    {
        this.rb = GetComponent<Rigidbody2D>();
        rb.velocity = transform.right * speed;
    }

    private void OnCollisionEnter2D(Collision2D col)
    {
        if (col.gameObject.tag.Equals("GameGround"))
        {
            Destroy(gameObject);
        }
    }
}
