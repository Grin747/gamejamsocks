// ReSharper disable ArrangeTypeMemberModifiers

using UnityEngine;
using UnityEngine.SceneManagement;

public abstract class PlayerController : MonoBehaviour
{
    private Rigidbody2D _rb;
    [SerializeField] private float speed = 5f;
    [SerializeField] private float jumpForce = 10f;
    private bool _isJump;
    [SerializeField] private LayerMask groundLayer;
    [SerializeField] private Transform groundCheck;
    [SerializeField] private int health;
    public EntityFacing Facing { get; private set; }

    public void Damage(int damage) => health -= damage;

    void Start()
    {
        _rb = GetComponent<Rigidbody2D>();
        Facing = EntityFacing.Right;
    }

    void Update()
    {
        if (Input.GetButtonDown("Jump") && IsDownGround())
        {
            _rb.AddForce(new Vector2(0, jumpForce), ForceMode2D.Impulse);
        }
    }

    private void FixedUpdate()
    {
        var horizontal = Input.GetAxis("Horizontal") * speed;
        Flip(horizontal);
        _rb.velocity = new Vector2(horizontal, _rb.velocity.y);
        CheckAlive();
    }

    private void CheckAlive()
    {
        if (health <= 0)
        {
            Destroy(gameObject);
            SceneManager.LoadScene(0);
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag.Equals("Enemy"))
        {
            var enemy = FindObjectOfType<EnemyController>();
            Damage(enemy.damageLevel);
        }
    }

    private void Flip(float horizontal)
    {
        if (Facing == EntityFacing.Right && horizontal < 0)
        {
            Facing = EntityFacing.Left;
            transform.Rotate(0f, 180f, 0f);
        }
        else if (Facing == EntityFacing.Left && horizontal > 0)
        {
            Facing = EntityFacing.Right;
            transform.Rotate(0f, 180f, 0f);
        }
    }

    private bool IsDownGround() => Physics2D.OverlapCircle(groundCheck.position, 0.2f, groundLayer);
}