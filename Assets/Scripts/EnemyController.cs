using System;
using UnityEngine;

public class EnemyController : MonoBehaviour
{
    [SerializeField] private int health;
    private Rigidbody2D _rb;
    private EntityFacing _facing;
    [SerializeField] private float speed;
    [SerializeField] public int damageLevel;
    [SerializeField] private LayerMask groundLayer;
    [SerializeField] private Transform groundCheck;
    [SerializeField] private Transform wallCheck;
    [SerializeField] private float jumpForce;
    private bool isJump = false;

    public void Damage(int damage) => health -= damage;

    private void Start()
    {
        _rb = GetComponent<Rigidbody2D>();
        _facing = EntityFacing.Right;
    }

    private void FixedUpdate()
    {
        var player = GameObject.Find("Collector");
        var horizontal = player.transform.position;
        var objectHorizontal = transform.position;
        var difference = horizontal.x - objectHorizontal.x;
        if (Math.Abs(difference) <= 15)
        {
            Flip(difference);
            _rb.velocity = new Vector2(difference * speed, _rb.velocity.y);
            CheckAlive();
            CheckWall();
        }
    }

    private void CheckWall()
    {
        if (IsDownGround() && IsWallInFront() && !isJump)
        {
            isJump = true;
            _rb.AddForce(new Vector2(0, jumpForce), ForceMode2D.Impulse);
        }
        else if (isJump)
        {
            isJump = false;
        }
    }

    private void OnCollisionEnter2D(Collision2D col)
    {
        if (col.gameObject.tag.Equals("Player"))
        {
            var player = col.gameObject.GetComponent<PlayerController>();
            player.Damage(damageLevel);
        }
    }

    private void CheckAlive()
    {
        if (health <= 0)
        {
            Destroy(gameObject);
        }
    }

    private void Flip(float horizontal)
    {
        if (_facing == EntityFacing.Right && horizontal < 0)
        {
            _facing = EntityFacing.Left;
            transform.Rotate(0f, 180f, 0f);
        }
        else if (_facing == EntityFacing.Left && horizontal > 0)
        {
            _facing = EntityFacing.Right;
            transform.Rotate(0f, 180f, 0f);
        }
    }

    private bool IsWallInFront() => Physics2D.OverlapCircle(wallCheck.position, 0.2f, groundLayer);

    private bool IsDownGround() => Physics2D.OverlapCircle(groundCheck.position, 0.2f, groundLayer);
}