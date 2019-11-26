using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using UnityEngine;
using UnityEngine.SceneManagement;

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
    public KeyCode restartMap;
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
    public bool addBanana = false;
    public bool isRed;
    public bool isBlue;
    public int bananaCount;

    private Animator _animator;
    private Rigidbody2D _rigidBody;
    private Stopwatch _stopwatch;

    public GameObject banana;
    public Transform throwPoint;
    public GameObject bananaInventory;

    // Start is called before the first frame update
    public void Start()
    {
        bananaInventory.SetActive(false);

        _initialMovementSpeed = movementSpeed;
        _initialJumpForce = jumpForce;

        _rigidBody = GetComponent<Rigidbody2D>();
        _animator = GetComponent<Animator>();
        _stopwatch = new Stopwatch();
    }

    // Update is called once per frame
    public void Update()
    {
        HandleRestartMap();
        HandleMovement();
        HandleAnimation();
        HandleHit();
        HandleJumpOnPlayer();
        HandleBananaThrow();
        HandleBananaAdd();
    }

    private void HandleRestartMap() {
        if (Input.GetKeyDown(restartMap)) {
            SceneManager.LoadScene("SampleScene");
     }
    }

    private void HandleMovement()
    {
        isGrounded =
            Physics2D.OverlapCircle(groundCheckPoint1.position, groundCheckRadius, whatIsGround)
            ||
            Physics2D.OverlapCircle(groundCheckPoint2.position, groundCheckRadius, whatIsGround);

        if (Input.GetKey(left))
        {
            if (!_stopwatch.IsRunning && isGrounded)
            {
                SoundManagerScript.PlaySound("walk");
            }
            _rigidBody.velocity = new Vector2(-movementSpeed, _rigidBody.velocity.y);
        }
        else if (Input.GetKey(right))
        {
            if (!_stopwatch.IsRunning && isGrounded)
            {
                SoundManagerScript.PlaySound("walk");
            }
            _rigidBody.velocity = new Vector2(movementSpeed, _rigidBody.velocity.y);
        }
        else if(Input.GetKeyUp(left) || Input.GetKeyUp(right))
        {
            SoundManagerScript.PlaySound("stopWalk");
        }
        else // Prevent sliding once key is released
        {
            _rigidBody.velocity = new Vector2(0, _rigidBody.velocity.y);
        }

        if (Input.GetKeyDown(jump))
        {

            if (!_isDoubleJumpUsed && isGrounded)
            {
                SoundManagerScript.PlaySound("jump");
                _rigidBody.velocity = new Vector2(_rigidBody.velocity.x, jumpForce);
            }

            if (!_isDoubleJumpUsed && !isGrounded) 
            {
                SoundManagerScript.PlaySound("jump");
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
            SoundManagerScript.PlaySound("jump");
            _rigidBody.velocity = new Vector2(_rigidBody.velocity.x, jumpForce * 1.3f);
        }
    }

    private void HandleHit()
    {
        if (isHit)
        {
            SoundManagerScript.PlaySound("playerHit");
            SoundManagerScript.PlaySound("stopWalk");

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
        _animator.SetBool("Falling", _rigidBody.velocity.y < 0 && !isGrounded);
        _animator.SetBool("Hit", isHit);
    }

    private void HandleBananaThrow()
    {
        if (Input.GetKeyDown(bananaThrow))
        {
            if (bananaCount != 0)
            {
                SoundManagerScript.PlaySound("fire");
                bananaCount--;
                GameObject bananaClone = (GameObject)Instantiate(banana, throwPoint.position, throwPoint.rotation);
                bananaClone.transform.localScale = transform.localScale;
                bananaInventory.SetActive(false);
                // TODO trigger throw animation
            }
        }
    }

    private void HandleBananaAdd()
    {
        if (addBanana)
        {
            if (bananaCount < 1)
            {
                bananaCount++;
                bananaInventory.SetActive(true);
            }            
            addBanana = false;
        }
    }
}
