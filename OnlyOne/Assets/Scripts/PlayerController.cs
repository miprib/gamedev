using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public float movementSpeed;
    private float _initialMovementSpeed;
    public float jumpForce;
    private float _initialJumpForce;
    
    public KeyCode left;
    public KeyCode right;
    public KeyCode jump;
    // TODO: Add more controlls

    public Transform groundCheckPoint;
    public bool isGrounded;
    public float groundCheckRadius;
    public LayerMask whatIsGround;
    public bool _isDoubleJumpUsed = false;
    private bool _isHit = false;

    private Animator _animator;
    private Rigidbody2D _rigidBody;
    private Stopwatch _stopwatch;

    public bool swRunning;

    // Start is called before the first frame update
    public void Start()
    {
        _initialMovementSpeed = movementSpeed;
        _initialJumpForce = jumpForce;

        _rigidBody = GetComponent<Rigidbody2D>();
        _animator = GetComponent<Animator>();
        _stopwatch = new Stopwatch();
    }

    // Update is called once per frame
    public void Update()
    {
        HandleMovement();
        HandleAnimation();
    }

    private void HandleMovement()
    {

        swRunning = _stopwatch.IsRunning;
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
            if (!_isDoubleJumpUsed)
            {
                _rigidBody.velocity = new Vector2(_rigidBody.velocity.x, jumpForce);
            }

            if (!isGrounded) 
            { 
                _isDoubleJumpUsed = true; 
            }
        }

        if (isGrounded)
        {
            _isHit = (_isDoubleJumpUsed) ? true : false; // This is temporary
            _isDoubleJumpUsed = false;
        }

        HandleHit();
    }

    private void HandleHit()
    {
        if (_isHit)
        {
            movementSpeed = 0;
            jumpForce = 0;

            _stopwatch.Start();
        }

        if (_stopwatch.IsRunning && _stopwatch.ElapsedMilliseconds > 1500)
        {
            _stopwatch.Reset();

            movementSpeed = _initialMovementSpeed;
            jumpForce = _initialJumpForce;
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
        _animator.SetBool("Double Jump", _isDoubleJumpUsed);
        _animator.SetBool("Falling", _rigidBody.velocity.y < 0);
        _animator.SetBool("Hit", _isHit);
    }
}
