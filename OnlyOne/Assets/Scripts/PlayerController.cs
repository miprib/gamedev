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
    public KeyCode bananaThrow;
    // TODO: Add more controlls

    public Transform groundCheckPoint1;
    public Transform groundCheckPoint2;
    public bool isGrounded;
    public bool isOnPlayer;
    public float groundCheckRadius;
    public LayerMask whatIsGround;
    public LayerMask whatIsOtherPlayer;
    private bool _isDoubleJumpUsed = false;
    public int stunTimeMs;
    public bool isHit = false;
    public bool isRed;
    public bool isBlue;

    private Animator _animator;
    private Rigidbody2D _rigidBody;
    private Stopwatch _stopwatch;

    public GameObject banana;
    public Transform throwPoint;

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
        HandleHit();
        HandleJumpOnPlayer();
        HandleBananaThrow();
    }

    private void HandleMovement()
    {
        isGrounded =
            Physics2D.OverlapCircle(groundCheckPoint1.position, groundCheckRadius, whatIsGround)
            ||
            Physics2D.OverlapCircle(groundCheckPoint2.position, groundCheckRadius, whatIsGround);

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
            if (!_isDoubleJumpUsed && isGrounded)
            {
                _rigidBody.velocity = new Vector2(_rigidBody.velocity.x, jumpForce);
            }

            if (!_isDoubleJumpUsed && !isGrounded) 
            {
                _rigidBody.velocity = new Vector2(_rigidBody.velocity.x, jumpForce);
                _isDoubleJumpUsed = true; 
            }
        }

        if (isGrounded)
        {           
            _isDoubleJumpUsed = false;
        }       
    }

    void OnCollisionEnter(Collision collision)
    {
        UnityEngine.Debug.Log("Colision started");
    }


    private void HandleJumpOnPlayer()
    {
        // Player must be falling and at least one foot has to make contact
        isOnPlayer =
            (
                Physics2D.OverlapCircle(groundCheckPoint1.position, groundCheckRadius, whatIsOtherPlayer)
                ||
                Physics2D.OverlapCircle(groundCheckPoint2.position, groundCheckRadius, whatIsOtherPlayer)
            )
            &&
            _rigidBody.velocity.y < 0;

        if (isOnPlayer)
        {         
            _rigidBody.velocity = new Vector2(_rigidBody.velocity.x, jumpForce * 1.3f);
        }
    }

    private void HandleHit()
    {
        isHit = (_isDoubleJumpUsed && isGrounded) ? true : false; // This is temporary

        if (isHit)
        {
            isHit = false;

            movementSpeed = 0;
            jumpForce = 0;

            _stopwatch.Start();
        }

        if (_stopwatch.IsRunning && _stopwatch.ElapsedMilliseconds > stunTimeMs)
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
        _animator.SetBool("Hit", isHit);
    }

    private void HandleBananaThrow()
    {
        if (Input.GetKeyDown(bananaThrow))
        {
            GameObject bananaClone = (GameObject) Instantiate(banana, throwPoint.position, throwPoint.rotation);
            bananaClone.transform.localScale = transform.localScale;
            // TODO trigger throw animation
        }
    }
}
