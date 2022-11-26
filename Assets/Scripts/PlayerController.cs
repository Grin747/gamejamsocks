// ReSharper disable ArrangeTypeMemberModifiers

using DefaultNamespace;
using UnityEngine;

public abstract class PlayerController : MonoBehaviour
{
    private Rigidbody2D _rb;
    [SerializeField] private float speed = 15f;
    [SerializeField] private float jumpForce = 10f;
    private bool _isJump;
    [SerializeField] private LayerMask groundLayer;
    [SerializeField] private Transform groundCheck;
    private EntityFacing _facing;
    public EntityFacing Facing => _facing;

    void Start()
    {
        _rb = GetComponent<Rigidbody2D>();
        _facing = EntityFacing.RIGHT;
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
        _rb.AddForce(new Vector2(horizontal, 0));
    }

    private void Flip(float horizontal)
    {
        if (_facing == EntityFacing.RIGHT && horizontal > 0)
        {
            _facing = EntityFacing.LEFT;
        }
        else
        {
            _facing = EntityFacing.RIGHT;
        }

        transform.Rotate(0f, 180f, 0f);
    }

    private bool IsDownGround() => Physics2D.OverlapCircle(groundCheck.position, 0.2f, groundLayer);
}