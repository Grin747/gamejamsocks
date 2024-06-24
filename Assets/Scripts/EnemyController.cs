using System;
using UnityEngine;

public class EnemyController : MonoBehaviour
{
    private Rigidbody2D _rb;
    private EntityFacing _facing;
    private bool _isJump;
    private Transform _target;
    
    [SerializeField] private int health;
    [SerializeField] private float speed;
    [SerializeField] public int damageLevel;
    [SerializeField] private LayerMask groundLayer;
    [SerializeField] private Transform groundCheck;
    [SerializeField] private Transform wallCheck;
    public Transform leftBar;
    public Transform rightBar;
    [SerializeField] private float jumpForce;

    public void Damage(int damage) => health -= damage;

    private void Start()
    {
        _rb = GetComponent<Rigidbody2D>();
        _facing = EntityFacing.Left;
        var player = GameObject.Find("Collector");
        _target = player.transform;
    }

    private void FixedUpdate()
    {
        var targetX = _target.position.x;
        var selfX = transform.position.x;
        var difference = targetX - selfX;

        if (targetX < rightBar.position.x && targetX > leftBar.position.x)
        {
            Flip(difference);
            _rb.velocity = new Vector2(Mathf.Sign(difference) * speed, _rb.velocity.y);
        }
        else
        {
            
        }
        
        CheckAlive();
        CheckWall();
    }

    private void CheckWall()
    {
        if (IsDownGround() && IsWallInFront() && !_isJump)
        {
            _isJump = true;
            _rb.AddForce(new Vector2(0, jumpForce), ForceMode2D.Impulse);
        }
        else if (_isJump)
        {
            _isJump = false;
        }
    }

    private void OnCollisionEnter2D(Collision2D col)
    {
        if (col.gameObject.tag.Equals("Player"))
        {
            var player = col.gameObject.GetComponent<PlayerController>();
            player.Damage(damageLevel);

            var dif = col.collider.gameObject.transform.position.x - gameObject.transform.position.x;
            col.rigidbody.AddForce(new Vector2(-Mathf.Sign(dif) * 100, 4f), ForceMode2D.Impulse);
            _rb.AddForce(new Vector2(Mathf.Sign(dif) * 100, 4f), ForceMode2D.Impulse);
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