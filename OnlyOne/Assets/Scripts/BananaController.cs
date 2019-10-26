using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BananaController : MonoBehaviour
{

    public float horizontalSpeed;
    public float verticalInitialSpeed;

    public GameObject bananaEffect;

    private Rigidbody2D rb;

    // Start is called before the first frame update
    void Start() 
    {
        rb = GetComponent<Rigidbody2D>();
        rb.velocity= new Vector2(0, verticalInitialSpeed);
    }

    // Update is called once per frame
    void Update() 
    {
        rb.velocity = new Vector2(horizontalSpeed * transform.localScale.x, rb.velocity.y);
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        string hitTag = other.tag;
        if (other.tag == "RedPlayer" || other.tag == "BluePlayer")
        {
            bool isRed = false;
            if (other.tag == "RedPlayer")
            {
                isRed = true;
            }

            Object[] players = FindObjectsOfType<PlayerController>();
            Debug.Log(players + " : " + players.Length);
            Debug.Log("isRed : " + isRed);
            foreach (PlayerController player in players)
            {
                Debug.Log(player + " isRed : " + player.isRed);
                Debug.Log(player + " isBlue : " + player.isBlue);
                if (player.isRed && isRed)
                {
                    player.isHit = true;
                }

                if (player.isBlue && !isRed)
                {
                    player.isHit = true;
                }
            }
        }

        Instantiate(bananaEffect, transform.position, transform.rotation);

        Destroy(gameObject);
    }
}
