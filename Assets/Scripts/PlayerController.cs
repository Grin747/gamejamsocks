// ReSharper disable ArrangeTypeMemberModifiers
using UnityEngine;

public abstract class PlayerController : MonoBehaviour
{
    private Rigidbody2D _rb;
    [SerializeField] private float speed = 5f;
    [SerializeField] private float jumpForce = 10f;
    private bool _isJump;
    [SerializeField] private LayerMask groundLayer;
    [SerializeField] private Transform groundCheck;
    public EntityFacing Facing { get; private set; }

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
    }

    private void Flip(float horizontal)
    {
        if (Facing == EntityFacing.Right && horizontal > 0)
        {
            Facing = EntityFacing.Left;
        }
        else
        {
            Facing = EntityFacing.Right;
        }

        transform.Rotate(0f, 180f, 0f);
    }

    private bool IsDownGround() => Physics2D.OverlapCircle(groundCheck.position, 0.2f, groundLayer);
}