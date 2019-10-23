using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public float movementSpeed;
    public float jumpForce;

    public KeyCode left;
    public KeyCode right;
    public KeyCode jump;
    // TODO: Add more controlls

    public Transform groundCheckPoint;
    public bool isGrounded;
    public float groundCheckRadius;
    public LayerMask whatIsGround;
    public int _jumpCount = 0;

    private Animator _animator;

    private Rigidbody2D _rigidBody;

    // Start is called before the first frame update
    public void Start()
    {
        _rigidBody = GetComponent<Rigidbody2D>();
        _animator = GetComponent<Animator>();
    }

    // Update is called once per frame
    public void Update()
    {
        HandleMovement();
        HandleAnimation();
    }

    private void HandleMovement()
    {
        isGrounded = Physics2D.OverlapCircle(groundCheckPoint.position, groundCheckRadius, whatIsGround);

        if (Input.GetKey(left))
        {
            _rigidBody.velocity = new Vector2(-movementSpeed, _rigidBody.velocity.y);
        }
        else if (Input.GetKey(right))
        {
            _rigidBody.velocity = new Vector2(movementSpeed, _rigidBody.velocity.y);
        }
        else // Prevent sliding once key is released
        {
            _rigidBody.velocity = new Vector2(0, _rigidBody.velocity.y);
        }

        if (Input.GetKeyDown(jump))
        {
            _jumpCount++;

            if (_jumpCount < 2)
            {
                _rigidBody.velocity = new Vector2(_rigidBody.velocity.x, jumpForce);
            }                  
        }

        if (isGrounded)
        {
            _jumpCount = 0;
        }
    }

    private void HandleAnimation()
    {
        // Flip the player based on movement direction
        if (_rigidBody.velocity.x < 0)
        {
            transform.localScale = new Vector3(-1, 1, 1);
        } 
        else if (_rigidBody.velocity.x > 0)
        {
            transform.localScale = new Vector3(1, 1, 1);
        }

        _animator.SetFloat("Speed", Mathf.Abs(_rigidBody.velocity.x));
        _animator.SetBool("Grounded", isGrounded);
    }
}
