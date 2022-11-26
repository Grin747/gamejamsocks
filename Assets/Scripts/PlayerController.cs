using UnityEngine;

public class PlayerController : MonoBehaviour
{
    private Rigidbody2D rb;
    [SerializeField] public float speed = 30f;
    [SerializeField] private float JumpForce = 5f;
    private bool _isJump;
    [SerializeField] private LayerMask GroundLayer;
    [SerializeField] private Transform groundCheck;

    private void Start()
    {
        this.rb = GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        var horizontal = Input.GetAxis("Horizontal") * speed;
        transform.position += new Vector3(horizontal, 0, 0) * Time.deltaTime * speed;
        if (Input.GetButtonDown("Jump") && IsDownGround())
        {
            rb.AddForce(new Vector2(0, JumpForce), ForceMode2D.Impulse);
        }
    }

    private bool IsDownGround() => Physics2D.OverlapCircle(groundCheck.position, 0.2f, GroundLayer);
}