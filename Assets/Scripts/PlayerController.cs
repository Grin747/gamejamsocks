// ReSharper disable ArrangeTypeMemberModifiers

using System;
using UnityEngine;

public abstract class PlayerController : MonoBehaviour
{
    public enum MovementType
    {
        keyboard,
        controller
    }

    public MovementType movementType;
    public CharacterController characterController;

    private Rigidbody2D _rb;
    [SerializeField] private float speed = 15f;
    [SerializeField] private float jumpForce = 10f;
    private bool _isJump;
    [SerializeField] private LayerMask groundLayer;
    [SerializeField] private Transform groundCheck;
    private bool isOnGround;
    private float x;
    private Vector2 velocity;

    void Start()
    {
        _rb = GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        if (movementType == MovementType.keyboard)
        {
            if (Input.GetButtonDown("Jump") && IsDownGround())
            {
                _rb.AddForce(new Vector2(0, jumpForce), ForceMode2D.Impulse);
            }

            //keyboardmovement
            x = Input.GetAxis("Horizontal");

            Vector2 move = transform.right * x;

            // characterController.Move(move * speed * Time.deltaTime);

            // characterController.Move(velocity * Time.deltaTime);
            float horizontal = x * speed;
            _rb.AddForce(new Vector2(horizontal, 0));
        }
        else
        {
            if(movementType == MovementType.controller)
            {
                //keyboardmovement
                x = Input.GetAxis("HorizontalGamepad");

                if (Input.GetButtonDown("JumpGamepad") && IsDownGround())
                {
                    _rb.AddForce(new Vector2(0, jumpForce), ForceMode2D.Impulse);
                }
                
                float horizontal = x * speed;
                _rb.AddForce(new Vector2(horizontal, 0));
            }
        }
    }

    private void FixedUpdate()
    {
        //var horizontal = Input.GetAxis("Horizontal") * speed;
        //_rb.AddForce(new Vector2(horizontal, 0));
    }

    private bool IsDownGround() => Physics2D.OverlapCircle(groundCheck.position, 0.2f, groundLayer);
}