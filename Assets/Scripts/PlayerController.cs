using UnityEngine;

public class PlayerController : MonoBehaviour
{
    private Rigidbody2D _rigidbody;
    [SerializeField] private float speed = 5f;
    [SerializeField] private float jumpForce = 10f;
    private bool _isJump;
    [SerializeField] private LayerMask groundLayer;
    [SerializeField] private Transform groundCheck;

    private void Start()
    {
        this._rigidbody = GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        var horizontal = Input.GetAxis("Horizontal") * speed;
        transform.position += new Vector3(horizontal, 0, 0) * Time.deltaTime * speed;
        if (Input.GetButtonDown("Jump") && IsDownGround())
        {
            _rigidbody.AddForce(new Vector2(0, jumpForce), ForceMode2D.Impulse);
        }
    }

    private bool IsDownGround() => Physics2D.OverlapCircle(groundCheck.position, 0.2f, groundLayer);
}