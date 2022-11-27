// ReSharper disable ArrangeTypeMemberModifiers

using System;
using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public abstract class PlayerController : MonoBehaviour
{
    protected enum MovementType
    {
        Keyboard,
        Controller
    }

    private Rigidbody2D _rb;
    private EntityFacing _facing;

    [SerializeField] private Animator animator;
    [SerializeField] protected MovementType movementType;
    [SerializeField] private float speed = 5f;
    [SerializeField] private float jumpForce = 10f;
    [SerializeField] private LayerMask groundLayer;
    [SerializeField] private Transform groundCheck;
    [SerializeField] protected int health = 6;
    public bool IsTeleporting { get; private set; }

    public void Teleport()
    {
        IsTeleporting = true;
        StartCoroutine(nameof(TeleportDelay));
    }

    private IEnumerator TeleportDelay()
    {
        yield return new WaitForSeconds(.2f);
        IsTeleporting = false;
    }

    public void Damage(int damage) => health -= damage;

    void Start()
    {
        _rb = GetComponent<Rigidbody2D>();
        _facing = EntityFacing.Right;
    }

    void Update()
    {
        if (movementType == MovementType.Keyboard)
        {
            if (Input.GetButtonDown("Jump") && IsDownGround())
            {
                _rb.AddForce(new Vector2(0, jumpForce), ForceMode2D.Impulse);
            }

            if (Input.GetButtonDown("Fire1"))
            {
                PlayerAction();
            }
        }
        else
        {
            if (movementType == MovementType.Controller)
            {
                if (Input.GetButtonDown("JumpGamepad") && IsDownGround())
                {
                    _rb.AddForce(new Vector2(0, jumpForce), ForceMode2D.Impulse);
                }
            }

            if (Input.GetButtonDown("Fire1Gamepad"))
            {
                PlayerAction();
            }
        }
    }

    protected abstract void PlayerAction();

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

    void FixedUpdate()
    {
        var horizontal = movementType == MovementType.Controller
            ? Input.GetAxis("HorizontalGamepad")
            : Input.GetAxis("Horizontal");
        Flip(horizontal);
        _rb.velocity = new Vector2(horizontal * speed, _rb.velocity.y);
        animator.SetFloat("Speed", Mathf.Abs(horizontal));
        CheckAlive();
    }

    private void CheckAlive()
    {
        if (health > 0) return;

        Destroy(gameObject);
        SceneManager.LoadScene(0);
    }

    private bool IsDownGround() => Physics2D.OverlapCircle(groundCheck.position, 0.2f, groundLayer);
}